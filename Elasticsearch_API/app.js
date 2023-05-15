const elasticsearch = require('@elastic/elasticsearch');
const client = new elasticsearch.Client({ node:'http://elastic:ata123456@10.224.183.38:9200'});
const express = require('express')
const bodyParser = require('body-parser');
const basicAuth = require('express-basic-auth')
const app = express()
const request = require('request');
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(basicAuth({ users: { 'elastic': 'ata123456' }}))


//--------------------------------postgresql/transaction------------------------------//
app.get('/postgresql/transaction/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/transaction/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_transactions_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//------------------------------------postgresql/pump--------------------------------//
app.get('/postgresql/pump/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/pump/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_pump_logs_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//------------------------------postgresql/site-config-------------------------------//
app.get('/postgresql/site-config/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/site-config/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_site_config_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------------postgresql/tank-------------------------------//
app.get('/postgresql/tank/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/tank/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs , sort : { _timestamp : 'asc'}}}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_tanks_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//--------------------------------postgresql/gd-profile---------------------------//
app.get('/postgresql/gd-profile/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs , sort : { _timestamp : 'asc'}}}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/gd-profile/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_gd_profile_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-------------------------------postgresql/price-profile-------------------------//
app.get('/postgresql/price-profile/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/postgresql/price-profile/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_price_profile_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//--------------------------------beat/heartbeat-----------------------------------//
app.get('/beat/heartbeat/search',function (req, res) {
    const list_data = req.body  
    console.log(list_data) 
    const dict_test = {'must':[]}
        for (const data_index in list_data) {
          if ( data_index == 'startdate') {
             dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
          } else if( data_index == 'enddate' ){
             continue   
          } else { 
           dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
          }}     
            client.count({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
               .then(function(resp) {  
                   const size_indexs = (resp.body.count).toString()
                   console.log(size_indexs)
                   if ( size_indexs == 0 ) {
                     res.send('No data for search')
                   } else {
                      client.indices.putSettings({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                        .then(function (resp) { 
                            client.search({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                              .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                          }) 
                   }
          }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/heartbeat/delete',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for delect')
                 } else {
                    client.indices.putSettings({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { 
                                 const data_id = (resp.body.hits.hits).length
                                 console.log(data_id)
                                 for (let i = 0; i < data_id ; i++) {
                                        console.log(resp.body.hits.hits[i]._id) 
                                        client.delete({ index: 'beat_heartbeat_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                      }
                                      res.send("Delect success")
                                    })
                       }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
//-------------------------------beat/matrixbeat/cpu-----------------------------//
app.get('/beat/metricbeat/cpu/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/metricbeat/cpu/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_metricbeat_cpu_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------beat/matrixbeat/memory-----------------------------//
app.get('/beat/metricbeat/memory/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/metricbeat/memory/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_metricbeat_memory_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------beat/metricbeat/disk/-----------------------------//
app.get('/beat/metricbeat/disk/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/metricbeat/disk/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_metricbeat_disk_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//----------------------------beat/matrixbeat/service-------------------------------//
app.get('/beat/metricbeat/service/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/metricbeat/service/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_matricbeat_service_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//----------------------------beat/filebeat/dispenser-------------------------------//
app.get('/beat/filebeat/dispenser/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/filebeat/dispenser/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                     .then(function (resp) { 
                        client.search({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_filebeat_dispenser_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-------------------------------beat/filebeat/atg---------------------------------//
app.get('/beat/filebeat/atg/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('beat/filebeat/atg/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_filebeat_atg_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-------------------------------beat/filebeat/api---------------------------------//
app.get('/beat/filebeat/api/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/filebeat/api/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'beat_filebeat_api_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-------------------------------beat/filebeat/update-----------------------------//
app.get('/beat/filebeat/update/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
          .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0  ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/filebeat/update/delete',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for delect')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { 
                                 const data_id = (resp.body.hits.hits).length
                                 console.log(data_id)
                                 for (let i = 0; i < data_id ; i++) {
                                        console.log(resp.body.hits.hits[i]._id) 
                                        client.delete({ index: 'beat_filebeat_lavender_update_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                      }
                                      res.send("Delect success")
                                    })
                       }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
//-------------------------------beat/filebeat/download-----------------------------//
app.get('/beat/filebeat/download/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
          .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0  ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/beat/filebeat/download/delete',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for delect')
                 } else {
                    client.indices.putSettings({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { 
                                 const data_id = (resp.body.hits.hits).length
                                 console.log(data_id)
                                 for (let i = 0; i < data_id ; i++) {
                                        console.log(resp.body.hits.hits[i]._id) 
                                        client.delete({ index: 'beat_filebeat_lavender_download_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                      }
                                      res.send("Delect success")
                                    })
                       }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
//-----------------------------realtime-atg/inventory------------------------------//
app.get('/realtime-atg/inventory/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/realtime-atg/inventor/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_realtime_atg_inventory_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------realtime-atg/delivery------------------------------//
app.get('/realtime-atg/delivery/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/realtime-atg/delivery/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_realtime_atg_delivery_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-------------------------------realtime-atg/alarm-------------------------------//
app.get('/realtime-atg/alarm/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/realtime-atg/alarm/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_realtime_atg_alarm_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------realtime-atg/gd-realtime---------------------------//
app.get('/realtime-atg/gd-realtime/search',function (req, res) {
  const list_data = req.body  
  console.log(list_data) 
  const dict_test = {'must':[]}
      for (const data_index in list_data) {
        if ( data_index == 'startdate') {
           dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
        } else if( data_index == 'enddate' ){
           continue   
        } else { 
         dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
        }}     
          client.count({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
             .then(function(resp) {  
                 const size_indexs = (resp.body.count).toString()
                 console.log(size_indexs)
                 if ( size_indexs == 0 ) {
                   res.send('No data for search')
                 } else {
                    client.indices.putSettings({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                      .then(function (resp) { 
                          client.search({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                            .then(function (resp) { res.send(resp.body.hits.hits),console.log((resp.body.hits.hits).length)}),function(err) { res.send(err.message) }
                        }) 
                 }
        }).catch(err => { res.send('index not found exception') });
})
app.get('/realtime-atg/gd-realtim/delete',function (req, res) {
const list_data = req.body  
console.log(list_data) 
const dict_test = {'must':[]}
    for (const data_index in list_data) {
      if ( data_index == 'startdate') {
         dict_test['must'].push({ range : { _timestamp: { gte: list_data['startdate'] , lte: list_data['enddate'] } } } )
      } else if( data_index == 'enddate' ){
         continue   
      } else { 
       dict_test['must'].push({ "match" : { [data_index] : list_data[data_index] }}) 
      }}     
        client.count({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } }})
           .then(function(resp) {  
               const size_indexs = (resp.body.count).toString()
               console.log(size_indexs)
               if ( size_indexs == 0 ) {
                 res.send('No data for delect')
               } else {
                  client.indices.putSettings({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { settings: { index: { max_result_window : size_indexs }}}})
                    .then(function (resp) { 
                        client.search({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase(), body: { query : { bool : dict_test } ,size: size_indexs }}) 
                          .then(function (resp) { 
                               const data_id = (resp.body.hits.hits).length
                               console.log(data_id)
                               for (let i = 0; i < data_id ; i++) {
                                      console.log(resp.body.hits.hits[i]._id) 
                                      client.delete({ index: 'postgresql_realtime_atg_gd_realtime_'+ (list_data.lav_serial).toLowerCase() ,id: resp.body.hits.hits[i]._id })  
                                    }
                                    res.send("Delect success")
                                  })
                     }) 
               }
      }).catch(err => { res.send('index not found exception') });
})
//-----------------------------------view/indexs----------------------------------//
app.get('/view/indexs/search', (req, res) => { 
  client.cat.indices({
    h: ['index'] ,size:'10000'
  }).then(function (body) {
    res.send(body.body)
  });    
});
  app.listen(5050, () => {
})

 