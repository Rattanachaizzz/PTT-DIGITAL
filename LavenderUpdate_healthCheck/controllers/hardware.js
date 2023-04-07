const os = require('os')
const checkDiskSpace = require('check-disk-space').default
const config = require('../config')
const moment = require('moment')
const get_site_name = config.getSite

async function uptime() {
    try {
        let totalNumberOfSeconds = os.uptime();
        let days = parseInt(totalNumberOfSeconds / 86400);
        let hours = parseInt((totalNumberOfSeconds - (days * 86400)) / 3600);
        let minutes = parseInt((totalNumberOfSeconds - ((hours * 3600) + (days * 86400))) / 60);
        let seconds = parseInt(totalNumberOfSeconds - ((hours * 3600) + (minutes * 60) + (days * 86400)));
        let result = (days < 10 ? "0" + days : days) + ":" + (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
        return result;
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}

async function getMemoryInfo() {
    try {
        let mem = {
            used: parseFloat(((os.totalmem() - os.freemem()) / Math.pow(1024, 3)).toFixed(2)),
            free: parseFloat((os.freemem() / Math.pow(1024, 3)).toFixed(2)),
            total: parseFloat((os.totalmem() / Math.pow(1024, 3)).toFixed(2))
        };
        mem.percent = parseFloat((mem.used * 100 / mem.total).toFixed(2));
        return mem;
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}

async function getDiskSpace() {
    try {
        let diskSpace = await checkDiskSpace('/lavender');
        let disk = {
            used: parseFloat(((diskSpace.size - diskSpace.free) / Math.pow(1024, 3)).toFixed(2)),
            free: parseFloat((diskSpace.free / Math.pow(1024, 3)).toFixed(2)),
            total: parseFloat((diskSpace.size / Math.pow(1024, 3)).toFixed(2))
        }
        disk.percent = parseFloat((disk.used * 100 / disk.total).toFixed(2));
        return disk
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}

async function generatePayload() {
    try {
        let diskSpace = await getDiskSpace();
        let mem = await getMemoryInfo()
        let cpu_n = os.cpus().length;
        // console.log(cpu_n);
        let payload = {
            "hostname": os.hostname(),
            "uptime": await uptime(),
            "cpu_percent": parseFloat((os.loadavg()[0] *100 / cpu_n).toFixed(2)),
            // "memory_used": mem.used,
            // "memory_free": mem.free,
            "mem_percent": mem.percent,
            // "disk_used": diskSpace.used,
            // "disk_free": diskSpace.free,
            "disk_percent": diskSpace.percent,
        }
        return Promise.resolve(payload);
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}

exports.hardwares = async function () {
    try {
        let payload = await generatePayload()
        let msg_hardware = {
            "site_code": await get_site_name,
            "client_version": "1.0.0",
            "msg_type": "hw-info",
            "msg_timestamp": moment().format('YYYY-MM-DD HH:mm:ss.SSS')
        }
        msg_hardware.data = payload
        return msg_hardware
    }
    catch (error) {
        // Handle the error
        console.error(error);
    }
}

