using Renci.SshNet;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Auto_Update
{
    public partial class UserControl_Update_Service : UserControl
    {
        public UserControl_Update_Service()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;

            backgroundWorker3.WorkerReportsProgress = true;
            backgroundWorker3.WorkerSupportsCancellation = true;

            backgroundWorker4.WorkerReportsProgress = true;
            backgroundWorker4.WorkerSupportsCancellation = true;
        }

        public DateTime DateTime = DateTime.Now;
        public string username = "lavender";
        public string password = "muj,nv,ufvdw,h";
        public int port = 22;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void UserControl_Update_Service_Load(object sender, EventArgs e)
        {

        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        public string Upload(string IP_Address, int port, string username, string password, string Path, string fileName_Windows, string fileName_Linux)
        {
            try
            {
                string file_name = fileName_Linux.Split('.')[0];
                SshClient client_rm = new SshClient(IP_Address, port, username, password);
                client_rm.Connect();
                var x = label33.Text; 
                if (client_rm.IsConnected)
                {
                    if (label33.Text == "Dispenser")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderDispenser*");
                        command.Execute();
                    }
                    else if (label33.Text == "Tankgauge")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderTankgauge*");
                        command.Execute();
                    }
                    else if (label33.Text == "Monitor")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderMonitor*");
                        command.Execute();
                    }
                    else if (label33.Text == "Web config")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderConfig*");
                        command.Execute();
                    }
                    else if (label33.Text == "Web support")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderSupport*");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r 000-lavenderwebconfig.conf");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r default");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r lavender-websupport.service");
                        command.Execute();
                    }
                    else if (label33.Text == "Api")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI*");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r dailCreate.sh");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r DeleteLogDB.sh");
                        command.Execute();
                    }
                    else if (label33.Text == "Gaia")
                    {
                        var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderGaia*");
                        command.Execute();
                    }
                }
                client_rm.Dispose();

                var client = new SftpClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    dynamic fileStream = null;
                    string[] files = Directory.GetFiles(Path, "*");
                    foreach (var file in files)
                    {
                        fileStream = new FileStream(file, FileMode.Open);
                        file_name = file.Split('\\')[file.Split('\\').Length - 1];
                        if (fileStream != null)
                        {
                            client.UploadFile(fileStream, "/home/lavender/" + file_name, null);
                        }
                    }
                    client.Disconnect();
                    client.Dispose();
                    fileStream.Close();
                    if (label33.Text == "Web config")
                    {
                        SshClient client_web = new SshClient(IP_Address, port, username, password);
                        client_web.Connect();
                        if (client_web.IsConnected)
                        {
                            var command = client_web.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  cat LavenderConfig.tar.gz.parta* > LavenderConfig.tar.gz");
                            command.Execute();
                            command = client_web.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderConfig.tar.gz.parta*");
                            command.Execute();
                            client_web.Dispose();
                        }
                    }
                    else if (label33.Text == "Web support")
                    {
                        SshClient client_Support = new SshClient(IP_Address, port, username, password);
                        client_Support.Connect();
                        if (client_Support.IsConnected)
                        {
                            var command = client_Support.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  cat LavenderSupport.tar.gz.parta* > LavenderSupport.tar.gz");
                            command.Execute();
                            command = client_Support.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderSupport.tar.gz.parta*");
                            command.Execute();
                            client_Support.Dispose();
                        }
                    }
                    else if (label33.Text == "Api")
                    {
                        SshClient client_Api = new SshClient(IP_Address, port, username, password);
                        client_Api.Connect();
                        if (client_Api.IsConnected)
                        {
                            //Get Version Old
                            char dubleCode = '"';
                            var command = client_Api.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'API_Version'" + dubleCode);
                            var result = command.Execute();
                            var Sevice_version_Api = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                            if (Sevice_version_Api == "3.0.0")
                            {
                                command = client_Api.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  cat LavenderAPI.tar.gz.parta* > LavenderAPI.tar.gz");
                                command.Execute();
                                command = client_Api.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI.tar.gz.parta*");
                                command.Execute();
                                client_Api.Dispose();
                            }
                        }
                    }
                    string Resulr_Check_MD5 = Check_MD5_Hash(IP_Address, port, username, password, fileName_Windows, fileName_Linux);
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

        public string[] Detail_Pump;
        public string Update(string IP_Address, int port, string username, string password, string Name_Service)
        {
            string key_version = "";
            int count = 0;
            string Service_version_new = "";
            string Sevice_version_old = "";
            string IP_Web_Config = $"http://{IP_Address}/lavenderconfig";
            string IP_Web_Support = $"http://{IP_Address}/lavendersupport";
            try
            {
                switch (Name_Service)
                {
                    case "Dispenser":
                        key_version = "Dispenser_Version";
                        break;
                    case "Tankgauge":
                        key_version = "Tank_Gauge_Version";
                        break;
                    case "Monitor":
                        key_version = "Monitor_Version";
                        break;
                    case "Web config":
                        key_version = "WebConfig_Version";
                        break;
                    case "Web support":
                        key_version = "WebSupport_Version";
                        break;
                    case "Api":
                        key_version = "API_Version";
                        break;
                    case "Gaia":
                        key_version = "GAIA_Version";
                        break;
                }
                char dubleCode = '"';
                SshClient client = new SshClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand("ls");
                    var result = command.Execute();

                    var Dispenser_Version = "";
                    var Tank_Gauge_Version = "";
                    var Monitor_Version = "";
                    var WebConfig_Version = "";
                    var WebSupport_Version = "";
                    var API_Version = "";

                    if (Name_Service == "Dispenser")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-dispenser");
                        result = command.Execute();

                        //Get Pump Count
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT COUNT(pump_id) FROM lavender.pumps" + dubleCode);
                        var pumps_Count = int.Parse(command.Execute().Split('\n')[2].Replace(" ", ""));

                        //Get Status Pump_Real_Times[Temp Stop] && [Unknown]
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT status FROM lavender.pumps_real_time ORDER BY pump_id ASC" + dubleCode);
                        var Detail_Pump = command.Execute().Split('\n');
                        var pumps_Status_Temp_Stopped = 0;
                        var pumps_Status_Unknown = 0;

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

                        if (pumps_Count == pumps_Status_Temp_Stopped || pumps_Count == pumps_Status_Unknown)
                        //if (true)
                        {
                            //Delete File
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderDispenser_*");
                            result = command.Execute();
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderDispenser.tar.gz");
                            result = command.Execute();

                            //Get Version Old
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                            result = command.Execute();
                            Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                            //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderDispenser /usr/share/LavenderDispenser_BK_V{Sevice_version_old.Replace(".", "")}");
                            command.Execute();

                            //Unzip File 
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderDispenser.tar.gz -C /usr/share/");
                            command.Execute();

                            //Start Service
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-dispenser");
                            command.Execute();

                            //Delete File Transfer
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderDispenser.tar.gz");
                            command.Execute();
                        }
                        else
                        {
                            //Get Version Old
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                            result = command.Execute();
                            Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                        }
                    }
                    else if (Name_Service == "Tankgauge")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-tankgauge");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderTankgauge_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderTankgauge_.tar.gz");
                        result = command.Execute();

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


                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT * FROM lavender.loops where loop_type = 2;" + dubleCode);
                        var x = command.Execute().Split('\n');

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderTankgauge /usr/share/LavenderTankgauge_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderTankgauge.tar.gz -C /usr/share/");
                        command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-tankgauge");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderTankgauge.tar.gz");
                        command.Execute();
                    }
                    else if (Name_Service == "Monitor")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-monitor");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderMonitor_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderMonitor.tar.gz");
                        result = command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderMonitor /usr/share/LavenderMonitor_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderMonitor.tar.gz -C /usr/share/");
                        command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-LavenderMonitor");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderMonitor.tar.gz");
                        command.Execute();
                    }
                    else if (Name_Service == "Api")
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
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S chmod 777 dailCreate.sh");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S chmod 777 DeleteLogDB.sh");
                        result = command.Execute();

                        //Move File bash
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/dailCreate.sh /root/AutomateScript/");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/DeleteLogDB.sh /root/AutomateScript/");
                        result = command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderAPI /usr/share/LavenderAPI_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf LavenderAPI.tar.gz -C /usr/share/");
                        command.Execute();

                        //Delect - Add pm2 --name
                        if (Sevice_version_old == "3.0.0") //DONW
                        {
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete LavenderSetting");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete ManageAPI");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete Support");
                            result = command.Execute();
                           
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 save");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 startup");
                            result = command.Execute();
                        }
                        else if (Sevice_version_old != "3.0.0") //UP
                        {
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
                        }

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI.tar.gz");
                        command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderAPI/Command/node_modules.tar.gz");
                        command.Execute();

                        //<-------------------------------------------------------------------------------------------------------------------------------------------------------->

                        //BackUp lavenderSetting >>> lavenderSetting_BK_V[Version Old] 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderSetting /usr/share/LavenderSetting_BK_V100");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderSetting.tar.gz -C /usr/share/");
                        command.Execute();

                        //Restart Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 restart all");
                        result = command.Execute();
                    }
                    else if (Name_Service == "Web config")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-webconfig");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/LavenderConfig_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/LavenderConfig.tar.gz");
                        result = command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /var/www/LavenderConfig /var/www/LavenderConfig_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderConfig.tar.gz -C /var/www/");
                        command.Execute();

                        //Create tb.serial_number_card
                        var msg = "CREATE TABLE IF NOT EXISTS lavender.serial_number_card ( serial_id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY(CYCLE INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1),serialnumber text COLLATE pg_catalog." + "default " + "NOT NULL, description text COLLATE pg_catalog." + "default " + "NOT NULL, status boolean,CONSTRAINT " + "Number_Card485_pkey" + " PRIMARY KEY(serial_id)) WITH(OIDS = FALSE) TABLESPACE pg_default;";
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                        result = command.Execute();
                        msg = "ALTER TABLE IF EXISTS lavender.serial_number_card OWNER to postgres";
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                        result = command.Execute();
                        msg = "GRANT DELETE, INSERT, SELECT, UPDATE ON TABLE lavender.serial_number_card TO lav_application_role";
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{msg}" + dubleCode);
                        result = command.Execute();
                        msg = "GRANT ALL ON TABLE lavender.serial_number_card TO postgres";
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.serial_number_card TO lav_application_role" + dubleCode);
                        result = command.Execute();

                        //Create tb.lavender_api
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
                        result = command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-webconfig");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderConfig*");
                        command.Execute();

                        //CallWeb
                        Thread.Sleep(3000);
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S curl {IP_Web_Config}");
                        result = command.Execute();
                    }
                    else if (Name_Service == "Web support")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-websupport");
                        result = command.Execute();

                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{Name_Service.Replace("Web", "")}_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{Name_Service.Replace("Web", "")}.tar.gz");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /etc/systemd/system/lavender-websupport.service");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /etc/nginx/sites-available/default");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /etc/apache2/sites-available/000-lavenderwebconfig.conf");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /etc/nginx/nginx.conf");
                        result = command.Execute();

                        //Add Row Version
                        command = client.CreateCommand($"PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.site_config(config_id, key, value, type) VALUES (49,'WebSupport_Version','1.0.0','string')" + dubleCode);
                        command.Execute();

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /var/www/{Name_Service.Replace("Web", "")} /var/www/{Name_Service.Replace("Web", "")}_BK_V100");
                        command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/{Name_Service.Replace("Web", "")}.tar.gz -C /var/www/");
                        command.Execute();

                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Chmod File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S chmod 664 /home/lavender/lavender-websupport.service");
                        result = command.Execute();
                        //Move File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/lavender-websupport.service /etc/systemd/system");
                        result = command.Execute();
                        //daemon-reload
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl daemon-reload");
                        result = command.Execute();
                        //Enable Service File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl enable lavender-websupport.service");
                        result = command.Execute();
                        //Restart Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl restart lavender-websupport");
                        result = command.Execute();

                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Server : Nginx
                        //Move File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/default /etc/nginx/sites-available");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/nginx.conf /etc/nginx");
                        result = command.Execute();
                        //reload nginx
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl reload nginx");
                        result = command.Execute();

                        //Server : Apache
                        //Move File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/000-lavenderwebconfig.conf /etc/apache2/sites-available");
                        result = command.Execute();
                        //reload apache2
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl reload apache2");
                        result = command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-websupport");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/{Name_Service.Replace("Web", "")}.tar.gz");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/default");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/000-lavenderwebconfig.conf");
                        result = command.Execute();

                        Thread.Sleep(3000);
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S curl {IP_Web_Support}");
                        command.Execute();
                    }
                    Thread.Sleep(5000);

                again:
                    //Get Version New
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key_version}'" + dubleCode);
                    result = command.Execute();
                    Service_version_new = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                    //Create - Drop Table
                    if (Name_Service == "Dispenser")
                    {
                        if (Service_version_new == "1.10.6")
                        {
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"CREATE TABLE lavender.config_fleet_fraud  (pump_id integer NOT NULL,counting integer NOT NULL,time_waiting integer NOT NULL,duration integer NOT NULL,update_time timestamp without time zone NOT NULL,PRIMARY KEY (pump_id))" + dubleCode);
                            result = command.Execute();
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.config_fleet_fraud TO lav_application_role" + dubleCode);
                            result = command.Execute();
                        }
                        else if (Service_version_new == "1.10.4")
                        {
                            //command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"DROP TABLE lavender.config_fleet_fraud" + dubleCode);
                            //result = command.Execute();
                        }

                        //Restart Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl restart lavender-{Name_Service.Replace("Lavender", "").ToLower()}");
                        command.Execute();

                        if (Detail_Pump != null)
                        {
                            foreach (var x in Detail_Pump)
                            {
                                if (x.Contains("Temp Stop"))
                                {
                                    var pump_id = Int32.Parse(x.Split('|')[0].Replace(" ", ""));
                                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.pump_commands(command, pump_id,create_date, status) VALUES ('stop', {pump_id}, '2022-11-14 17:18:15.139976',false)" + dubleCode);
                                    command.Execute();
                                }
                            }
                        }
                    }
                    if (Name_Service == "Tankgauge")
                    {
                        if (Service_version_new == "1.0.7")
                        {
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"ALTER TABLE lavender.tanks DROP COLUMN IF EXISTS loop_id;" + dubleCode);
                            result = command.Execute();
                        }
                    }
                    if (Name_Service == "Api")
                    {
                        if (Service_version_new == "3.0.0")
                        {
                            //Stop Service
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 stop all");
                            command.Execute();

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

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 restart all");
                            result = command.Execute();
                        }
                        else
                        {
                            //Stop Service
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 stop all");
                            command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete LavenderSetting");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete ManageAPI");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 delete Support");
                            result = command.Execute();
 
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 save");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 startup");
                            result = command.Execute();

                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 restart all");
                            result = command.Execute();
                        }
                    }
                    if (Name_Service == "Web config")
                    {
                        if (Service_version_new == "1.4.2")
                        {
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"DROP TABLE lavender.lavender_api" + dubleCode);
                            result = command.Execute();
                        }
                    }

                    //Check Version
                    count++;
                    if (count >= 3)
                    {
                        return "False";
                        goto end;
                    }
                    if (Sevice_version_old != Service_version_new)
                    {
                        return "True";
                    }
                    else
                    {
                        Thread.Sleep(5000);
                        goto again;
                    }
                end:
                    #region Stamp Versio Image
                    //Dispenser_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Dispenser_Version'" + dubleCode);
                    result = command.Execute();
                    Dispenser_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //Tank_Gauge_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Tank_Gauge_Version'" + dubleCode);
                    result = command.Execute();
                    Tank_Gauge_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //Monitor_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Monitor_Version'" + dubleCode);
                    result = command.Execute();
                    Monitor_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //WebConfig_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'WebConfig_Version'" + dubleCode);
                    result = command.Execute();
                    WebConfig_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //WebSupport_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'WebSupport_Version'" + dubleCode);
                    result = command.Execute();
                    WebSupport_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //API_Version
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'API_Version'" + dubleCode);
                    result = command.Execute();
                    API_Version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    //Stamp Image Version
                    if (Dispenser_Version == "1.10.6" && Tank_Gauge_Version == "1.1.0" && Monitor_Version == "1.0.4" && WebConfig_Version == "1.5.0" && WebSupport_Version == "1.0.3" && API_Version == "3.0.0")
                    {
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /root/LavenderSoftwareInfo.conf");
                        command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S echo -e " + dubleCode + "Image Version: 3.1.1\n" + dubleCode + dubleCode + $"Image Release Date: 02-09-2022\n" + dubleCode + dubleCode + $"Image File Name: OR-DEALER" + dubleCode + ">>" + "/root/LavenderSoftwareInfo.conf");
                        command.Execute();
                    }
                    #endregion
                    client.Dispose();
                }
                else
                {
                    return "False";
                }
            }
            catch
            {
                return "False";
            }
        }
        public string Check_MD5_Hash(string Ip_Address, int port, string username, string password, string part_MD5_Windows, string fileName_Linux)
        {
            try
            {
                string MD5_Windows = "";
                string MD5_Linux = "";

                MD5_Windows = File.ReadAllText(part_MD5_Windows);

                SshClient client = new SshClient(Ip_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand($"md5sum {fileName_Linux}");
                    MD5_Linux = command.Execute().Split(' ')[0];
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
            }
        }


        //Button Transfer
        private void rjButton1_Click(object sender, EventArgs e)
        {
            DialogResult result_Transfer = MessageBox.Show("Do you want start process update file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result_Transfer == DialogResult.Yes)
            {
                rjButton1.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                rjButton1.Enabled = true;
            }
        }

        //Transfer All Station 
        public string station_id;
        public Image image_done = Image.FromFile(Directory.GetCurrentDirectory() + @"\img\icons_done.png");
        public Image image_close = Image.FromFile(Directory.GetCurrentDirectory() + @"\img\icons_close.png");
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (STATIONDBEntities db = new STATIONDBEntities())
            {
                string fileName_Linux = "";
                if (label33.Text == "Dispenser")
                {
                    fileName_Linux = "LavenderDispenser.tar.gz";
                }
                else if (label33.Text == "Tankgauge")
                {
                    fileName_Linux = "LavenderTankgauge.tar.gz";
                }
                else if (label33.Text == "Monitor")
                {
                    fileName_Linux = "LavenderMonitor.tar.gz";
                }
                else if (label33.Text == "Web config")
                {
                    fileName_Linux = "LavenderConfig.tar.gz";
                }
                else if (label33.Text == "Web support")
                {
                    fileName_Linux = "LavenderSupport.tar.gz";
                }
                else if (label33.Text == "Api")
                {
                    fileName_Linux = "LavenderAPI.tar.gz";
                }
                else if (label33.Text == "Gaia")
                {
                    fileName_Linux = "LavenderGAIA.tar.gz";
                }

                foreach (DataGridViewRow row in dataGridView11.Rows)
                {
                    station_id = row.Cells[0].Value.ToString().Replace(" ", "");
                    var station_ip = row.Cells[4].Value.ToString().Replace(" ", "");
                    var result_transfer = Upload(station_ip, port, username, password, part.Text.Replace(" ", ""), part.Text.Replace("File_Transfer", "MD5") + @"\KEY.txt", fileName_Linux);
                    if (result_transfer.ToString() == "True")
                    {
                        var Station_Transfer = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip);
                        Station_Transfer.result_transfer = true;
                        backgroundWorker1.ReportProgress(1);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        var Station_Transfer = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip);
                        Station_Transfer.result_transfer = false;
                        backgroundWorker1.ReportProgress(0);
                        Thread.Sleep(1000);
                    }
                }
                db.SaveChanges();
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int result_transfer = e.ProgressPercentage;
            if (result_transfer == 1)
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[5].Value = image_done;
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[7].Value = "True";
                }
                ));
            }
            else
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[5].Value = image_close;
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[7].Value = "False";
                }
               ));
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int Station_Total = dataGridView11.RowCount;
            int Transfer_Success_Total = 0;
            int Update_Success_Total = 0;

            foreach (DataGridViewRow row in dataGridView11.Rows)
            {
                string Result_Station_Transfer_Completed = row.Cells[7].Value.ToString();
                string Result_Station_Updtae_Completed = row.Cells[8].Value.ToString();
                Transfer_Success_Total = Result_Station_Transfer_Completed == "True" ? Transfer_Success_Total + 1 : Transfer_Success_Total;
                Update_Success_Total = Result_Station_Updtae_Completed == "True" ? Update_Success_Total + 1 : Update_Success_Total;
            }
            string msg = $"Transfer file Success \n  - Station Total : {Station_Total} \n  - Transfer Success : {Transfer_Success_Total} \n  - Update Success : {Update_Success_Total}";
            MessageBox.Show(msg, "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Question);
            rjButton1.Enabled = true;
        }



        //Button Update
        private void rjButton2_Click(object sender, EventArgs e)
        {
            DialogResult result_Update = MessageBox.Show("Do you want start process update file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result_Update == DialogResult.Yes)
            {
                rjButton2.Enabled = false;
                backgroundWorker3.RunWorkerAsync();
            }
            else
            {
                rjButton2.Enabled = true;
            }
        }

        //Update All Station
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            using (STATIONDBEntities db = new STATIONDBEntities())
            {
                string fileName_Linux = "";
                if (label33.Text == "Dispenser")
                {
                    fileName_Linux = "LavenderDispenser.tar.gz";
                }
                else if (label33.Text == "Tankgauge")
                {
                    fileName_Linux = "LavenderTankgauge.tar.gz";
                }
                else if (label33.Text == "Monitor")
                {
                    fileName_Linux = "LavenderMonitor.tar.gz";
                }
                else if (label33.Text == "Web config")
                {
                    fileName_Linux = "LavenderConfig.tar.gz";
                }
                else if (label33.Text == "Web support")
                {
                    fileName_Linux = "LavenderSupport.tar.gz";
                }
                else if (label33.Text == "Api")
                {
                    fileName_Linux = "LavenderAPI.tar.gz";
                }
                else if (label33.Text == "Gaia")
                {
                    fileName_Linux = "LavenderGAIA.tar.gz";
                }

                foreach (DataGridViewRow row in dataGridView11.Rows)
                {
                    station_id = row.Cells[0].Value.ToString().Replace(" ", "");
                    var station_ip = row.Cells[4].Value.ToString().Replace(" ", "");
                    var result_update = Update(station_ip, port, username, password, label33.Text);
                    if (result_update.ToString() == "True")
                    {
                        var Station_Update = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip);
                        Station_Update.result_update = true;
                        backgroundWorker3.ReportProgress(1);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        var Station_Update = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip);
                        Station_Update.result_update = false;
                        backgroundWorker3.ReportProgress(0);
                        Thread.Sleep(1000);
                    }
                }
                db.SaveChanges();
            }
        }
        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int result_update = e.ProgressPercentage;
            if (result_update == 1)
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[6].Value = image_done;
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[8].Value = "True";
                }
                ));
            }
            else
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[6].Value = image_close;
                    dataGridView11.Rows[int.Parse(station_id) - 1].Cells[8].Value = "False";
                }
               ));
            }
        }
        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int Station_Total = dataGridView11.RowCount;
            int Transfer_Success_Total = 0;
            int Update_Success_Total = 0;

            foreach (DataGridViewRow row in dataGridView11.Rows)
            {
                string Result_Station_Transfer_Completed = row.Cells[7].Value.ToString();
                string Result_Station_Updtae_Completed = row.Cells[8].Value.ToString();
                Transfer_Success_Total = Result_Station_Transfer_Completed == "True" ? Transfer_Success_Total + 1 : Transfer_Success_Total;
                Update_Success_Total = Result_Station_Updtae_Completed == "True" ? Update_Success_Total + 1 : Update_Success_Total;
            }
            string msg = $"Update file Success \n  - Station Total : {Station_Total} \n  - Transfer Success : {Transfer_Success_Total} \n  - Update Success : {Update_Success_Total}";
            MessageBox.Show(msg, "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Question);
            rjButton2.Enabled = true;
        }




        //Check Transfer and Update By Pump
        private void dataGridView11_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            rjButton1.Enabled = false;

            station_id2 = null;
            station_name2 = null;
            station_ip2 = null;
            station_rs = null;
            int rowIndex = e.RowIndex;
            int ColumnIndex = e.ColumnIndex;
            DataGridViewRow row;
            if (rowIndex != -1)
            {
                if (ColumnIndex == 5)
                {
                    row = dataGridView11.Rows[rowIndex];
                    station_id2 = row.Cells[0].Value.ToString();
                    station_name2 = row.Cells[3].Value.ToString();
                    station_ip2 = row.Cells[4].Value.ToString().Replace(" ", "");
                    DialogResult result = MessageBox.Show($"Do you want transfer file to station_name : {station_name2}", "TRANSFER FILE", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        backgroundWorker2.RunWorkerAsync();
                    }
                    else
                    {
                        rjButton1.Enabled = true;
                    }
                }
                else if (ColumnIndex == 6)
                {
                    row = dataGridView11.Rows[rowIndex];
                    station_id2 = row.Cells[0].Value.ToString();
                    station_name2 = row.Cells[3].Value.ToString();
                    station_ip2 = row.Cells[4].Value.ToString();
                    DialogResult result = MessageBox.Show($"Do you want update file station_name : {station_name2}", "UPDATE FILE", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                    }
                    else
                    {
                        rjButton1.Enabled = true;
                    }
                }
                else
                {
                    rjButton1.Enabled = true;
                }
            }
        }

        //Transfer By Station 
        public string station_id2 = null;
        public string station_name2 = null;
        public string station_ip2 = null;
        public string station_rs = null;
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            using (STATIONDBEntities db = new STATIONDBEntities())
            {
                string fileName_Linux = "";
                if (label33.Text == "Dispenser")
                {
                    fileName_Linux = "LavenderDispenser.tar.gz";
                }
                else if (label33.Text == "Tankgauge")
                {
                    fileName_Linux = "LavenderTankgauge.tar.gz";
                }
                else if (label33.Text == "Monitor")
                {
                    fileName_Linux = "LavenderMonitor.tar.gz";
                }
                else if (label33.Text == "Web config")
                {
                    fileName_Linux = "LavenderConfig.tar.gz";
                }
                else if (label33.Text == "Web support")
                {
                    fileName_Linux = "LavenderSupport.tar.gz";
                }
                else if (label33.Text == "Api")
                {
                    fileName_Linux = "LavenderAPI.tar.gz";
                }
                else if (label33.Text == "Gaia")
                {
                    fileName_Linux = "LavenderGAIA.tar.gz";
                }

                var result_transfer = Upload(station_ip2, port, username, password, part.Text.Replace(" ", ""), part.Text.Replace("File_Transfer", "MD5") + @"\KEY.txt", fileName_Linux);
                if (result_transfer.ToString() == "True")
                {
                    var Station_Transfer = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip2);
                    Station_Transfer.result_transfer = true;
                    station_rs = "Success";
                    backgroundWorker2.ReportProgress(1);
                    Thread.Sleep(1000);
                }
                else
                {
                    var Station_Transfer = db.station_installer.ToList().SingleOrDefault(x => x.station_ip == station_ip2);
                    Station_Transfer.result_transfer = false;
                    station_rs = "Unsuccess";
                    backgroundWorker2.ReportProgress(0);
                    Thread.Sleep(1000);
                }
                db.SaveChanges();
            }
        }
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int result_transfer = e.ProgressPercentage;
            if (result_transfer == 1)
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id2) - 1].Cells[5].Value = image_done;
                    dataGridView11.Rows[int.Parse(station_id2) - 1].Cells[7].Value = "True";
                }
                ));
            }
            else
            {
                dataGridView11.BeginInvoke(new Action(() =>
                {
                    dataGridView11.Rows[int.Parse(station_id2) - 1].Cells[5].Value = image_close;
                    dataGridView11.Rows[int.Parse(station_id2) - 1].Cells[7].Value = "False";
                }
               ));
            }
        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string msg = $"Transfer file to Station {station_name2} is {station_rs}";
            MessageBox.Show(msg, "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Question);
            rjButton1.Enabled = true;
        }




    }
}
