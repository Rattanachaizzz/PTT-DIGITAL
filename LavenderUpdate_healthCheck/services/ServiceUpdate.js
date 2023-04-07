const moment = require("moment");
const util = require('util');
const exec = util.promisify(require('child_process').exec);
const systemdStatus = require('systemd-status');
const baseControl = require('../basecontrol')
const io = require('socket.io-client');
const socket = io('http://10.195.2.141:6001');
const fs = require('fs');

module.exports.DownloadFile = async (downloadTime, messageID, siteCode) => {
  try {
    let dataSelectActions = await baseControl.serviceBase.SelectWaitDownloadOntime(downloadTime)
    if (dataSelectActions.length !== 0) {
      console.log(`Download Time : ${downloadTime}`)
      try {
        await exec('sudo mv /etc/apt/sources.list.d/nodesource.list  /etc/apt/sources.list.d/nodesource.list.old')
        await exec('sudo mv /etc/apt/sources.list.d/pgadmin4.list  /etc/apt/sources.list.d/pgadmin4.list.old')
        await exec('sudo mv /etc/apt/sources.list.d/pgdg.list.list  /etc/apt/sources.list.d/pgdg.list.list.old')
      } catch (error) {

      }
      await baseControl.serviceBase.data_stationCode().then((value) => {
        sendStatus_Downloading(value[0].value, dataSelectActions[0].message_id, dataSelectActions[0].action_id, dataSelectActions[0].service_name)
      });
      console.log(`Downloading`)
      await DeleteFileOldVersion(dataSelectActions)
      await exec('sudo apt-get update --allow-insecure-repositories');
      let dataDownload = []
      let responeDownloadAll = await Promise.all(dataSelectActions.map(async (data, i) => {
        let responeDownloadMain = await new Promise(async (resolve, reject) => {
          exec(`sudo apt-get -y --allow-unauthenticated upgrade --download-only ${data.service_name}`, async (error, stdout, stderr) => {
            if (error) {
              resolve({
                status_code: -1,
                Description: "Download failed",
                error: error.message
              })
              return;
            }
            if (stderr) {
              resolve({
                status_code: -1,
                Description: "Download failed",
                error: stderr
              })
              return;
            }
            if (stdout.includes("Download complete and in download only mode")) {
              resolve({
                status_code: 2,
                Description: "Waiting for trigger/execute",
                error: null
              })
            } else {
              resolve({
                status_code: -1,
                Description: "Download failed",
                error: "Download failed."
              })
            }
            return;
          })
        })
         //console.log(responeDownloadMain.status_code)
         //console.log(responeDownloadMain.Description)
        await baseControl.serviceBase.UpdateStatusCode(responeDownloadMain, data) // update status db.sqlite
        if (responeDownloadMain.status_code === 2) {
          dataDownload.push({
            version: data.version,
            downloadDate: moment().format('YYYY-MM-DD HH:mm:ss'),
            downloadStatus: "completed"
          })
          console.log(`Download Complete`)
        } else {
          dataDownload.push({
            version: data.version,
            downloadDate: moment().format('YYYY-MM-DD HH:mm:ss'),
            downloadStatus: "Failed"
          })
          console.log(`Download Failed`)
        }

        if (Number(data.rollback_enable) === 1) {
          let checkFileRollback = await new Promise(async (resolve, reject) => {
            fs.access(`/var/cache/apt/archives/${data.service_name}_${data.rollback_version}_amd64.deb`, fs.constants.F_OK, (err) => {
              if (err) {
                //console.log(err)
                resolve(err)
              } else {
                resolve(true)
              }
            });
          })
          if (checkFileRollback === true) {
            dataDownload.push({
              versionRollback: data.rollback_version,
              downloadDate: moment().format('YYYY-MM-DD HH:mm:ss'),
              downloadStatus: "completed"
            })
            console.log(`Download File_Rollback Complete`)
          } else {
            let responeDownloadRollback = await new Promise(async (resolve, reject) => {
              exec(`sudo apt-get -y --allow-unauthenticated upgrade --download-only ${data.service_name}=${data.rollback_version}`, async (error, stdout, stderr) => {
                if (error) {
                  resolve({
                    status_code: -1,
                    Description: "Download failed",
                    error: error.message
                  })
                  return;
                }
                if (stderr) {
                  resolve({
                    status_code: -1,
                    Description: "Download failed",
                    error: stderr
                  })
                  return;
                }

                if (stdout.includes("Download complete and in download only mode")) {
                  resolve({
                    status_code: 2,
                    Description: "Waiting for trigger/execute",
                    error: null
                  })
                } else {
                  resolve({
                    status_code: -1,
                    Description: "Download failed",
                    error: "Download failed."
                  })
                }
              });
            })

            //console.log("responeDownloadRollback :" + responeDownloadRollback);
            if (responeDownloadRollback.status_code === 2) {
              dataDownload.push({
                versionRollback: data.rollback_version,
                downloadDate: moment().format('YYYY-MM-DD HH:mm:ss'),
                downloadStatus: "completed"
              })
              console.log(`Download File_Rollback Complete`)
            } else {
              dataDownload.push({
                versionRollback: data.rollback_version,
                downloadDate: moment().format('YYYY-MM-DD HH:mm:ss'),
                downloadStatus: "Failed"
              })
              console.log(`Download File_Rollback Failed`)
            }
          }
        }
        let dataArray = {
          actionID: data.action_id,
          serviceName: data.service_name,
          download: dataDownload
        }
        return Promise.resolve(dataArray)
      }))

      let data_station = baseControl.serviceBase.data_stationCode();
      data_station.then((value) => {
        let msgSend = {};
        msgSend.messageID = Number(dataSelectActions[0].message_id);
        msgSend.clientID = value[0].value;
        msgSend.clientVersion = value[1].value;;
        msgSend.messageType = "operation-report";
        msgSend.data = responeDownloadAll;
        socket.emit('chat message', msgSend);
      })
    } else {
      console.log(`Can't find action on time`)
    }
  } catch (error) {
    console.log(`error ${error}`)
  }
}

