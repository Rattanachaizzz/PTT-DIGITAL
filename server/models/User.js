const mongoose = require('mongoose')

const UserShema = new mongoose.Schema({
    username: String,
    password: String,
    role: { type: String, default: 'user' },
    enabled: { type: Boolean, default: false },
},
    { timestamps: true }
)
module.exports = User = mongoose.model('users', UserShema)