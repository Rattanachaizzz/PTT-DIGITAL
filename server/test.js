require('dotenv').config();
const bcrypt = require('bcrypt');
const { token } = require('morgan');


async function Run(){
    let solt = await bcrypt.genSalt(10);
    //await console.log(solt);
    let msgReq = {
        "siteCode" : "I201",
        "messageID": 1000,
        "messageType": "Request",
        "action": [
            {
                "actionID": 1,
                "serviceName": "lavender-dispenser",
                "operation": "apt-install",
                "version": "1.10.6",
                "downloadDate": "2023-03-16 15:26:00",
                "executeBy": "server-trigger",
                "executeDate": "",
                "rollbackEnable": true,
                "rollbackVersion": "1.10.4"
            }
        ]
    };
    let msgDecrypt = await bcrypt.hash(JSON.stringify(msgReq), solt);
    await console.log(msgDecrypt);
    // const isCheck = await bcrypt.compare(msgDecrypt, msgDecrypt)
    // console.log(JSON.parse(msgDecrypt))
    // const isCheck = await bcrypt.compare(clientID, msgDecrypt)
    // await console.log(isCheck);
}

Run()

// function find_max(nums) {
//     let max_num = Number.NEGATIVE_INFINITY; // smaller than all other numbers
//     for (let num of nums) {
//         if (num > max_num) {
//             // (Fill in the missing line here)
//              max_num += num;
//         }
//     }
//     return max_num;
// }

// let nums = [1, 2, 3, 4, 5]
// let rs = find_max(nums)
// console.log(rs);