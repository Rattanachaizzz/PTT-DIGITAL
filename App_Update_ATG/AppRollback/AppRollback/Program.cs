using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AppRollback
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            //local_Transfer_file
            string localPath_Lavender = Directory.GetCurrentDirectory() + @"\LavenderTransfer\File_Transfer";
            //local_MD5_file
            string localPath_MD5_Lavender = Directory.GetCurrentDirectory() + @"\LavenderTransfer\File_Transfer";
            //localPath_Logs_Complete
            string localPath_Logs_Complete = Directory.GetCurrentDirectory() + @"\LavenderTransfer\Logs\UpdateComplete.txt";
            //localPath_Logs_False
            string localPath_Logs_False = Directory.GetCurrentDirectory() + @"\LavenderTransfer\Logs\UpdateFalse.txt";

            Console.WriteLine("Program AppRollback");
            saveLogs("Program AppRollback");
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
                string username = "lavender";
                string password = "muj,nv,ufvdw,h";
                int port = 22;

                char dubleCode = '"';
                SshClient client = new SshClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + "SELECT (key),(value) FROM lavender.site_config where key = 'Tank_Gauge_Version'" + dubleCode);
                    var result = command.Execute();

                    string Tankgauge_version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                    if (Tankgauge_version == "1.0.6")
                    {
                        Console.WriteLine($"{dateTime} : Program Status >>> Tankgauge_version >> 1.0.6 (Program will don'ot rollback)");
                        saveLogs($"{dateTime} : Program Status >>> Tankgauge_version >> 1.0.6 (Program will don'ot rollback)");
                        Thread.Sleep(3000);
                    }
                    else if (Tankgauge_version == "1.0.7")
                    {
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-tankgauge");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S ls -F /usr/share/");
                        result = command.Execute();
                        string[] fileList = result.Split('\n');
                        string Result_fileBK = "FALSE";
                        foreach (var x in fileList)
                        {
                            if (x.Replace("/", "") == "LavenderTankgauge_BK_V106")
                            {
                                Result_fileBK = "TRUE";
                            }
                        }

                        if (Result_fileBK == "TRUE")
                        {
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderTankgauge");
                            command.Execute();
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/LavenderTankgauge_BK_V106 /usr/share/LavenderTankgauge");
                            command.Execute();
                            Thread.Sleep(5000);
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-tankgauge");
                            command.Execute();
                            Thread.Sleep(5000);
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + "SELECT (key),(value) FROM lavender.site_config where key = 'Tank_Gauge_Version'" + dubleCode);
                            result = command.Execute();
                            Tankgauge_version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                            if (Tankgauge_version == "1.0.6")
                            {
                                Console.WriteLine($"{dateTime} : Program Status >>> Rollback Success.");
                                saveLogs($"{dateTime} : Program Status >>> Rollback Success.");
                                File.AppendAllText(localPath_Logs_Complete, $"Lavender Tankgauge  Rollback Complete {dateTime}");
                                Thread.Sleep(3000);
                            }
                            else
                            {
                                Console.WriteLine($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                                saveLogs($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                                File.AppendAllText(localPath_Logs_False, $"Lavender Tankgauge Rollback False {dateTime}");
                                Thread.Sleep(3000);
                            }
                        }
                        else if (Result_fileBK == "FALSE")
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
                            var Result = Upload(IP_Address, port, username, password, localPath_Lavender, localPath_MD5_Lavender, name_1);
                            if (Result == "True")
                            {
                                client = new SshClient(IP_Address, port, username, password);
                                client.Connect();
                                if (client.IsConnected)
                                {
                                    command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-tankgauge");
                                    result = command.Execute();
                                    command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/LavenderTankgauge");
                                    command.Execute();
                                    command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/LavenderTankgauge.tar.gz -C /usr/share/");
                                    command.Execute();
                                    command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-tankgauge");
                                    command.Execute();
                                    command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderTankgauge.tar.gz");
                                    command.Execute();
                                    Thread.Sleep(5000);
                                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + "SELECT (key),(value) FROM lavender.site_config where key = 'Tank_Gauge_Version'" + dubleCode);
                                    result = command.Execute();
                                    Tankgauge_version = result.Split('\n')[2].Split('|')[1].Replace(" ", "");
                                    client.Dispose();
                                    if (Tankgauge_version == "1.0.6")
                                    {
                                        Console.WriteLine($"{dateTime} : Program Status >>> Rollback Success.");
                                        saveLogs($"{dateTime} : Program Status >>> Rollback Success.");
                                        File.AppendAllText(localPath_Logs_Complete, $"Lavender Tankgauge  Rollback Complete {dateTime}");
                                        Thread.Sleep(3000);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                                        saveLogs($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                                        File.AppendAllText(localPath_Logs_False, $"Lavender Tankgauge Rollback False {dateTime}");
                                        Thread.Sleep(3000);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{dateTime} : Program Status >>> Upload Unsuccess.");
                                    saveLogs($"{dateTime} : Program Status >>> Upload Unsuccess.");
                                    Thread.Sleep(3000);
                                    Console.WriteLine($"{dateTime} : Program Status >>> Update Unsuccess.");
                                    saveLogs($"{dateTime} : Program Status >>> Update Unsuccess.");
                                    Thread.Sleep(3000);
                                }
                            }
                        }
                    }
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                saveLogs($"{dateTime} : Program Status >>> Rollback Unsuccess.");
                File.AppendAllText(localPath_Logs_False, $"Lavender Tankgauge Rollback False {dateTime}");
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
