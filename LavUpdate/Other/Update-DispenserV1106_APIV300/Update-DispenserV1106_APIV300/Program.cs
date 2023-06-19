using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Diagnostics;
using System.IO;

namespace Update_DispenserV1106_APIV300
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now} : Program Update DispenserV1.10.6 && APIV.3.0.0 version1.0.4");
            Console.WriteLine($"{DateTime.Now} : Program is Process Start");

            if (Condition_Update())
            {
                //Extract file
                //ExecuteCommand($"echo 'muj,nv,ufvdw,h' | sudo -S tar -xzf /home/lavender/Update-DispenserV1106_APIV300/LavenderTransfer/File_Transfer/PackageUpdate.tar.gz");

                //Dispenser
                if (versionDispenser() != "1.10.6")
                {
                    //Update_DispenserV1106();
                    Console.WriteLine("Update Dispenser");
                }
                else
                {
                    //Write log 
                    Console.WriteLine("Can,t update Dispenser");
                }

                //API
                if (versionAPI() != "3.0.0")
                {

                }
                else
                {
                    //Write log
                }
            }
        }

        public static string ExecuteCommand(string command)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = "-c \" " + command + " \"";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();

            string result = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return result;
        }

        private static void Update_DispenserV1106()
        {
            //Stop service dispenser
            ExecuteCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-dispenser");
            //Backup file
            ExecuteCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv LavenderDispenser LavenderDispenser_BK_V{versionDispenser().Replace(".", "")}");
            //Copy file
            ExecuteCommand($"echo 'muj,nv,ufvdw,h' | sudo -S cp -r /home/lavender/Update-DispenserV1106_APIV300/LavenderTransfer/File_Transfer/PackageUpdate/LavenderDispenser /usr/share");
            //Start service dispenser
            ExecuteCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-dispenser");
        }

        static string pgSQL_Query(string cm)
        {
            try
            {
                string result = "";
                var CurrentDirectory = Directory.GetCurrentDirectory();
                StreamReader r = new StreamReader(CurrentDirectory + @"\ConnectionString.json");
                string jsonString = r.ReadToEnd();
                JObject rss = JObject.Parse(jsonString);
                string IP_Address = (string)rss["ConnectionStrings"]["ConnectionString"];
                //using (NpgsqlConnection conn = new NpgsqlConnection($"Server={IP_Address};User Id=postgres;Password=8545;Database=LAVENDERDB;"))
                using (NpgsqlConnection conn = new NpgsqlConnection($"User ID=postgres;Password=\"gdH[,yogvkw;hg]pmyh'fvdw,h\";Port=5432;Host={IP_Address};Database=LAVENDERDB;"))
                {                                                                                        
                    conn.Open();
                    NpgsqlCommand command = new NpgsqlCommand(cm, conn);
                    result = command.ExecuteScalar().ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        static bool Condition_Update()
        {
            string cm = "";
            string result = "";
            string pumps_Count = "";
            string pos_shift = "";
            string pos_status = "";
            string pumps_Status_Unknown = "";
            string pumps_Status_Temp_Stopped = "";

            //Get Pump Count
            cm = "SELECT COUNT(pump_id) FROM lavender.pumps";
            pumps_Count = pgSQL_Query(cm);

            //Get Pump_Count[Unknown]
            cm = "SELECT COUNT(pump_id) FROM lavender.pumps_real_time WHERE status = 'Unknown'";
            pumps_Status_Unknown = pgSQL_Query(cm);

            //Get Pump_Count[Temp Stopped]
            cm = "SELECT COUNT(pump_id) FROM lavender.pumps_real_time WHERE status = 'Temp Stopped'";
            pumps_Status_Temp_Stopped = pgSQL_Query(cm);

            //Get pos_status
            cm = "SELECT status FROM lavender.pos_status WHERE history_id =(SELECT max(history_id) FROM lavender.pos_status)";
            pos_status = pgSQL_Query(cm);

            //Get pos_shift
            cm = "SELECT status FROM lavender.pos_shift WHERE shift_id =(SELECT max(shift_id) FROM lavender.pos_shift)";
            pos_shift = pgSQL_Query(cm);

            if (pumps_Status_Unknown == pumps_Count || pumps_Status_Temp_Stopped == pumps_Count || pos_status == "0" || pos_shift == "0")
                return true;
            return false;
        }

        static string versionDispenser()
        {
            //Get Sevice Dispenser
            string cm = "SELECT value FROM lavender.site_config where key = 'Dispenser_Version'";
            string SeviceDispenser = pgSQL_Query(cm);
            return SeviceDispenser;
        }

        static string versionAPI()
        {
            //Get Sevice API
            string cm = "SELECT value FROM lavender.site_config where key = 'API_Version'";
            string SeviceAPI = pgSQL_Query(cm);
            return SeviceAPI;
        }
    }
}
