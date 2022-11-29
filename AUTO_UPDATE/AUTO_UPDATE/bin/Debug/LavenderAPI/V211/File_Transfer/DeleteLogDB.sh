#!/bin/bash
now_date=$(date)


#Command History
echo "INSERT INTO lavender.commands_bk SELECT * FROM lavender.pump_commands WHERE create_date < NOW() - INTERVAL '1 days';" | su - postgres -c "psql LAVENDERDB"

#Command Log
echo "DELETE FROM lavender.pump_commands WHERE pump_commands.command_id <= (SELECT max(command_id) FROM lavender.commands_bk);" | su - postgres -c "psql LAVENDERDB"

#Pump Log
echo "DELETE FROM lavender.pump_logs WHERE create_date < NOW() - INTERVAL '14 days';" | su - postgres -c "psql LAVENDERDB"

#Tank Log
echo "DELETE FROM lavender.tank_gauge_logs WHERE create_date < NOW() - INTERVAL '10 days';" | su - postgres -c "psql LAVENDERDB"

#System Log
echo "DELETE FROM lavender.system_logs WHERE create_date < NOW() - INTERVAL '10 days';" | su - postgres -c "psql LAVENDERDB"

#Clear Command History
echo "DELETE FROM lavender.commands_bk WHERE create_date < NOW() - INTERVAL '14 days';" | su - postgres -c "psql LAVENDERDB"

#Transaction History
echo "DELETE FROM lavender.transactions_bk WHERE completed_ts < NOW() - INTERVAL '6 month';" | su - postgres -c "psql LAVENDERDB"