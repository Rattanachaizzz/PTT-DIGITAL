const path = require('path');
const Pool = require('pg').Pool

const setting = {
    urlLogin: 'http://localhost:3000/Lavender/Login',
    urlStatusPm2: 'http://localhost:4003/Lavender/GetStatusPM2',
    urlCurrentStatus: 'http://localhost:3001/Lavender/CurrentStatus-v2',
    terminalId: '7',
    dbSettings: {
      pool: new Pool({
        user: 'lav_api_app',
        host: 'localhost',
        database: 'LAVENDERDB',
        password: 'GCb7JA+W6Hg?4=Vf',
        port: 5432,
      })
    }
  }
  
  async function Site() {
    const Connectdb = setting.dbSettings.pool
    try {
      let site = await Connectdb.query(`select value from lavender.site_config where  key = 'Site_Code'`)
      let resultSite = site.rows[0].value
      //console.log(resultSite)
      return resultSite
    }
    catch (error) {
      // Handle the error
      console.error(error.message);
    }
  }
  async function Login() {
    try {
      let response = await axios.post(setting.urlLogin, qs.stringify({ 'terminal_id': `${setting.terminalId}` }))
      let resultPass = response.data.password
      //console.log(resultPass)
      return resultPass
    }
    catch (error) {
      console.error(error.message);
    }
  }
  
 const getSite = Site()
 const postLogin = Login()

module.exports = {
    name: 'Lavender API : UploadImage Group',
    version: '1.0.0',
    env: process.env.NODE_ENV || 'development',
    serverSettings: {
        port: process.env.PORT || 3500
    },dbSettings: {
        dataName : path.join(__dirname,`..`,`databasefile/mydb.sqlite`)
    },
    getSite : getSite,
    postLogin : postLogin,
    setting : setting,
    serviceName : ['lavender-dispenser', 'lavender-tankgauge', 'lavender-webconfig', 'lavender-websupport', 'lavender-monitor', 'pm2-root', 'postgresql'],
    descriptionName : 'LAV-FAN-CTRL'
}
