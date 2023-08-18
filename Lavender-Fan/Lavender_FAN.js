let device;
const util = require('util');
const FTDI = require('ftdi-d2xx');
const exec = util.promisify(require('child_process').exec);

async function main() {
      await fileCheck();
      await serviceStop('lavender-update');
      let Result = false;
      const deviceList = await FTDI.getDeviceInfoList();
      deviceList.forEach(list => {
            if (list.description == 'LAV-FAN-CTRL') {
                  Result = true;
                  connectPort(list.serial_number);
            } 
      });

      if(Result == false){
            console.log('fail');
            description_NoneFound();
      }
}

async function serviceStart(serviceName) {
      await exec(`systemctl start ${serviceName}`)
}

async function dateGet() {
      let date = ((await exec(`date`)).stdout).replace('\n','');
      return date;
}

async function timeStamp() {
      let date = await dateGet();
      let hostname = await hostnameGet();
      await exec(`echo '#TimeStamp' >>/root/${hostname}-FAN.txt`)
      await exec(`echo '  Time             : ${date}' >>/root/${hostname}-FAN.txt`);

      console.log('#TimeStamp');
      console.log(`  Time             : ${date}`);
}

async function serviceStop(serviceName) {
      await exec(`systemctl stop ${serviceName}`)
}

async function description_NoneFound() {
      let hostname = await hostnameGet();
      //tempNow
      await exec(`echo '------------------------- Fan Controller -------------------------' >>/root/${hostname}-FAN.txt`)
      await exec(`echo '#Temperature Now' >>/root/${hostname}-FAN.txt`)
      await exec(`echo '  Massage Response : Unknows' >>/root/${hostname}-FAN.txt`);
      await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)
      console.log('------------------------- Fan Controller -------------------------');
      console.log('#Temperature Now');
      console.log(`  Massage Response : Unknows`);
      console.log(`  Result           : Fail`);

      //configGet
      await exec(`echo '#Temperature Config(GET)' >>/root/${hostname}-FAN.txt`)
      await exec(`echo '  Massage Response : Unknows' >>/root/${hostname}-FAN.txt`);
      await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)
      console.log('#Temperature Config(GET)');
      console.log(`  Massage Response : Unknows`);
      console.log(`  Result           : Fail`);

      //configSet
      await exec(`echo '#Temperature Config(SET)' >>/root/${hostname}-FAN.txt`)
      await exec(`echo '  Massage Response : Unknows' >>/root/${hostname}-FAN.txt`);
      await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)
      console.log('#Temperature Config(SET)');
      console.log(`  Massage Response : Unknows`);
      console.log(`  Result           : Fail`);

      //timeStamp
      await timeStamp();

      //start service
      await serviceStart('lavender-update');
}

async function hostnameGet() {
      let hostName = (((await exec(`hostnamectl | grep hostname`)).stdout).split(':')[1].replace(' ', '')).replace('\n', '')
      return hostName;
}

async function connectPort(serial_number) {
      device = await FTDI.openDevice(serial_number);
      device.setTimeouts(1000, 1000); // set the max TX and RX duration in ms
      device.purge(FTDI.FT_PURGE_RX); // purge the RX buffer from previous received data
      device.setBaudRate(9600); // baud rate (bits per second)
      device.setDataCharacteristics(FTDI.FT_BITS_8, FTDI.FT_STOP_BITS_1, FTDI.FT_PARITY_NONE);
      await processCheck();
}

async function fileCheck() {
      exec(`ls /root/`, async (error, stdout, stderr) => {
            let hostname = await hostnameGet();
            if (stdout.includes(`${hostname}-FAN.txt`)) {
                  exec(`rm -r /root/${hostname}-FAN.txt`)
            }
      })

}

async function processCheck() {
      await tempNow();
      await configGet();
      await configSet();
      await timeStamp();
      await serviceStart('lavender-update');
}

