const config = require("../config");
const Connectdb = config.dbSettings.pool;
const fs = require("fs");

module.exports.LavGa = async () => {
  try {

    let querySiteConfig = await Connectdb.query(`select * from lavender.site_config WHERE  ORDER BY config_id ASC`)


  } catch (err) {
    console.log(err.messaeg)
  }
}
