const config = require('../config')
const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database(config.dbSettings.dataName);
const moment = require("moment")

module.exports.InsertAction = async (data, siteCode,messageID) => {
  return await new Promise((resolve, reject) => {
    let status_code = data.operation !== "apt-purge" ? 1 : 2;
    let Description = data.operation !== "apt-purge" ? "Waiting for download" : "Waiting for trigger/execute";
    db.run(`UPDATE station_code SET value == "${siteCode}" WHERE key = "site_code"`),
      db.run(`UPDATE station_code SET value == "1.0.0" WHERE key = "version"`),
      db.run(`INSERT INTO "action_update" ("message_id","action_id", "service_name", "operation", "version", "download_date", "execute_by", "execute_date", "rollback_enable", "rollback_version", "create_date", "completed_date", "status_code", "Description") VALUES (?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
        [
          messageID, // message_id
          data.action_id, // message_id
          data.serviceName, // service_name
          data.operation, // operation
          data.version, // version
          data.downloadDate, // download_date
          data.executeBy, // execute_by
          data.executeDate, // execute_date
          data.rollbackEnable, // rollback_enable
          data.rollbackVersion, // rollback_version
          moment().format('YYYY-MM-DD HH:mm:ss'), // create_date
          null, // completed_date
          status_code, // status_code
          Description // Description
        ],
        (err) => {
          if (err) {
            resolve({
              messageID: "",
              messageType: "Response",
              actionID: data.actionID,
              status: `denied`,
              timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              error: err.message
            });
          } else {
            resolve({
              messageID: "",
              messageType: "Response",
              actionID: data.actionID,
              status: `accepted`,
              timeStamp: moment().format('YYYY-MM-DD HH:mm:ss'),
              error: null
            });
          }
        }
      );
  })
}

module.exports.SelectWaitDownloadOntime = async (downloadTime) => {
  return await new Promise((resolve, reject) => {
    db.all(`SELECT * FROM action_update WHERE download_date = "${downloadTime}" AND status_code = 1`, function (err, rows) {
      if (err) {
        console.log(err)
        resolve(err)
      } else {
        resolve(rows)
      }
    });
  })
}

module.exports.SelectWaitDownloadAll = async () => {
  return await new Promise((resolve, reject) => {
    db.all(`SELECT * FROM action_update WHERE status_code = 1`, function (err, rows) {
      if (err) {
        console.log(err)
        resolve(err)
      } else {
        resolve(rows)
      }
    });
  })
}

module.exports.UpdateStatusCode = async (respone, data) => {
  return await new Promise((resolve, reject) => {
    //console.log("UpdateStatusCode")
    //console.log(responeDownloadMain.status_code)
    //console.log(responeDownloadMain.Description)
    //console.log(data.action_id)
    db.all(`UPDATE action_update SET status_code = ${respone.status_code}, Description = "${respone.Description}" WHERE action_id = ${data.action_id}`, function (err, rows) {
      if (err) {
        console.log(err)
        resolve(err)
      } else {
        resolve(rows)
      }
    });
  })
}

module.exports.SelectActionID = async (actionID) => {
  return await new Promise((resolve, reject) => {
    db.all(`SELECT * FROM action_update WHERE action_id = ${actionID} AND status_code = 2`, function (err, rows) {
      if (err) {
        resolve(err)
      } else {
        resolve(rows)
      }
    });
  })
}

module.exports.data_stationCode = async () => {
  return await new Promise((resolve, reject) => {
    db.all(`SELECT * FROM station_code`, function (err, rows) {
      if (err) {
        resolve(err)
      } else {
        resolve(rows)
      }
    });
  })
}

module.exports.getAction = async function (req, res, next) {
  return await new Promise((resolve, reject) => {
    db.all(`SELECT * FROM action_update`, function (err, rows) {
      if (err) {
        res.send(404, err);
      } else {
        res.send(200, rows);
      }
    });
  })
}

module.exports.deleteAction = async function (req, res, next) {
  db.all(`DELETE FROM action_update`, function (err, rows) {
    if (err) {
      res.send(404, err);
    } else {
      res.send(200, "Delete action Success!");
    }
  })
}