module.exports.checkInstallService = async (dataSelectActions, messageID, siteCode) => {
  if (dataSelectActions.length !== 0) {
    exec(`sudo systemctl restart ${dataSelectActions[0].service_name}`)
    let counter = 0;
    const interval = setInterval(CheckStatusService, 5000);
    let msgSend_Ins_complete = {
      "messageID": messageID,
      "clientID": siteCode,
      "clientVersion": "1.0.0",
      "messageType": "operation-report",
      "data": [
        {
          "operation": dataSelectActions[0].operation,
          "actionID": dataSelectActions[0].action_id,
          "serviceName": dataSelectActions[0].service_name,
          "serviceStatus": "active",
          "installVersion": dataSelectActions[0].version,
          "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
          "rollbackStatus": "false"
        }
      ]
    };
    async function CheckStatusService() {
      counter++;
      //const serviceStatus = systemdStatus([dataSelectActions[0].service_name])
      exec(`sudo systemctl is-active ${dataSelectActions[0].service_name}`, (error, stdout, stderr) => {
        if (stdout.includes('active')) {
          baseControl.serviceBase.UpdateStatusCode({ status_code : 0 ,Description : 'Completed all process'}, { action_id : dataSelectActions[0].action_id})
          socket.emit('chat message', msgSend_Ins_complete);
          clearInterval(interval);
        }
        if (counter === 6) {
          RollbackInstall(dataSelectActions, messageID, siteCode)
          clearInterval(interval);
        }
      })
    }
  } else {
    console.log(`Can not find actions_id`)
  }
}

module.exports.checkPurgeService = async (nameService) => {
  return await new Promise((resolve, reject) => {
    exec(`sudo apt list --installed ${nameService}`, (error, stdout, stderr) => {
      if (stdout.includes("Listing...")) {
        resolve({
          status_code: 0,
          Description: "Completed all process",
          error: error
        })
        return
      }
      if (error) {
        resolve({
          status_code: -3,
          Description: "Purge failed",
          error: error.message
        })
        return;
      }
      resolve({
        status_code: -3,
        Description: "Purge failed",
        error: "Can not purge service!!!"
      })
      return;
    })
  })
}

