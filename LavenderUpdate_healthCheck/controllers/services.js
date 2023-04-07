const config = require('../config')
const moment = require('moment')
const get_site_name = config.getSite
const systemdStatus = require('systemd-status')
const serviceName = ['lavender-dispenser', 'lavender-tankgauge', 'lavender-webconfig', 'lavender-websupport', 'lavender-monitor', 'pm2-root', 'postgresql']
// const serviceStatus = systemdStatus(serviceName)
// const serviceName = ['lavender-dispenser', 'lavender-tankgauge', 'lavender-webconfig']
// const serviceStatus =
//     [
//         {
//             name: 'lavender-dispenser',
//             isActive: false,
//             state: 'dead',
//             timestamp: '2020-06-02T13:21:51.716Z',
//             isDisabled: false
//         },
//         {
//             name: 'lavender-tankgauge',
//             isActive: false,
//             state: 'dead',
//             timestamp: null,
//             isDisabled: true
//         },
//         {
//             name: 'lavender-webconfig',
//             isActive: true,
//             state: 'exited',
//             timestamp: '2020-06-02T13:21:51.716Z',
//             isDisabled: false
//         }
//     ]

exports.services = async function () {
    try {
	let serviceStatus = systemdStatus(serviceName)
        let payload = serviceName.reduce(await function (accumulator, name, index) {
            if (serviceStatus[index].isActive == false) {
                accumulator[name] = serviceStatus[index].isActive ? "Active": "Inactive"
            }
            return accumulator;
        }, {});
        let msg_services = {
            "site_code": await get_site_name,
            "client_version": "1.0.0",
            "msg_type": "services-noti",
            "msg_timestamp": moment().format('YYYY-MM-DD HH:mm:ss.SSS')
        }
        msg_services.data = payload
        return msg_services
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}
