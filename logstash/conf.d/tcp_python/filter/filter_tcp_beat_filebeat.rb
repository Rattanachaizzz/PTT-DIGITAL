def register(params)
   print "heello ruby come in"
end


def filter(event)

  t = Time
  mytime = t.new
  great = mytime.iso8601(3)
  split_great = great.split('+', -1)
  final_great = split_great[0] + "Z"
  event.set("[server_timestamp]",  LogStash::Timestamp.coerce(final_great))


   msg = event.get("[message]")
   #msg = msg.tr('\\',"")
   msg = JSON.parse(msg)

   msg.each{|key, value|

         	if key == "_timestamp" or key == "log_timesamp" or key == "message_timestamp"
            	    event.set("["+key+"]",LogStash::Timestamp.coerce(value))
         	else
                    if key == "lav_serial"
                      event.set("[@metadata][lav_serial]",value.downcase)
                    end
            	    event.set("["+key+"]",value)
        	 
         	end
   }
   event.remove("[logsource]")
   event.remove("[program]")
   event.remove("[pid]")
   event.remove("[extra]")
   event.remove("[message]")
   event.remove("[type]")
   event.remove("[logger_name]")
   event.remove("[path]")
   event.remove("[tags]")
   event.remove("[stack_info]")
   event.remove("[port]")
   event.remove("[host]")
   event.remove("[level]")  
   event.remove("[@version]")
   event.remove("[@timestamp]")

  return [event]
end

