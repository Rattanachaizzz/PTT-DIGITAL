const Pool = require('pg').Pool
console.log("Res => config/index.js");
module.exports = {
    name: 'Lavender API : Access Group',
    version: '3.0.0',
    env: process.env.NODE_ENV || 'development',
    serverSettings: {
        port: process.env.PORT || 3000
    },
    dbSettings: {
         pool : new Pool({
            user: 'lav_api_app',
            host: '10.195.2.64',
            database: 'LAVENDERDB',
            password: 'GCb7JA+W6Hg?4=Vf',
            port: 5432,
          })
    },
    tokenSettings: {
        publicKey: process.env.PUBLIC_KEY || 'cat4dog',
        accessTokenExpiry: 60 * 60 * 24 * 14, // 2 weeks.
        privateKey: process.env.PRIVATE_KEY || 'hermes4digital',
        refreshTokenExpiry: 60 * 60 * 24 * 14, // 2 weeks.
    },

    log_path:"/lavender/log/api/Login_logoff"
}