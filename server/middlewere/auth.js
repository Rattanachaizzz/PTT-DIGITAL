const { response } = require('express');
const jwt = require('jsonwebtoken');

exports.auth = (req,res,next)=>{
    try {
        
    } catch (error) {
        console.log(error)
        res.sent(401,'Token Invalid');
    }
};