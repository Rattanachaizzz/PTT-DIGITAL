
const LavenderSetting = require('../controllers/LavenderSetting.js');
const { Authentication } = require('../untils')

module.exports = (server) => {
    server.get('/Lavender/GetSerialDescription',Authentication.isAuthen,LavenderSetting.GetSerialDescription);
    server.post('/Lavender/SetSerialDescription',Authentication.isAuthen,LavenderSetting.SetSerialDescription);
} 