require 'json'
require 'time'

def register (params)
    @drop_percentage = params["percentage"]
    print ""
    print "***** ****** ***** "
    print "consumer_sitecode_filter.rb : register method is called. "
    print "***** ***** ***** "
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

             if key == "_timestamp" or key == "message_timestamp"
                event.set("["+key+"]",LogStash::Timestamp.coerce(value))
             else
                if key == "site_code" or key == "lav_serial"
                   event.set("[@metadata]["+key+"]",value.downcase)
		else
		   event.set("["+key+"]",value)
                end
                #event.set("["+key+"]",value)

             end
   }
   
   event.remove("[@timestamp]")
   event.remove("[message]")
   event.remove("[@version]")
  return [event]
end
