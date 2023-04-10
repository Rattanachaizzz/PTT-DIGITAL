const mongoose = require('mongoose');
require('dotenv').config();

module.exports.connectDB = async () => {
    try {
        await mongoose.connect(process.env.DATABASE)
        console.log("Connect to DB");
    } catch (error) {
        console.log(error);
        process.exit(1);
    }
};