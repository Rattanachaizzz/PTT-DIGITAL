// const cors = require('cors');
const moment = require("moment");
const config = require('./config/index')
const io_client = require('socket.io-client');
const service = require('./services/ScheduleJob')
const { ScheduleJob } = require('./controller/Functions')
const jwt = require('jsonwebtoken');

  let extraHeadersObj = {};
  async function formatExtraHeaders(serviceversion) {
    serviceversion.forEach(service => {
        extraHeadersObj[service.key] = service.value;
    });
  }

async function Run() {
  const clientid = await config.getSite;
  let serviceversion = await config.getServiceVersion;
  await formatExtraHeaders(serviceversion)
  extraHeadersObj.clientid = clientid
  //const socket_client = await io_client('http://10.230.26.156:5001', { //Rit
  //const socket_client = await io_client('http://10.236.138.90:5001', { //Ethernet adapter Ethernet >>> vpn Aof
  const socket_client = await io_client('http://10.230.30.7:5001', { //Wireless LAN adapter Wi-Fi: >>> wifi Aof
    extraHeaders: extraHeadersObj
  });

  socket_client.on('connect', (msg) => {
    console.log("connect...");
  })

  socket_client.on('request', (msg) => {
    ScheduleJob(socket_client, msg);
  });

  console.log(`---${config.name} Service ---`)
  console.log(`Connecting to ${config.name} repository...`)
  console.log(`Open Service By Port ${config.serverSettings.port} Success`);
  console.log(`Time Now :` + moment().format('YYYY-MM-DD HH:mm:ss'));
  service.SetScheduleJob();
}

Run()
