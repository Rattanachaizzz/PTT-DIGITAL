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
   msg = event.get("[message]")
   #msg = msg.tr('\\',"")
   msg = JSON.parse(msg)
   

   msg.each{|key, value| 

             if key == "_timestamp"
                event.set("["+key+"]",LogStash::Timestamp.coerce(value))
             else
 
                event.set("["+key+"]",value)
             
             end
   }
   #event.remove("[message]")
   
   event.remove("[message]")
   event.remove("[@version]")  
   return [event]
end
