using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Update_DispenserV1106_APIV300
{
    public class Program
    {
        static string result_update_dispenser = "No-Update";
        static string result_update_API = "No-Update";
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;

            string Sevice_version_old_Dispenser;
            string Sevice_version_new_Dispenser;
            string Sevice_status_Dispenser;
            string Sevice_version_old_API;
            string Sevice_version_new_API;
            string Sevice_status_API;
            string pos_shift = "";
            string pos_status = "";
            int pumps_Status_Temp_Stopped = 0;
            int pumps_Status_Unknown = 0;
            int pumps_Count = 0;
            //local_Transfer_file
            string localPath_Lavender = Directory.GetCurrentDirectory() + @"\LavenderTransfer\File_Transfer";
            //local_MD5_file
            string localPath_MD5_Lavender = Directory.GetCurrentDirectory() + @"\LavenderTransfer\File_Transfer";
            //localPath_Logs_Complete
            string localPath_Logs_Complete = Directory.GetCurrentDirectory() + @"\LavenderTransfer\Logs\UpdateComplete.txt";
            //localPath_Logs_False
            string localPath_Logs_False = Directory.GetCurrentDirectory() + @"\LavenderTransfer\Logs\UpdateFalse.txt";

            Console.WriteLine("Program Update DispenserV1.10.6 && APIV.3.0.0");
            saveLogs("Program Update DispenserV1.10.6 && APIV.3.0.0");
            Console.WriteLine($"{dateTime} : Program is Process Start");
            saveLogs($"{dateTime} : Program is Process Start");

            try
            {
                if (File.Exists(localPath_Logs_Complete))
                {
                    File.Delete(localPath_Logs_Complete);
                }
                if (File.Exists(localPath_Logs_False))
                {
                    File.Delete(localPath_Logs_False);
                }

                var CurrentDirectory = Directory.GetCurrentDirectory();
                StreamReader r = new StreamReader(CurrentDirectory + @"\ConnectionString.json");
                string jsonString = r.ReadToEnd();
                JObject rss = JObject.Parse(jsonString);
                string IP_Address = (string)rss["ConnectionStrings"]["ConnectionString"];
                var Services = ((string)rss["ConnectionStrings"]["Service"]).Replace(" ", "").Split(',');
                string username = "lavender";
                string password = "muj,nv,ufvdw,h";
                int port = 22;
                char dubleCode = '"';

                SshClient client_rm = new SshClient(IP_Address, port, username, password);
                client_rm.Connect();
                if (client_rm.IsConnected)
                {
                    //Delect File PackageUpdate
                    var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r PackageUpdate*");
                    var result = command.Execute();

                    //Delect File Dispenser
                    command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderDispenser*");
                    result = command.Execute();

                    //Delect File API
                    command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI*");
                    command.Execute();

                    //Delect File Dispenser
                    command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderSetting*");
                    result = command.Execute();

                    //Get Pump Count
                    command = client_rm.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT COUNT(pump_id) FROM lavender.pumps" + dubleCode);
                    pumps_Count = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));

                    //Get pos_shift
                    command = client_rm.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT status FROM lavender.pos_shift WHERE shift_id =(SELECT max(shift_id) FROM lavender.pos_shift);" + dubleCode);
                    result = command.Execute();
                    pos_shift = command.Execute().Split('\n')[2].Replace(" ", "");

                    // Get pos_status
                    command = client_rm.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT status FROM lavender.pos_status WHERE history_id =(SELECT max(history_id) FROM lavender.pos_status);" + dubleCode);
                    pos_status = command.Execute().Split('\n')[2].Replace(" ", "");

                    //Get Status Pump_Real_Times[Temp Stop] && [Unknown]
                    command = client_rm.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT status FROM lavender.pumps_real_time ORDER BY pump_id ASC" + dubleCode);
                    var Detail_Pump = command.Execute().Split('\n');
                    foreach (var pump in Detail_Pump)
                    {
                        if (pump.Contains("Temp Stopped"))
                        {
                            pumps_Status_Temp_Stopped++;
                        }
                        else if (pump.Contains("Unknown"))
                        {
                            pumps_Status_Unknown++;
                        }
                    }
                }
                client_rm.Dispose();

                if (pos_shift == "0" || pos_status == "0" || pumps_Status_Temp_Stopped == pumps_Count || pumps_Status_Unknown == pumps_Count)
                {
                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\LavenderTransfer\File_Transfer", "*");
                    string name_1 = "";
                    foreach (var file in files)
                    {
                        var fileStream = new FileStream(file, FileMode.Open);
                        if (fileStream != null)
                        {
                            name_1 = file.Split('\\')[file.Split('\\').Length - 1];
                        }
                        fileStream.Close();
                    }
                    //var Result = "False";
                    var Result = Upload(IP_Address, port, username, password, localPath_Lavender, localPath_MD5_Lavender, name_1);
                    if (Result == "True")
                    {
                        SshClient client = new SshClient(IP_Address, port, username, password);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            //Extract File
                            var command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S tar -xzf /home/lavender/PackageUpdate.tar.gz");
                            var result = command.Execute();
                            Thread.Sleep(3000);

                            //Get Version Old
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Dispenser_Version'" + dubleCode);
                            result = command.Execute();
                            Sevice_version_old_Dispenser = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                            //Get Version Old
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'API_Version'" + dubleCode);
                            result = command.Execute();
                            Sevice_version_old_API = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                            foreach (var Service in Services)
                            {
                                if (Service.ToString() == "Dispenser")
                                {
                                    if (Sevice_version_old_Dispenser != "1.10.6")
                                    {
                                        //Stop Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-dispenser");
                                        command.Execute();

                                        //Delete File
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderDispenser_*");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderDispenser.tar.gz");
                                        result = command.Execute();

                                        //Get Version Old
                                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Dispenser_Version'" + dubleCode);
                                        result = command.Execute();
                                        Sevice_version_old_Dispenser = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderDispenser /usr/share/LavenderDispenser_BK_V{Sevice_version_old_Dispenser.Replace(".", "")}");
                                        command.Execute();

                                        //Add tb.config_fleet_fraud
                                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"CREATE TABLE lavender.config_fleet_fraud  (pump_id integer NOT NULL,counting integer NOT NULL,time_waiting integer NOT NULL,duration integer NOT NULL,update_time timestamp without time zone NOT NULL,PRIMARY KEY (pump_id))" + dubleCode);
                                        result = command.Execute();
                                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.config_fleet_fraud TO lav_application_role" + dubleCode);
                                        result = command.Execute();

                                        //Unzip File 
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/PackageUpdate/LavenderDispenser.tar.gz -C /usr/share/");
                                        command.Execute();

                                        //Start Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-dispenser");
                                        command.Execute();

                                        Thread.Sleep(5000);

                                        //Get Version New
                                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Dispenser_Version'" + dubleCode);
                                        result = command.Execute();
                                        Sevice_version_new_Dispenser = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                                        //Status Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-dispenser");
                                        Sevice_status_Dispenser = command.Execute().Split('\n')[1].Split('(')[0].Split(':')[1].Replace(" ", "");

                                        Thread.Sleep(5000);

                                        //Condition Check Update
                                        if (Sevice_version_old_Dispenser != Sevice_version_new_Dispenser && Sevice_status_Dispenser == "loaded")
                                        {
                                            result_update_dispenser = "True";
                                            Console.WriteLine($"{dateTime} : Service_name {Service.ToString()} Update Success.");
                                            saveLogs($"{dateTime} : Service_name {Service.ToString()} Update Success.");
                                        }
                                        else
                                        {
                                            result_update_dispenser = "False";
                                            Console.WriteLine($"{dateTime} : Service_name {Service.ToString()} Update Unsuccess.");
                                            saveLogs($"{dateTime} : Service_name {Service.ToString()} Update Unsuccess.");

                                            //Rollback
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderDispenser");
                                            command.Execute();
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderDispenser_BK_V{Sevice_version_old_Dispenser.Replace(".", "")} /usr/share/LavenderDispenser");
                                            command.Execute();

                                            //Start Service
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-dispenser");
                                            command.Execute();
                                        }

                                        //Add Command Temp Stop
                                        for (int i = 1; i <= pumps_Count; i++)
                                        {
                                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.pump_commands( command, pump_id, create_date) VALUES ('stop', {i},'2015-12-25 15:32:06.427');" + dubleCode);
                                            result = command.Execute();
                                        }
                                    }
                                }
                                else if (Service.ToString() == "API")
                                {
                                    if (Sevice_version_old_API != "3.0.0")
                                    {
                                        //Stop Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 stop all");
                                        result = command.Execute();

                                        //Delete File
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderAPI_*");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderAPI.tar.gz");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderSetting_*");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderSetting.tar.gz");
                                        result = command.Execute();

                                        //Delete File bash
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /root/AutomateScript/dailCreate.sh");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /root/AutomateScript/DeleteLogDB.sh");
                                        result = command.Execute();

                                        //Chmod File bash
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S chmod 777 PackageUpdate/dailCreate.sh");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S chmod 777 PackageUpdate/DeleteLogDB.sh");
                                        result = command.Execute();

                                        //Move File bash
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/PackageUpdate/dailCreate.sh /root/AutomateScript/");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/PackageUpdate/DeleteLogDB.sh /root/AutomateScript/");
                                        result = command.Execute();

                                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderAPI /usr/share/LavenderAPI_BK_V{Sevice_version_old_API.Replace(".", "")}");
                                        command.Execute();

                                        //Unzip File 
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/PackageUpdate/LavenderAPI.tar.gz -C /usr/share/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/Command/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetGrade/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetPos/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetPrice/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetRealtimeValue/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetStack/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetTanks/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetTotalizer/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/GetTransaction/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/Initialize/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/LavenderSetting/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/Login_Logout/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/ManageAPI/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/Setting/");
                                        command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /usr/share/LavenderAPI/Command/node_modules.tar.gz -C /usr/share/LavenderAPI/Support/");
                                        command.Execute();

                                        //<-------------------------------------------------------------------------------------------------------------------------------------------------------------->
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete LavenderSetting");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 start /usr/share/LavenderAPI/LavenderSetting/server.js --name LavenderSetting");
                                        result = command.Execute();

                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete ManageAPI");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 start /usr/share/LavenderAPI/ManageAPI/server.js --name ManageAPI");
                                        result = command.Execute();

                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete Support");
                                        result = command.Execute();
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 start /usr/share/LavenderAPI/Support/server.js --name Support");
                                        result = command.Execute();

                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 save");
                                        result = command.Execute();

                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 startup");
                                        result = command.Execute();

                                        //<-------------------------------------------------------------------------------------------------------------------------------------------------------------->

                                        //Delete File Transfer
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderAPI/Command/node_modules.tar.gz");
                                        command.Execute();

                                        //BackUp lavenderSetting >>> lavenderSetting_BK_V[Version Old]
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderSetting /usr/share/LavenderSetting_BK_V100");
                                        command.Execute();

                                        //Unzip File 
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/PackageUpdate/LavenderSetting.tar.gz -C /usr/share/");
                                        command.Execute();

                                        //Start Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 restart all");
                                        command.Execute();

                                        Thread.Sleep(10000);

                                        //Get Version New
                                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'API_Version'" + dubleCode);
                                        result = command.Execute();
                                        Sevice_version_new_API = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                                        //Status Service
                                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status pm2-root.service");
                                        result = command.Execute();
                                        Sevice_status_API = result.Split('\n')[1].Split('(')[0].Split(':')[1].Replace(" ", "");

                                        //Condition Check Update
                                        if (Sevice_version_old_API != Sevice_version_new_API && Sevice_status_API == "loaded" && Sevice_status_API == "loaded")
                                        {
                                            result_update_API = "True";
                                            Console.WriteLine($"{dateTime} : Service_name {Service.ToString()} Update Success.");
                                            saveLogs($"{dateTime} : Service_name {Service.ToString()} Update Success.");
                                        }
                                        else
                                        {
                                            result_update_API = "False";
                                            Console.WriteLine($"{dateTime} : Service_name {Service.ToString()} Update Unsuccess.");
                                            saveLogs($"{dateTime} : Service_name {Service.ToString()} Update Unsuccess.");

                                            //Rollback
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderAPI");
                                            command.Execute();
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderAPI_BK_V{Sevice_version_old_API.Replace(".", "")} /usr/share/LavenderAPI");
                                            command.Execute();

                                            //Start Service
                                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 restart all");
                                            command.Execute();
                                        }
                                    }
                                }
                                else
                                {
                                    File.AppendAllText(localPath_Logs_False, $"Lavender Update False {dateTime}");
                                    Console.Write($"{dateTime} : Service_name {Service.ToString()} not macth in case.");
                                    saveLogs($"{dateTime} : Service_name {Service.ToString()} not macth in case.");
                                    Console.WriteLine($"{dateTime} : Program Status >>> Upload Unsuccess.");
                                    saveLogs($"{dateTime} : Program Status >>> Upload Unsuccess.");
                                    Thread.Sleep(3000);
                                    Console.WriteLine($"{dateTime} : Program Status >>> Update Unsuccess.");
                                    saveLogs($"{dateTime} : Program Status >>> Update Unsuccess.");
                                    Thread.Sleep(3000);
                                }

                                //<---------------------------------------------------------------------------------------------------Tank--------------------------------------------------------------------------------------------------------->
                                //Add column loop_id
                                /*command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT COUNT(loop_id) FROM lavender.loops where loop_type = 2;" + dubleCode);
                                var Atg_Count = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));
                                if (Atg_Count == 1)
                                {
                                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT loop_id FROM lavender.loops where loop_type = 2;" + dubleCode);
                                    var loop_id = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));

                                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"ALTER TABLE lavender.tanks ADD COLUMN loop_id int DEFAULT {loop_id}" + dubleCode);
                                    result = command.Execute();
                                }
                                else
                                {
                                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"ALTER TABLE lavender.tanks ADD COLUMN loop_id int DEFAULT 1" + dubleCode);
                                    result = command.Execute();
                                }*/
                                //< -----------------------------------------------------------------------------------------------Web_Config--------------------------------------------------------------------------------------------------------->
                                //Create tb.lavender_api
                                /*string msg = "";
                                msg = "CREATE TABLE IF NOT EXISTS lavender.lavender_api ( api_id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( CYCLE INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),  api_name text NOT NULL, PRIMARY KEY(api_id));";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "ALTER TABLE IF EXISTS lavender.lavender_api OWNER to postgres";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE lavender.lavender_api TO lav_application_role";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "GRANT ALL ON TABLE lavender.lavender_api TO postgres";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.serial_number_card TO lav_application_role" + dubleCode);
                                result = command.Execute();

                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Command');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetGrade');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetPos');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetPrice');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetRealtimeValue');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetStack');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTanks');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTotalizer');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTransaction');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Initialize');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('LavenderSetting');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Login_Logout');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('ManageAPI')" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Setting');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Support');" + dubleCode);

                                //< -----------------------------------------------------------------------------------------------Web_Support--------------------------------------------------------------------------------------------------------->
                                //Add Row Version
                                command = client.CreateCommand($"PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.site_config(config_id, key, value, type) VALUES (49,'WebSupport_Version','null','string')" + dubleCode);
                                command.Execute();*/
                            }

                            //Add column loop_id
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT COUNT(loop_id) FROM lavender.loops where loop_type = 2;" + dubleCode);
                            var Atg_Count = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));
                            if (Atg_Count == 1)
                            {
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT loop_id FROM lavender.loops where loop_type = 2;" + dubleCode);
                                var loop_id = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));

                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"ALTER TABLE lavender.tanks ADD COLUMN loop_id int DEFAULT {loop_id}" + dubleCode);
                                result = command.Execute();
                            }
                            else
                            {
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"ALTER TABLE lavender.tanks ADD COLUMN loop_id int DEFAULT 1" + dubleCode);
                                result = command.Execute();
                            }
                            //< -----------------------------------------------------------------------------------------------Web_Config--------------------------------------------------------------------------------------------------------->
                            //Create tb.lavender_api
                            string msg = "";
                            msg = "SELECT * FROM lavender.lavender_api";
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                            result = command.Execute();
                            if (result == "")
                            {
                                msg = "CREATE TABLE IF NOT EXISTS lavender.lavender_api ( api_id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( CYCLE INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),  api_name text NOT NULL, PRIMARY KEY(api_id));";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "ALTER TABLE IF EXISTS lavender.lavender_api OWNER to postgres";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE lavender.lavender_api TO lav_application_role";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                                result = command.Execute();
                                msg = "GRANT ALL ON TABLE lavender.lavender_api TO postgres";
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.serial_number_card TO lav_application_role" + dubleCode);
                                result = command.Execute();

                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Command');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetGrade');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetPos');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetPrice');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetRealtimeValue');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetStack');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTanks');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTotalizer');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('GetTransaction');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Initialize');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('LavenderSetting');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Login_Logout');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('ManageAPI')" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Setting');" + dubleCode);
                                result = command.Execute();
                                command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.lavender_api(api_name) VALUES ('Support');" + dubleCode);
                            }
                            //< -----------------------------------------------------------------------------------------------Web_Support--------------------------------------------------------------------------------------------------------->
                            //Add Row Version
                            command = client.CreateCommand($"PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.site_config(config_id, key, value, type) VALUES (49,'WebSupport_Version','null','string')" + dubleCode);
                            command.Execute();

                            if (result_update_dispenser == "No-Update" && result_update_API == "No-Update")
                            {
                                Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service Dispenser And API Version is New version already !!!.");
                                saveLogs($"{dateTime} : Program Status >>> Lavender Service Dispenser And API Version is New version already !!!.");
                                Thread.Sleep(1500);
                                File.AppendAllText(localPath_Logs_Complete, $"Lavender Service Dispenser And API Version is New version already !!! {dateTime}");
                            }
                            else if (result_update_dispenser == "No-Update" && result_update_API != "No-Update")
                            {
                                if (result_update_API == "True")
                                {
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service Dispenser New version already !!!");
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service API Update Success.");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service Dispenser New version already !!!");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service API Update Success.");
                                    Thread.Sleep(1500);
                                    File.AppendAllText(localPath_Logs_Complete, $"Lavender Service Dispenser New version already !!! {dateTime} \nLavender Service API Update Success. {dateTime}");
                                }
                                else
                                {
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service Dispenser New version already !!!");
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service API Update Unsuccess.");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service Dispenser New version already !!!");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service API Update Unsuccess.");
                                    Thread.Sleep(1500);
                                    File.AppendAllText(localPath_Logs_Complete, $"Lavender Service Dispenser New version already !!! {dateTime} \nLavender Service API Update Unsuccess. {dateTime}");
                                }
                            }
                            else if (result_update_dispenser != "No-Update" && result_update_API == "No-Update")
                            {
                                if (result_update_dispenser == "True")
                                {
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service API New version already !!!");
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service Dispenser Update Success");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service API New version already !!!");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service Dispenser Update Success");
                                    Thread.Sleep(1500);
                                    File.AppendAllText(localPath_Logs_Complete, $"Lavender Service API New version already !!! {dateTime} \nLavender Service Dispenser Update Success {dateTime}");
                                }
                                else
                                {
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service API New version already !!!");
                                    Console.WriteLine($"{dateTime} : Program Status >>> Lavender Service Dispenser Update Unsuccess");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service API New version already !!!");
                                    saveLogs($"{dateTime} : Program Status >>> Lavender Service Dispenser Update Unsuccess");
                                    Thread.Sleep(1500);
                                    File.AppendAllText(localPath_Logs_Complete, $"Lavender Service API New version already !!! {dateTime} \nLavender Service Dispenser Update Unsuccess");
                                }
                            }
                            else if (result_update_dispenser == "True" && result_update_API == "True")
                            {
                                File.AppendAllText(localPath_Logs_Complete, $"Lavender Update Complte {dateTime}");
                            }
                            else
                            {
                                File.AppendAllText(localPath_Logs_False, $"Lavender Update False {dateTime}");
                            }

                            //Delect File PackageUpdate
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r PackageUpdate*");
                            result = command.Execute();

                            //Delect File Dispenser
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderDispenser*");
                            result = command.Execute();

                            //Delect File API
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI*");
                            command.Execute();

                            //Delect File LavenderSetting
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderSetting*");
                            result = command.Execute();

                        }
                        else
                        {
                            File.AppendAllText(localPath_Logs_False, $"Can Not Connect to Lavender [Update False] {dateTime}");
                            Console.WriteLine($"{dateTime} : Program Status >>> Can not connect Lavender.");
                            saveLogs($"{dateTime} : Program Status >>> Can not connect Lavender.");
                            Console.WriteLine($"{dateTime} : Program Status >>> Upload Unsuccess.");
                            saveLogs($"{dateTime} : Program Status >>> Upload Unsuccess.");
                            Thread.Sleep(3000);
                            Console.WriteLine($"{dateTime} : Program Status >>> Update Unsuccess.");
                            saveLogs($"{dateTime} : Program Status >>> Update Unsuccess.");
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        File.AppendAllText(localPath_Logs_False, $"Lavender Upload False {dateTime}");
                        Console.WriteLine($"{dateTime} : Program Status >>> Upload Unsuccess.");
                        saveLogs($"{dateTime} : Program Status >>> Upload Unsuccess.");
                        Thread.Sleep(3000);
                        Console.WriteLine($"{dateTime} : Program Status >>> Update Unsuccess.");
                        saveLogs($"{dateTime} : Program Status >>> Update Unsuccess.");
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Console.WriteLine($"{dateTime} : Program Status >>> pos_shift is ON ,pos_status is ON ,pumps_Status_Temp_Stopped dono't equal to pumps_Count OR pumps_Status_Unknown dono't equal to pumps_Count.");
                    saveLogs($"{dateTime} : Program Status >>> pos_shift is ON ,pos_status is ON ,pumps_Status_Temp_Stopped dono't equal to pumps_Count OR pumps_Status_Unknown dono't equal to pumps_Count.");
                    File.AppendAllText(localPath_Logs_False, $"Lavender Update False {dateTime} \nERROR : pos_shift is ON ,pos_status is ON ,pumps_Status_Temp_Stopped dono't equal to pumps_Count OR pumps_Status_Unknown dono't equal to pumps_Count");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{dateTime} : Program Status >>> Upload/Update Unsuccess.");
                saveLogs($"{dateTime} : Program Status >>> Upload/Update Unsuccess.");
                File.AppendAllText(localPath_Logs_False, $"Lavender Update False {dateTime} \nERROR : {ex}");
                Thread.Sleep(2000);
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{dateTime} : Program is Process End");
            saveLogs($"{dateTime} : Program is Process End");
            Thread.Sleep(2000);
        }
        static void saveLogs(string msg)
        {
            using (StreamWriter w = File.AppendText(Directory.GetCurrentDirectory() + @"\LavenderTransfer\Logs\log.txt"))
            {
                w.WriteLine(msg);
            }
        }
        static string Upload(string IP_Address, int port, string username, string password, string localPath, string fileName_Windows, string fileName_Linux)
        {
            try
            {
                var client = new SftpClient(IP_Address, port, username, password);
                client.Connect();

                if (client.IsConnected)
                {
                    string _name = "";
                    string[] files = Directory.GetFiles(localPath, "*");
                    foreach (var file in files)
                    {
                        var fileStream = new FileStream(file, FileMode.Open);
                        if (fileStream != null)
                        {
                            _name = file.Split('\\')[file.Split('\\').Length - 1];
                            client.UploadFile(fileStream, "/home/lavender/" + _name, null);
                        }
                        fileStream.Close();
                    }
                    client.Disconnect();
                    client.Dispose();

                    string Resulr_Check_MD5 = Check_MD5_Hash(IP_Address, fileName_Windows + "\\" + _name, fileName_Linux);
                    return Resulr_Check_MD5;
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                return "False";
            }
        }
        static string Check_MD5_Hash(string Ip_Address, string fileName_Windows, string fileName_Linux)
        {
            try
            {
                string MD5_Windows = "";
                string MD5_Linux = "";

                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    using (var stream = System.IO.File.OpenRead(fileName_Windows))
                    {
                        var hash = md5.ComputeHash(stream);
                        MD5_Windows = BitConverter.ToString(hash).Replace("-", "");
                    }
                }

                SshClient client = new SshClient(Ip_Address, 22, "lavender", "muj,nv,ufvdw,h");
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand($"md5sum {fileName_Linux}");
                    command.Execute();
                    var result = command.Result;
                    MD5_Linux = result.Split(' ')[0];
                    command.Dispose();
                }
                else
                {
                    return "False";
                }

                if (MD5_Linux.ToUpper() == MD5_Windows.ToUpper())
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                return "False";
                Console.WriteLine(ex.Message);
            }
        }
    }
}