async function RollbackInstall(dataSelectActions, messageID, siteCode) {
  if (dataSelectActions.length !== 0) {
    let rollbackStatus = await new Promise((resolve, reject) => {
      exec(`sudo apt-get -y --allow-unauthenticated --allow-downgrades install /var/cache/apt/archives/${dataSelectActions[0].service_name}_${dataSelectActions[0].rollback_version}_amd64.deb`, (error, stdout, stderr) => {
        if (error) {
          resolve({
            status_code: -2,
            Description: "Rollback failed",
            error: error.message
          })
        }
        if (stderr) {
          resolve({
            status_code: -2,
            Description: "Rollback failed",
            error: error.message
          })
        }
        resolve({
          status_code: 3,
          Description: "Rollback completed",
          error: error.message
        })
      })
    })

    let msgSend_Roll_complete = {
      "messageID": Number(messageID),
      "clientID": siteCode,
      "clientVersion": "1.0.0",
      "messageType": "operation-report",
      "data": [
        {
          "operation": dataSelectActions[0].operation,
          "actionID": "",
          "serviceName": dataSelectActions[0].service_name,
          "serviceStatus": "active",
          "installVersion": dataSelectActions[0].rollback_version,
          "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
          "rollbackStatus": "true"
        }
      ]
    };

    let msgSend_Roll_uncomplete = {
      "messageID": Number(messageID),
      "clientID": siteCode,
      "clientVersion": "1.0.0",
      "messageType": "operation-report",
      "data": [
        {
          "operation": dataSelectActions[0].operation,
          "actionID": "",
          "serviceName": dataSelectActions[0].service_name,
          "serviceStatus": "inactive",
          "installVersion": dataSelectActions[0].rollback_version,
          "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
          "rollbackStatus": "true"
        }
      ]
    };

    if (rollbackStatus.includes('Rollback failed')) {
      // socket.IO respone
      socket.emit('chat message', msgSend_Roll_uncomplete);
      baseControl.serviceBase.UpdateStatusCode(rollbackStatus, dataSelectActions[0])
      clearInterval(intervalRollbace);
    } else {
      let counter = 0;
      const intervalRollbace = setInterval(CheckStatusService, 5000);
      async function CheckStatusService() {
        counter++;
        const serviceStatus = systemdStatus([dataSelectActions[0].service_name])
        //console.log(serviceStatus)
        if (serviceStatus.isActive === true) {
          // socket.IO respone 
          //socket.emit('chat message', `start service rollback version ${dataSelectActions[0].version}`);
          //console.log(`start service rollback version ${dataSelectActions[0].version}`)
          baseControl.serviceBase.UpdateStatusCode(rollbackStatus, dataSelectActions[0])
          socket.emit('chat message', msgSend_Roll_complete);
          clearInterval(intervalRollbace);
        }
        if (counter === 6) {
          // socket.IO respone
          //socket.emit('chat message', `timeout can't start service`);
          //console.log("timeout can't start service rollback")
          rollbackStatus.then(() => {
            baseControl.serviceBase.UpdateStatusCode(({
              status_code: -2,
              Description: "Rollback failed",
              error: `Can not start service!`
            }), dataSelectActions[0])
          });
          socket.emit('chat message', msgSend_Roll_uncomplete);
          clearInterval(intervalRollbace);
        }
      }
    }
  } else {
    console.log(`can't find actions`)
  }

}

async function DeleteFileOldVersion(data) {

  const path = '/var/cache/apt/archives';
  const versioninstall = `${data[0].service_name}_${data[0].version}_amd64.deb`;
  const versionRollback = `${data[0].service_name}_${data[0].rollback_version}_amd64.deb`;

  fs.readdir(path, (err, files) => {
    if (err) throw err;
    files.forEach((file) => {
      if (file !== versioninstall && file !== versionRollback && file.includes(data[0].service_name)) {
        exec(`sudo rm -rf ${path}/${file}`, (error, stdout, stderr) => {
          if (error) {
            //console.log(error)
          }
          if (stderr) {
            //console.log(stderr)
          }
          //console.log(stdout)
        })
      }
    });
  });

}

async function sendStatus_Downloading(siteCode, messageID, actionID, service_name) {
  socket.emit('chat message', {
    "messageID": Number(messageID),
    "clientID": siteCode,
    "messageType": "Response",
    "actionID": actionID,
    "serviceName": service_name,
    "status": "Downloading",
    "timeStamp": moment().format('YYYY-MM-DD HH:mm:ss')
  })
}

module.exports.sendStatus_Purging = async (siteCode, messageID, actionID, service_name) => {
  socket.emit('chat message', {
    "messageID": Number(messageID),
    "clientID": siteCode,
    "messageType": "Response",
    "actionID": actionID,
    "serviceName": service_name,
    "status": "Purging",
    "timeStamp": moment().format('YYYY-MM-DD HH:mm:ss')
  })
}

module.exports.sendStatus_Installing = async (siteCode, messageID, actionID, service_name) => {
  socket.emit('chat message', {
    "messageID": Number(messageID),
    "clientID": siteCode,
    "messageType": "Response",
    "actionID": actionID,
    "serviceName": service_name,
    "status": "Installing",
    "timeStamp": moment().format('YYYY-MM-DD HH:mm:ss')
  })
}