async function tempNow() {
      let hostname = await hostnameGet();
      while (true) {
            let count = 0;
            count++;
            let response = await device.read(1024); // expected response byte length
            let str = await String.fromCharCode(...response);
            if (str.includes('{') && str.includes('{')) {
                  await exec(`echo '------------------------- Fan Controller -------------------------' >>/root/${hostname}-FAN.txt`)
                  await exec(`echo '#Temperature Now' >>/root/${hostname}-FAN.txt`)
                  await exec(`echo '  Massage Response : ${str}' >>/root/${hostname}-FAN.txt`);
                  await exec(`echo '  Result           : Pass' >>/root/${hostname}-FAN.txt`)

                  console.log('------------------------- Fan Controller -------------------------');
                  console.log('#Temperature Now');
                  console.log(`  Massage Response : ${str}`);
                  console.log(`  Result           : Pass`);
                  return;
            }
            if (count == 20) {
                  await exec(`echo '------------------------- Fan Controller -------------------------' >>/root/${hostname}-FAN.txt`)
                  await exec(`echo '#Temperature Now' >>/root/${hostname}-FAN.txt`)
                  await exec(`echo '  Massage Response : ${str}' >>/root/${hostname}-FAN.txt`);
                  await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)

                  console.log('------------------------- Fan Controller -------------------------');
                  console.log('#Temperature Now');
                  console.log(`  Massage Response : ${str}`);
                  console.log(`  Result           : Fail`);
                  return;
            }
      }
}

async function configGet() {
      let messageReq = {
            "status": "GET",
            "request": "ALL"
      };
      let hostname = await hostnameGet();
      let dataReq = await JSON.stringify(messageReq)
      let dataWrite = await Buffer.alloc(dataReq.length, dataReq);
      let a = await device.write(dataWrite);
      let response = await device.read(1024);
      response = await String.fromCharCode(...response);
      if (response) {
            await exec(`echo '#Temperature Config(GET)' >>/root/${hostname}-FAN.txt`)
            await exec(`echo '  Massage Response : ${response}' >>/root/${hostname}-FAN.txt`);
            await exec(`echo '  Result           : Pass' >>/root/${hostname}-FAN.txt`)

            console.log('#Temperature Config(GET)');
            console.log(`  Massage Response : ${response}`);
            console.log(`  Result           : Pass`);
            return;
      } else {
            await exec(`echo '#Temperature Config(GET)' >>/root/${hostname}-FAN.txt`)
            await exec(`echo '  Massage Response : ${response}' >>/root/${hostname}-FAN.txt`);
            await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)

            console.log('#Temperature Config(GET)');
            console.log(`  Massage Response : ${response}`);
            console.log(`  Result           : Fail`);
            return;
      }
}

async function configSet() {
      let messageReq = {
            "status": "SET",
            "temp1": 25,
            "temp2": 32,
            "interval": 5,
            "pwm_low": 12,
            "pwm_high": 100
      };
      let hostname = await hostnameGet();
      let dataReq = await JSON.stringify(messageReq)
      let dataWrite = await Buffer.alloc(dataReq.length, dataReq);
      let a = await device.write(dataWrite);
      let response = await device.read(1024);
      response = await String.fromCharCode(...response);
      if (response.includes('SUCCESS')) {
            await exec(`echo '#Temperature Config(SET)' >>/root/${hostname}-FAN.txt`)
            await exec(`echo '  Massage Response : ${response}' >>/root/${hostname}-FAN.txt`);
            await exec(`echo '  Result           : Pass' >>/root/${hostname}-FAN.txt`)

            console.log('#Temperature Config(SET)');
            console.log(`  Massage Response : ${response}`);
            console.log(`  Result           : Pass`);
            return;
      } else {
            await exec(`echo '#Temperature Config(SET)' >>/root/${hostname}-FAN.txt`)
            await exec(`echo '  Massage Response : ${response}' >>/root/${hostname}-FAN.txt`);
            await exec(`echo '  Result           : Fail' >>/root/${hostname}-FAN.txt`)

            console.log('#Temperature Config(SET)');
            console.log(`  Massage Response : ${response}`);
            console.log(`  Result           : Fail`);
            return;
      }

}

main();
