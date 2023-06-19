const config = require('../config')
const Loginandlogoff = require('../controllers/Login_logoff');
const { authorise } = require('../untils')
console.log("Res => routes/index.js");

module.exports = (server) => {
    server.post('/Lavender/Login', Loginandlogoff.Login);
    server.post('/Lavender/Logoff', Loginandlogoff.Logoff);
}