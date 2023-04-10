const express = require('express');
const route = express.Router();
const {getUser,addUser,editUser,deleteUser,registerUser,loginUser} = require('../controllers/auth');

route.get('/get', getUser);
route.post('/add', addUser);
route.put('/edit', editUser);
route.delete('/deleter', deleteUser);
route.post('/register', registerUser);
route.post('/login', loginUser);

module.exports = route;