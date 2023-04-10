const bcrypt = require('bcrypt');
const { findOneAndUpdate } = require('../models/User');
const User = require('../models/User')
const jwt = require('jsonwebtoken');
const { token } = require('morgan');

exports.getUser = async (req, res) => {
    try {
        let ip = await req.header('x-forwarded-for') || req.connection.remoteAddress;
        let ip_client = ip.toString().split(":");
        resolve(ip_client[3])
        res.send(`getUser`);
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};

exports.addUser = async (req, res) => {
    try {
        res.send(`addUser`);
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};

exports.editUser = async (req, res) => {
    try {
        res.send(`editUser`);
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};

exports.deleteUser = async (req, res) => {
    try {
        res.send(`deleteUser`);
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};

exports.registerUser = async (req, res) => {
    try {
        const { username, password } = req.body;
        //checkUser
        var rsUser = await User.findOne({ username });
        if (rsUser) {
            res.send(400, 'User is already exist.')
            return;
        }
        let solt = await bcrypt.genSalt(10);
        user = new User({
            username: username,
            password: password
        })
        user.password = await bcrypt.hash(password, solt);
        await user.save()
        res.send(200, `Resgister is Success.`);
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};

exports.loginUser = async (req, res) => {
    try {
        const { username, password } = req.body;
        let user = await User.findOneAndUpdate({ username }, { new: true })
        if (user && user.enabled) {
            const isCheck = await bcrypt.compare(password, user.password)
            if (!isCheck) {
                return res.send(400, `Password Invalid!!!`)
            }

            // payload
            const payload = {
                user: {
                    username: user.username,
                    role: user.role
                }
            }

            //genarate token
            jwt.sign(payload, "jwtSecret", { expiresIn: 3600 }, (error, token) => {
                if (error) throw error
                return res.json(200, { token, payload })
            })
        } else {
            res.send(400, "Do not this user!!!")
        }
    } catch (error) {
        console.error(error);
        res.send(500, 'server error');
    }
};