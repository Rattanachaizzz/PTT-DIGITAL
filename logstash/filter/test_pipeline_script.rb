def register(params)
	@drop_percentage = params["percentage"]
	print ""
	print "***** ***** ***** *****"
	print "test_pipeline_script.rb : register method is called."
	print "***** ***** ***** *****"
end

def filter(event)

	return [event]
end
