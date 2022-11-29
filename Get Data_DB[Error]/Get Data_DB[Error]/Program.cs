using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Get_Data_DB_Error_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            var CurrentDirectory = Directory.GetCurrentDirectory();
            StreamReader r = new StreamReader(CurrentDirectory + @"\ConnectionString.json");
            string jsonString = r.ReadToEnd();
            JObject rss = JObject.Parse(jsonString);
            string IP_ADDRESS = (string)rss["ConnectionStrings"]["IP_ADDRESS"];
            string Disk = (string)rss["ConnectionStrings"]["Disk"];
            string USERNAME = "lavender";
            string PASSWORD = "muj,nv,ufvdw,h";
            string Result_Update = "";
            int PORT = 22;

            Console.WriteLine($"Program Get Data DB");
            Console.WriteLine($"{dateTime} : Program is Process Start.");

            Upload(IP_ADDRESS, PORT, USERNAME, PASSWORD, Disk);

            Console.WriteLine($"{dateTime} : Program is Process End.");
            Thread.Sleep(3000);
        }
        static void Upload(string IP_ADDRESS, int PORT, string USERNAME, string PASSWORD, string Disk)
        {
            SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
            DateTime dateTime = DateTime.Now;
            char dubleCode = '"';
            try
            {
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = 'Site_Code'" + dubleCode);
                    var Site_Code = command.Execute().Split('|')[2].Split('\n')[0].Trim(' ');

                    command = client.CreateCommand($"rm -r {Site_Code}.csv");
                    command.Execute();

                    //command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -t -A -F" + dubleCode + "," + dubleCode + " -c " + dubleCode + $@"\COPY (SELECT * FROM lavender.pump_logs WHERE message like '%erroe%' or  message like '%Non%' ORDER BY pump_id DESC) To '/home/lavender/{Site_Code}.csv' With CSV DELIMITER ',' HEADER" + dubleCode);
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -t -A -F" + dubleCode + "," + dubleCode + " -c " + dubleCode + $@"\COPY (SELECT pump_id,SUM(CASE WHEN create_date >= now() - INTERVAL '14 DAY' AND catagory = 'Error' AND message like '%error%' THEN 1 ELSE 0 END) AS count_error,SUM(CASE WHEN create_date >= now() - INTERVAL '14 DAY' AND catagory = 'Warning' AND message like '%Non%' THEN 1 ELSE 0 END) AS count_non, SUM(CASE WHEN create_date >= now() - INTERVAL '14 DAY' AND catagory = 'Error' AND message like '%error%' OR catagory = 'Warning' AND message like '%Non%' THEN 1 ELSE 0 END) AS sum_error FROM lavender.pump_logs GROUP BY pump_id ORDER BY pump_id ASC) To '/home/lavender/{Site_Code}.csv' With CSV DELIMITER ',' HEADER" + dubleCode);
                    command.Execute();
                    Thread.Sleep(2000);

                    command = client.CreateCommand("ls");
                    var issuccess = command.Execute().Split('\n');
                    client.Dispose();

                    if (issuccess.Contains($"{Site_Code}.csv"))
                    {
                        Console.WriteLine($"{dateTime} : Program Status >>> Get Data Success.");
                        Thread.Sleep(2000);
                        try
                        {
                            using (var sftp = new SftpClient(IP_ADDRESS, PORT, USERNAME, PASSWORD))
                            {
                                sftp.Connect();
                                if (sftp.IsConnected)
                                {
                                    using (var file = File.Create($"{Disk}:/{Site_Code}.csv")) //local file at will download keep. 
                                    {
                                        sftp.DownloadFile($"/home/lavender/{Site_Code}.csv", file); //local file in remote server , local file in client
                                    }
                                }
                                sftp.Disconnect();

                                Console.WriteLine($"{dateTime} : Program Status >>> Press recheck file name is {Site_Code}.csv at {Disk}:/");
                                Thread.Sleep(2000);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{dateTime} : Program Status >>> Do not can download {Site_Code}.csv");
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{dateTime} : Program Status >>> Get Data Unsuccess.");
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{dateTime} : Program Status >>> Get Data Unsuccess.");
            }
        }
    }
}
