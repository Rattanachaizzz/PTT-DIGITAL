const axios = require('axios')
const config = require('../config')
const moment = require('moment')
const get_site_name = config.getSite
const password = config.postLogin
const urlStatusPm2 = config.setting.urlStatusPm2
const terminalId = config.setting.terminalId

async function getStatusPm2() {
    try {
        let token = Buffer.from(`${terminalId}:${await password}`, 'utf8').toString('base64')
        let response = await axios.get(urlStatusPm2, {
            headers: {
                'Authorization': `Basic ${token}`
            },
        })
        let result = response.data
        // console.log(result);
        let inActiveApi = result.reduce(function (accumulator, name) {
            if (name.status == 'stopped') {
                accumulator[name.api_name] = name.status
            }
            return accumulator;
        }, {});
        return inActiveApi
    } catch (error) {
        console.error(error);
    }
}

exports.apiNoti = async function () {
    try {
        let payload = await getStatusPm2()
        let msg_api = {
            "site_code": await get_site_name,
            "client_version": "1.0.0",
            "msg_type": "api-noti",
            "msg_timestamp": moment().format('YYYY-MM-DD HH:mm:ss.SSS')
        }
        msg_api.data = payload
        return msg_api
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}
