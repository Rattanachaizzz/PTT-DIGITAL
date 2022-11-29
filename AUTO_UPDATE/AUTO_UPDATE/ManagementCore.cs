using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTO_UPDATE
{
    public class ManagementCore
    {
        public DateTime DateTime = DateTime.Now;
        public string username = "lavender";
        public string password = "muj,nv,ufvdw,h";
        public int port = 22;
        public string localPath_LavenderConfig = Directory.GetCurrentDirectory() + @"\LavenderConfig\File_Transfer";
        public string localPath_LavenderAPI = Directory.GetCurrentDirectory() + @"\LavenderAPI\File_Transfer";

        public string Upload(string IP_Address, int port, string username, string password, string localPath, string fileName_Windows, string fileName_Linux)
        {
            try
            {
                string file_name = fileName_Linux.Split('.')[0];
                SshClient client_rm = new SshClient(IP_Address, port, username, password);
                client_rm.Connect();
                if (client_rm.IsConnected)
                {
                    var command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r {fileName_Linux}");
                    var result = command.Execute();
                    command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r {file_name}");
                    command.Execute();
                }
                client_rm.Dispose();

                var client = new SftpClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    dynamic fileStream = null;
                    string[] files = Directory.GetFiles(localPath, "*");
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
                    if (localPath == localPath_LavenderConfig)
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
                    else if (localPath == localPath_LavenderAPI)
                    {
                        SshClient client_Api = new SshClient(IP_Address, port, username, password);
                        client_Api.Connect();
                        if (client_Api.IsConnected)
                        {
                            var command = client_Api.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  cat LavenderAPI.tar.gz.parta* > LavenderAPI.tar.gz");
                            command.Execute();
                            command = client_Api.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r LavenderAPI.tar.gz.parta*");
                            command.Execute();
                            client_Api.Dispose();
                        }
                    }
                    string Resulr_Check_MD5 = Check_MD5_Hash(IP_Address, fileName_Windows, fileName_Linux);
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
        public string Check_MD5_Hash(string Ip_Address, string fileName_Windows, string fileName_Linux)
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

                SshClient client = new SshClient(Ip_Address, port, username, password);
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
            }
        }
    }
}
