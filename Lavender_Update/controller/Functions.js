const config = require('../config')
const service = require('../services')
const baseControl = require('../basecontrol')
const schedule = require('node-schedule');
const moment = require("moment")
const io = require('socket.io-client');
const util = require('util');
const exec = util.promisify(require('child_process').exec);
const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database(config.dbSettings.dataName);

module.exports.ScheduleJob = async (socket, req) => {
  const siteCode = req.clientID
  const messageID = req.messageID //1000
  const messageType = req.messageType //request
  try {
    console.log(req);
    if (req.messageType === `request`) {
      const action = req.action
      const operation = req.action[0].operation
      let checkInsertArray = await Promise.all(action.map(async (data) => {
        let statusInsert = await baseControl.serviceBase.InsertAction(data, siteCode, messageID)
        statusInsert.messageID = messageID;
        if (data.downloadDate !== "" && operation !== "apt-purg") {
          const futureTime = moment(data.downloadDate).toDate() //Type str to Type Date
          schedule.scheduleJob(futureTime, function () {
            let downloadTime = moment().format('YYYY-MM-DD HH:mm:ss') //Time Date Now
            service.serviceUpdate.DownloadFile(downloadTime, messageID, siteCode, socket)
          })
        }
        return Promise.resolve(statusInsert)
      }));
      let checkInsert = checkInsertArray.some(element => element.status === `denied`)
      if (checkInsert === false) {
        socket.emit('response', checkInsertArray[0] )
      } else {
        socket.emit('response', checkInsertArray[0] )
      }
    } else if (req.messageType === `trigger`) {
      const actionID = req.actionID
      const command = req.command
      const dataSelectActions = await baseControl.serviceBase.SelectActionID(actionID);
      if (dataSelectActions.length !== 0) {
        if (dataSelectActions[0].operation === "apt-install") {
           exec( command , (error, stdout, stderr) => {
          //exec(`sudo apt -y  install /var/cache/apt/archives/${dataSelectActions[0].service_name}_${dataSelectActions[0].version}_amd64.deb`, (error, stdout, stderr) => {
            if (stdout.includes("Setting up")) {
              socket.emit('response', {
                clientID: siteCode,
                messageID: messageID,
                messageType: "response",
                actionID: dataSelectActions[0].action_id,
                status: `accepted`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: null
              })
              baseControl.serviceBase.data_stationCode().then((value) => {
                service.serviceUpdate.sendStatus_Installing(value[0].value, messageID, dataSelectActions[0].action_id, dataSelectActions[0].service_name, socket)
              });
              service.serviceUpdate.checkInstallService(dataSelectActions, messageID, siteCode, socket);
              return
            }else{
              if (error) {
                socket.emit('response', {
                  clientID: siteCode,
                  messageID: messageID,
                  messageType: "response",
                  actionID: dataSelectActions[0].action_id,
                  status: `denied`,
                  timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                  error: error.message
                })
                return
              }
              if (stderr) {
                socket.emit('response', {
                  clientID: siteCode,
                  messageID: messageID,
                  messageType: "response",
                  actionID: dataSelectActions[0].action_id,
                  status: `denied`,
                  timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                  error: stdout
                })
                return
              }
            }
          });
        } else if (dataSelectActions[0].operation === "apt-purge") {
          let msgSend_purge_complete = {
            "messageID": messageID,
            "clientID": siteCode,
            "clientVersion": "1.0.0",
            "messageType": "operation-report",
            "data": [
              {
                "operation": dataSelectActions[0].operation,
                "actionID": actionID,
                "serviceName": dataSelectActions[0].service_name,
                "serviceStatus": "inactive",
                "installVersion": "",//dataSelectActions[0].rollback_version,
                "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
                "rollbackStatus": "false"
              }
            ]
          };
          let msgSend_purge_uncomplete = {
            "messageID": messageID,
            "clientID": siteCode,
            "clientVersion": "1.0.0",
            "messageType": "operation-report",
            "data": [
              {
                "operation": dataSelectActions[0].operation,
                "actionID": actionID,
                "serviceName": dataSelectActions[0].service_name,
                "serviceStatus": "active",
                "installVersion": "",//dataSelectActions[0].rollback_version,
                "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
                "rollbackStatus": "false"
              }
            ]
          };

           const command = req.command
           const callPurge = function () {
            exec( command , async (error, stdout, stderr) => {
              //if (error) {
              //  socket.emit('response', {
              //    clientID: siteCode,
              //    messageID: messageID,
              //    messageType: "response",
              //    actionID: dataSelectActions[0].action_id,
              //    status: `denied`,
              //    timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              //    error: error.message
              //  })
              //}
              //if (stderr) {
              //  socket.emit('response', {
              //    clientID: siteCode,
              //   messageID: messageID,
              //    messageType: "response",
              //    actionID: dataSelectActions[0].action_id,
              //    status: `denied`,
              //    timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              //    error: stderr
              //  })
              //}
              socket.emit('response', {
                clientID: siteCode,
                messageID: messageID,
                messageType: "response",
                actionID: dataSelectActions[0].action_id,
                status: `accepted`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: null
              })
              service.serviceUpdate.sendStatus_Purging(siteCode, messageID, dataSelectActions[0].action_id, dataSelectActions[0].service_name, socket)
              responsePurge();
            });
          }

          const responsePurge = function () {
            let responeInsMain = service.serviceUpdate.checkPurgeService(dataSelectActions[0].service_name);
            responeInsMain.then((value) => {
              if (value.status_code === 0) {
                socket.emit('operation-report', msgSend_purge_complete);
                baseControl.serviceBase.UpdateStatusCode(value, dataSelectActions[0]) // update status db.sqlite
              } else {
                socket.emit('operation-report', msgSend_purge_uncomplete);
                baseControl.serviceBase.UpdateStatusCode(value, dataSelectActions[0]) // update status db.sqlite
              }
            })
          }
          callPurge(responsePurge)

        } else {
          socket.emit('response', {
            messageID: req.messageType,
            messageType: messageType,
            actionID: "",
            status: `denied`,
            timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
            error: `operation not match in case`
          });
        }
      } else {
        socket.emit('response', {
          messageID: messageID,
          messageType: messageType,
          actionID: "",
          status: `denied`,
          timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
          error: `Can not found data`
        });
      }
    } else if (req.messageType === `getAction`) {
      await baseControl.serviceBase.getAction();
    } else if (req.messageType === `deleteAction`) {
      await baseControl.serviceBase.deleteAction();
    } else if (req.messageType === `updateAction`) {
      await baseControl.serviceBase.updateAction(2,req.action[0].actionID);
    } else {
      socket.emit('response', {
        messageID: messageID,
        messageType: messageType,
        actionID: "",
        status: `denied`,
        timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
        error: `messageType not match in case`
      });
    }
  } catch (error) {
    socket.emit('response', { "status": 500, "message": error.message })
  }
}



