const config = require('../config')
const service = require('../services')
const baseControl = require('../basecontrol')
const schedule = require('node-schedule');
const moment = require("moment")
const io = require('socket.io-client');
const socket = io('http://10.195.2.141:6001');
const util = require('util');
const exec = util.promisify(require('child_process').exec);
const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database(config.dbSettings.dataName);

exports.ScheduleJob = async function (req, res, next) {
  const siteCode = req.body.siteCode
  const messageID = req.body.messageID //1000
  const messageType = req.body.messageType //Request
  const actionID = req.body.actionID
  try {
    console.log(req.body)
    if (messageType === "Request") {
      const action = req.body.action
      const operation = req.body.action[0].operation
      let checkInsertArray = await Promise.all(action.map(async (data) => {

        let statusInsert = await baseControl.serviceBase.InsertAction(data, siteCode, messageID)
        statusInsert.clientID = siteCode;
        statusInsert.messageID = messageID;

        if (data.downloadDate !== "" && operation !== "apt-purg") {
          //console.log("operation : " + operation)
          const futureTime = moment(data.downloadDate).toDate() //Type str to Type Date
          schedule.scheduleJob(futureTime, function () {
            let downloadTime = moment().format('YYYY-MM-DD HH:mm:ss') //Time Date Now
            service.serviceUpdate.DownloadFile(downloadTime, messageID, siteCode)
          })
        }
        return Promise.resolve(statusInsert)
      }));
      //console.log(checkInsertArray)
      let checkInsert = checkInsertArray.some(element => element.status === `denied`)
      if (checkInsert === false) {
        res.send(200, checkInsertArray)
        return
      } else {
        res.send(500, checkInsertArray)
        return
      }

    } else if (messageType === "Trigger") {
      const dataSelectActions = await baseControl.serviceBase.SelectActionID(actionID);
      //console.log(dataSelectActions)
      if (dataSelectActions.length !== 0) {
        if (dataSelectActions[0].operation === "apt-install") {
          await baseControl.serviceBase.data_stationCode().then((value) => {
            service.serviceUpdate.sendStatus_Installing(value[0].value, messageID, dataSelectActions[0].action_id, dataSelectActions[0].service_name)
          });
          exec(`sudo apt-get -y --allow-unauthenticated --allow-downgrades install /var/cache/apt/archives/${dataSelectActions[0].service_name}_${dataSelectActions[0].version}_amd64.deb`, (error, stdout, stderr) => {
            if (stdout.includes("Setting up")) {
              service.serviceUpdate.checkInstallService(dataSelectActions, messageID, siteCode);
              res.send(200, {
                messageID: messageID,
                messageType: "Response",
                actionID: dataSelectActions[0].action_id,
                status: `accepted`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: null
              })
              //service.serviceUpdate.checkInstallService(dataSelectActions, messageID, siteCode);
              return
            }
            if (error) {
              res.send(400, {
                messageID: messageID,
                messageType: "Response",
                actionID: dataSelectActions[0].action_id,
                status: `denied`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: error.message
              })
              return
            }
            if (stderr) {
              res.send(400, {
                messageID: messageID,
                messageType: "Response",
                actionID: dataSelectActions[0].action_id,
                status: `denied`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: stderr
              })
             return
            }
            //service.serviceUpdate.checkInstallService(dataSelectActions, messageID, siteCode);
            //service.serviceUpdate.checkInstallService(dataSelectActions)
            //res.send(200, {
              //messageID: messageID,
              //messageType: "Response",
              //actionID: dataSelectActions[0].action_id,
              //status: `accepted`,
              //timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              //error: null
            //})
          });
          //await service.serviceUpdate.checkInstallService(dataSelectActions, messageID, siteCode);
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
                "installVersion": dataSelectActions[0].rollback_version,
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
                "installVersion": dataSelectActions[0].rollback_version,
                "actionDate": moment().format('YYYY-MM-DD HH:mm:ss'),
                "rollbackStatus": "false"
              }
            ]
          };
          await baseControl.serviceBase.data_stationCode().then((value) => {
            service.serviceUpdate.sendStatus_Purging(value[0].value, messageID, dataSelectActions[0].action_id, dataSelectActions[0].service_name)
          });

          exec(`sudo apt-get -y --allow-unauthenticated purge ${dataSelectActions[0].service_name}`, async (error, stdout, stderr) => {
            if (error) {
              res.send(400, {
                messageID: messageID,
                messageType: "Response",
                actionID: dataSelectActions[0].action_id,
                status: `denied`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: error.message
              })
            }
            if (stderr) {
              res.send(400, {
                messageID: messageID,
                messageType: "Response",
                actionID: dataSelectActions[0].action_id,
                status: `denied`,
                timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
                error: stderr
              })
            }
            res.send(200, {
              messageID: messageID,
              messageType: "Response",
              actionID: dataSelectActions[0].action_id,
              status: `accepted`,
              timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              error: null
            })
          });

          let responeInsMain = service.serviceUpdate.checkPurgeService(dataSelectActions[0].service_name);
          responeInsMain.then((value) => {
            if (value.status_code === 0) {
              socket.emit('chat message', msgSend_purge_complete);
              baseControl.serviceBase.UpdateStatusCode(value, dataSelectActions[0]) // update status db.sqlite
            } else {
              socket.emit('chat message', msgSend_purge_uncomplete);
              baseControl.serviceBase.UpdateStatusCode(value, dataSelectActions[0]) // update status db.sqlite
            }
          })
        } else {
          response.send(200, `messageType not match in case`)
        }
      }
      else {
        res.send(200, "Can not found data")
      }
    } else {
      res.send(200, `messageType not match in case`)
    }
  } catch (err) {
    res.send(500, { "message": err })
    return
  }
}



