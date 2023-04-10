// const cors = require('cors');
const moment = require("moment");
const config = require('./config/index')
const get_site_name = config.getSite;
const io_client = require('socket.io-client');
const service = require('./services/ScheduleJob')
const { ScheduleJob } = require('./controller/Functions')

async function Run() {
  const clientid = await config.getSite
  const socket_client = await io_client('http://10.230.25.113:3500', {
    extraHeaders: {
      "clientid": clientid,
      "token": "sdddf"
    }
  });
  
     socket_client.on('request', (msg) => {
      ScheduleJob(socket_client, msg);
    });

  console.log(`---${config.name} Service ---`)
  console.log(`Connecting to ${config.name} repository...`)
  console.log(`Open Service By Port ${config.serverSettings.port} Success`);
  console.log(`Time Now :` + moment().format('YYYY-MM-DD HH:mm:ss'));
  service.SetScheduleJob();
}

async function getToken() {
  token_expires = {
      "siteCode" : await config.getSite,
      "messageType": "token expires"
    }
  socket_client.emit("request",token_expires);
}

Run()
