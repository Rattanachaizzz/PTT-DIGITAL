const app = require('express')();
const http = require('http').createServer(app);
const io = require('socket.io')(http);
const config = require('./config/index')
const moment = require("moment");
const {ventilation} = require('./controller/ventilation')
const {temperatureNow} = require('./controller/ventilation')
const io_client  = require('socket.io-client');
const socket_client = io_client('http://10.230.25.113:5001'); // Wireless LAN adapter Wi-Fi: >>> wifi
//const socket_client = io_client('http://10.236.138.150:5001'); // Ethernet adapter Ethernet >>> vpn

let Device = false;
function statusStopDevice() {
  return Device;
}
exports.statusStopDevice = statusStopDevice

io.on('connection', (socket) => {
  console.log('user connected');
  socket.on('disconnect', () => {
    console.log('user disconnected');
  });
  socket.on('request', (msg) => {
    console.log(msg);
    if((JSON.stringify(msg)).includes("GET") || (JSON.stringify(msg)).includes("SET")){
      Device = true;
      ventilation(socket_client,msg);
    }else{
      console.log('ScheduleJob Strating....')
    }
  });
});

http.listen(3500, () => {
  console.log(`---${config.name} Service ---`)
  console.log(`Connecting to ${config.name} repository...`)
  console.log(`Open Service By Port ${config.serverSettings.port} Success`);
  console.log(`Time Now :` + moment().format('YYYY-MM-DD HH:mm:ss'));
  temperatureNow(socket_client);
});
