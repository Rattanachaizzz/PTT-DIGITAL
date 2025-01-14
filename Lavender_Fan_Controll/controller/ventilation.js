const FTDI = require('ftdi-d2xx');
let device;
const fs = require('fs');
const config = require('../config')
const { WriteLog } = require('../service/Log_Management')
const descriptionName = config.descriptionName

async function delay(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

async function delayedFunction() {
  await delay(100);
}

module.exports.ventilation = async (socket, req) => {
  try {
    //console.log(`req : ` + req )
    ////await closeDevice()
    // Get the connected devices info list
    ////await delay(5000);
    ////const list = await FTDI.getDeviceInfoList();
    //console.log(`${list.length} device${list.length > 1 ? 's' : ''} found:`, list);    
    ////if (list.length) {
    ////for (let x in list) {
    ////if (list[x].description === descriptionName) {

    // Try to open the first device from the list
    ////device = await FTDI.openDevice(list[x].serial_number);
    //console.log(`One device open:`, device);

    // Setup the device
    ////device.setTimeouts(1000, 1000); // set the max TX and RX duration in ms
    ////device.purge(FTDI.FT_PURGE_RX); // purge the RX buffer from previous received data
    ////device.setBaudRate(9600); // baud rate (bits per second)
    ////device.setDataCharacteristics(FTDI.FT_BITS_8, FTDI.FT_STOP_BITS_1, FTDI.FT_PARITY_NONE);

    // Send data from the TXD pin of the device
    // let data = `{"status":"GET","request":"ALL"}`
    // let data = `{"status":"SET","temp":40,"fan":"high","interval":10}`

    let data = await JSON.stringify(req)
    await console.log(`Data : ` + data)
    //let data = JSON.stringify(req)
    //await console.log(`Data : ` + data)
    //const dataToWrite = Buffer.from(data)
    let dataToWrite = await Buffer.alloc(data.length, data);
    //console.log('data write is : ', dataToWrite)
    let a = await device.write(dataToWrite);
    //console.log(`Data sent successfully.`, a);

    // // Wait to receive from the RXD pin
    await delayedFunction();
    //console.log(`Trying to receive data...`);
    let response = await device.read(1024); // expected response byte length
    //console.log(`${response.byteLength} bytes were received:`, response);
    //let str = await String.fromCharCode(...response);
    //await console.log(str);
    try {
      let str = await String.fromCharCode(...response);
      await console.log(str);
      WriteLog("ventilation_", str)
      await socket.emit('response', JSON.parse(str))
      //await socket.emit('response',str)
    } catch (error) {
      //console.log("asdasds : " + error)
    }
    //await socket.emit('response',JSON.parse(str))
    //await socket.emit('response',str)
    // Close the device (device object must then be recreated using openDevice)
    ////device.close();
    ////}
    ////}
    ////}
    //await delay(5000);
    //await module.exports.temperatureNow()
  } catch (error) {
    //console.error("ERROR1 : " + error.message);
  }
}

module.exports.temperatureNow = async (socket) => {
  try {
    const list = await FTDI.getDeviceInfoList();
    if (list.length) {
      for (let x in list) {
        if (list[x].description === descriptionName) {
          device = await FTDI.openDevice(list[x].serial_number);
          device.setTimeouts(1000, 1000); // set the max TX and RX duration in ms
          device.purge(FTDI.FT_PURGE_RX); // purge the RX buffer from previous received data
          device.setBaudRate(9600); // baud rate (bits per second)
          device.setDataCharacteristics(FTDI.FT_BITS_8, FTDI.FT_STOP_BITS_1, FTDI.FT_PARITY_NONE);
          while (true) {
            let response = await device.read(1024); // expected response byte length
            let str = await String.fromCharCode(...response);
            if (str != "") {
    		if(str.includes("{") && str.includes("}")){
        	    await console.log(str);
        	    WriteLog("ventilation_", str)
        		try {
          		   await socket.emit('response', JSON.parse(str))
        		} catch (error) {
       			}
    		}
	    }
          }
        }
      }
    }
  } catch (error) {
    ///console.error(`ERROR2 : ` + error.message);
  }
}
