const schedule = require('node-schedule');
const baseControl =  require('../basecontrol')
const moment = require("moment");
const service = require('./ServiceUpdate.js')

module.exports.SetScheduleJob = async () => {
    let dataSelectWaitDownload = await baseControl.serviceBase.SelectWaitDownloadAll()
    if(dataSelectWaitDownload.length !== 0){
    await Promise.all(dataSelectWaitDownload.map(async (data) => {
        const futureTime = moment(data.download_date).toDate()
            schedule.scheduleJob(futureTime, function(){
                let downloadTime = moment().format('YYYY-MM-DD HH:mm:ss')
                service.DownloadFile(downloadTime)
            }) 
        }
    ))        
    }
}