//const userService = require('./user.service')
const ServiceLog = require('./Log_Management.js')
const serviceUpdate = require('./ServiceUpdate.js')
const serviceSchedule = require('./ScheduleJob.js')

module.exports = Object.assign({}, { ServiceLog,serviceUpdate,serviceSchedule})