const express = require('express');
const cors = require("cors");
const bodyParser = require("body-parser");
const morgan = require('morgan')
const path = require('path');
const moment = require("moment");
const {connectDB} = require('./config/db');
require('dotenv').config();

const app = express();
app.use(cors());
app.use(morgan());
app.use(bodyParser.json({ limit: '20mb' }));

connectDB();
app.use('/', require('./routes/app'));

const port = process.env.PORT;
app.listen(port, () => {
    console.log(`server started listening on port ${port}`)
})