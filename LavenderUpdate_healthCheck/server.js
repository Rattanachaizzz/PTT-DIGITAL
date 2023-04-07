const cors = require("cors");
const logger = require('morgan')
const service = require('./services')
const express = require("express");
const server = express();
const config = require('./config/index')
const moment = require("moment");
const io = require('socket.io-client');
const socket = io('http://10.195.2.141:6001');
const util = require('util');
const exec = util.promisify(require('child_process').exec);

server.use(cors())
server.use(logger('dev'));
server.use(express.urlencoded({ extended: true }));
server.use(express.json());

// let counter = 0;
// async function conSole(){
//     counter++
//     if (counter === 3) {
//         console.log('CONNECT SUCCESSFULL');
//         clearInterval(interval);
//         return;
//     }
//     if (counter === 6) {
//         console.log('Can not Connect to server');
//         clearInterval(interval);
//         return;
//     }
//     console.log(`${counter}`);
// }
// const interval = setInterval(conSole, 1000);

//const rollbackStatus = `Test rollback`;
//const rs =  rollbackStatus.includes('Test')
//console.log(rs)
//exec('sudo mv /etc/apt/sources.list.d/nodesource.list  /etc/apt/sources.list.d/nodesource.list.old', (error) => {
        //console.log(error);


      //});

const hw = require('./controllers/hardware')
const sv = require('./controllers/services')
const api = require('./controllers/api')
const hc = require('./controllers/healthcheck')

//setInterval(async function () {
//    let result_hw = await hw.hardwares()
//    let result_sv = await sv.services()
//    let result_api = await api.apiNoti()
//    let result_hc = await hc.hcNoti()
//    // socket.emit('clock', { currentDate: currentDate })
//    console.log('hw_payload',result_hw)
//    console.log('sv_payload',result_sv)
//    console.log('api_payload',result_api)
//    console.log('hc_payload',result_hc)
//}, 3000)


server.listen(config.serverSettings.port, () => {
    console.log(`---${config.name} Service ---`)
    console.log(`Connecting to ${config.name} repository...`)
    console.log(`Open Service By Port ${config.serverSettings.port} Success`);
    console.log(`Time Now :` + moment().format('YYYY-MM-DD HH:mm:ss'));
    service.serviceSchedule.SetScheduleJob()
    require('./routes')(server)
});


// Graceful shutdown
process.on('SIGTERM', () => {
    console.log(`Closing ${config.name} Service.`)
    server.close((err) => {
        if (err) {
            console.error(err)
            process.exit(1)
        }
        console.log('Server closed.')
    })
})

module.exports = server
