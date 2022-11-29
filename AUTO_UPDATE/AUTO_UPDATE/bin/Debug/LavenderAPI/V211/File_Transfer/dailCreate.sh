#!/bin/bash

echo "[$(date +"%Y-%m-%d %T")] Automatic Create Script"

rm -rf cronExecute.sh

TAR_DATE=$(date --date="2 days ago" +"%Y-%m-%d")
DELETE_DATE=$(date --date="6 month ago" +"%Y-%m-%d")
SCRIPT_FILE="/root/AutomateScript/cronExecute.sh"

#Begin Script
echo "#!/bin/bash" > ${SCRIPT_FILE}

#Command log
echo "cd /lavender/log/api/Command" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf Command_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv Command_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetGrade log
echo "cd /lavender/log/api/GetGrade" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetGrade_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetGrade_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetPos log
echo "cd /lavender/log/api/GetPos" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetPos_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetPos_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetPrice
echo "cd /lavender/log/api/GetPrice" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetPrice_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetPrice_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetRealtimevalue
echo "cd /lavender/log/api/GetRealtimeValue" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetRealtimeValue_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetRealtimeValue_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetStack
echo "cd /lavender/log/api/GetStack" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetStack_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetStack_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetTanks log
echo "cd /lavender/log/api/GetTanks" >> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetTanks_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetTanks_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#GetTotalizer
echo "cd /lavender/log/api/GetTotalizer">> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetTotalizer_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetTotalizer_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}


#GetTransaction
echo "cd /lavender/log/api/GetTransaction">> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf GetTransaction_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv GetTransaction_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#Initialize
echo "cd /lavender/log/api/Initialize">> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf Initialize_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv Initialize_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#Login_logoff
echo "cd /lavender/log/api/Login_logoff">> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf Login_logoff_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv Login_logoff_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#Setting
echo "cd /lavender/log/api/Setting">> ${SCRIPT_FILE}
echo "find -type f -name '*_${TAR_DATE}*.txt' -exec tar -zcf Setting_${TAR_DATE}.tar.gz '{}' \\+" >> ${SCRIPT_FILE}
echo "rm -rf *_${TAR_DATE}*.txt" >> ${SCRIPT_FILE}
#echo "mv Setting_${TAR_DATE}.tar.gz /lavender/log/api/" >> ${SCRIPT_FILE}
echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

#Delete tar
#echo "cd /lavender/log/api">> ${SCRIPT_FILE}
#echo "rm -rf *_${DELETE_DATE}.tar.gz" >> ${SCRIPT_FILE}
#echo "" >> ${SCRIPT_FILE}

#pm2
echo "cd /root/.pm2/logs">> ${SCRIPT_FILE}
echo "rm -rf *.log" >> ${SCRIPT_FILE}
echo "" >> ${SCRIPT_FILE}

chmod +x ${SCRIPT_FILE}

echo "[$(date +"%Y-%m-%d %T")] Generate Completed!"
