
const main = require('../controllers/Functions.js');
const DbControl = require('../basecontrol/BaseControl.js')
const { Authentication } = require('../untils')

module.exports = (server) => {
    server.post("/LavenderUpdate/schedule", main.ScheduleJob)
    server.get("/LavenderUpdate/getAction", DbControl.getAction)
    server.delete("/LavenderUpdate/deleteAction", DbControl.deleteAction)
} 