const axios = require('axios')
const config = require('../config')
const moment = require('moment')
const get_site_name = config.getSite
const password = config.postLogin
const urlCurrentStatus = config.setting.urlCurrentStatus
const terminalId = config.setting.terminalId

async function getCurrentStatus() {
    try {
        let token = Buffer.from(`${terminalId}:${await password}`, 'utf8').toString('base64')
        let response = await axios.get(urlCurrentStatus, {
            headers: {
                'Authorization': `Basic ${token}`
            },
        })
        let result = response.data
        // console.log(result);
        let inActiveDis = result.reduce(function (accumulator, name) {
            if (name.status == 'Unknown') {
                accumulator[name.pump_id] = name.status
            }
            return accumulator;
        }, {});
        return inActiveDis
    } catch (error) {
        console.error(error);
    }
}

exports.hcNoti = async function () {
    try {
        let payload = await getCurrentStatus()
        let msg_hc = {
            "site_code": await get_site_name,
            "client_version": "1.0.0",
            "msg_type": "health-noti",
            "msg_timestamp": moment().format('YYYY-MM-DD HH:mm:ss.SSS')
        }
        msg_hc.data = payload
        return msg_hc
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}
