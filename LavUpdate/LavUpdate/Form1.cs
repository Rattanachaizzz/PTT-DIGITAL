using System;
using Ganss.Excel;
using Renci.SshNet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Security;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;


namespace LavUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string dateTime = "";
        public IEnumerable<PumpStations> pump_stations;
        public List<Transfer> Result_Transfer = new List<Transfer>();
        public List<Deploy> Result_Deploy = new List<Deploy>();
        public List<Status> Result_Status = new List<Status>();
        public List<Delete> Result_Delete = new List<Delete>();

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        //Brows station file
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog Excel = new OpenFileDialog();
                Excel.Filter = "All Files (*.*)|*.*";
                Excel.FilterIndex = 1;
                Excel.Multiselect = true;
                if (Excel.ShowDialog() == DialogResult.OK)
                {
                    string FileName = Excel.FileName;
                    textBox1.Text = FileName;
                    string path = FileName;
                    pump_stations = new ExcelMapper(path).Fetch<PumpStations>(0);
                    progressBar1.Maximum = pump_stations.Count();
                    progressBar2.Maximum = pump_stations.Count();
                    progressBar3.Maximum = pump_stations.Count();
                    progressBar4.Maximum = pump_stations.Count();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Brows transfer file
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Button transfer file
        private void button3_Click(object sender, EventArgs e)
        {
            Result_Transfer.Clear();
            DialogResult resultPopup_1 = MessageBox.Show("Do you want start process tranfers file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultPopup_1 == DialogResult.Yes)
            {
                //Have path file?
                if (textBox1.Text.Contains('\\') && textBox2.Text.Contains('\\') && textBox3.Text.Contains('\\'))
                {
                    update_percentTransfer.RunWorkerAsync();
                    return;
                }
                MessageBox.Show("Press recheck import station ,transfer file or md5 file,And try agian.", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Button status file
        private void button5_Click(object sender, EventArgs e)
        {
            Result_Status.Clear();
            DialogResult resultPopup_1 = MessageBox.Show("Do you want start process get status file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultPopup_1 == DialogResult.Yes)
            {
                //Have path file?
                if (textBox1.Text.Contains('\\'))
                {
                    update_percentStatus.RunWorkerAsync();
                    return;
                }
                MessageBox.Show("Press recheck import station ,And try agian.", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        //Button delete file
        private void button6_Click(object sender, EventArgs e)
        {
            Result_Delete.Clear();
            DialogResult resultPopup_1 = MessageBox.Show("Do you want start process  delete file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultPopup_1 == DialogResult.Yes)
            {
                //Have path file?
                if (textBox1.Text.Contains('\\') && textBox2.Text.Contains('\\') && textBox3.Text.Contains('\\'))
                {
                    update_percentDelete.RunWorkerAsync();
                    return;
                }
                MessageBox.Show("Press recheck import station ,transfer file or md5 file,And try agian.", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ResultStatus(PumpStations pump, List<KeyValuePair<string, string>> serviceVersions, string remark)
        {
            Status s = new Status();
            s.id_station = pump.id_station;
            s.pbl_station = pump.pbl_station;
            s.bu_station = pump.bu_station;
            s.pbl_station = pump.pbl_station;
            s.name_station = pump.name_station;
            s.ip_sim = pump.ip_sim;
            s.remark = remark;

            foreach (var serviceVersion in serviceVersions)
            {
                if (serviceVersion.Key == "dispenser")
                {
                    s.dispenser = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "tankgauge")
                {
                    s.tankgauge = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "gaia")
                {
                    s.gaia = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "monitor")
                {
                    s.monitor = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "webconfig")
                {
                    s.webconfig = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "websupport")
                {
                    s.websupport = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "lavupdate")
                {
                    s.lavupdate = serviceVersion.Value;
                }
                else if (serviceVersion.Key == "api")
                {
                    s.api = serviceVersion.Value;
                }
            }

            Result_Status.Add(s);
        }

        public void ResultTransfer(PumpStations pump, string result, string remark)
        {
            Transfer t = new Transfer();
            t.id_station = pump.id_station;
            t.pbl_station = pump.pbl_station;
            t.bu_station = pump.bu_station;
            t.pbl_station = pump.pbl_station;
            t.name_station = pump.name_station;
            t.ip_sim = pump.ip_sim;
            t.result = result;
            t.remark = remark;
            Result_Transfer.Add(t);
        }

        public void ResultDelete(PumpStations pump, string result, string remark)
        {
            Delete d = new Delete();
            d.id_station = pump.id_station;
            d.pbl_station = pump.pbl_station;
            d.bu_station = pump.bu_station;
            d.pbl_station = pump.pbl_station;
            d.name_station = pump.name_station;
            d.ip_sim = pump.ip_sim;
            d.result = result;
            d.remark = remark;
            Result_Delete.Add(d);
        }
        public void ResultDeploy(PumpStations pump, string result, string remark)
        {
            Deploy d = new Deploy();
            d.id_station = pump.id_station;
            d.pbl_station = pump.pbl_station;
            d.bu_station = pump.bu_station;
            d.pbl_station = pump.pbl_station;
            d.name_station = pump.name_station;
            d.ip_sim = pump.ip_sim;
            d.result = result;
            d.remark = remark;
            Result_Deploy.Add(d);
        }

        public void reportDeploy()
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string partFile = Directory.GetCurrentDirectory() + "\\Reports\\deployFile\\Report_" + dateTime + ".xlsx";
            ExcelMapper mapper = new ExcelMapper();
            mapper.Save(partFile, Result_Deploy, dateTime);
        }

        public void reportStatus()
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string partFile = Directory.GetCurrentDirectory() + "\\Reports\\statusFile\\Report_" + dateTime + ".xlsx";
            ExcelMapper mapper = new ExcelMapper();
            mapper.Save(partFile, Result_Status, dateTime);
        }
        public void reportDelet()
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string partFile = Directory.GetCurrentDirectory() + "\\Reports\\deleteFile\\Report_" + dateTime + ".xlsx";
            ExcelMapper mapper = new ExcelMapper();
            mapper.Save(partFile, Result_Delete, dateTime);
        }

        public void reportTransfer()
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string partFile = Directory.GetCurrentDirectory() + "\\Reports\\transfereFile\\Report_" + dateTime + ".xlsx";
            ExcelMapper mapper = new ExcelMapper();
            mapper.Save(partFile, Result_Transfer, dateTime);
        }

        private void update_percentDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (var p in pump_stations)
                {
                    try
                    {
                        string USERNAME = "lavender";
                        string PASSWORD = "muj,nv,ufvdw,h";
                        int PORT = 22;
                        var IP_ADDRESS = p.ip_sim;
                        var nameFileDelete = (textBox2.Text).Replace(" ", "").Split('\\').LastOrDefault();
                        SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S ls /home/lavender/";
                            var command = client.CreateCommand(command_exc);
                            var result = command.Execute();
                            //Do have file yes or no?
                            if (result.Contains(nameFileDelete)) //Have file 
                            {
                                //Delete file
                                command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/" + nameFileDelete;
                                command = client.CreateCommand(command_exc);
                                result = command.Execute();

                                command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S ls /home/lavender/";
                                command = client.CreateCommand(command_exc);
                                result = command.Execute();
                                if (!result.Contains(nameFileDelete))
                                {
                                    //Delete Success
                                    ResultDelete(p, "Success", "-");

                                    //Update progressBar
                                    int type = 4;
                                    int percent_ProgressBar = int.Parse(p.id_station);
                                    int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                    update_percentDelete.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                                }
                                else
                                {
                                    //Delete Unsuccess
                                    ResultDelete(p, "Unsuccess", "Can't delete file");

                                    //Update progressBar
                                    int type = 4;
                                    int percent_ProgressBar = int.Parse(p.id_station);
                                    int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                    update_percentDelete.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                                }
                            }
                            else //Don't have file
                            {
                                //Delete Unsuccess
                                ResultDelete(p, "Unsuccess", "Don't have file");

                                //Update progressBar
                                int type = 4;
                                int percent_ProgressBar = int.Parse(p.id_station);
                                int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                update_percentDelete.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));

                            }
                        }
                    }
                    catch (Exception ex)//Error or can't remote lav 
                    {
                        //Delete Unsuccess 
                        ResultDelete(p, "Unsuccess", ex.Message);

                        //Update progressBar
                        int type = 4;
                        int percent_ProgressBar = int.Parse(p.id_station);
                        int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                        update_percentDelete.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                    }
                }

                reportDelet();
            }
            catch (Exception ex)//Error
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_percentDelete_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int type = e.ProgressPercentage;
            var args = (Tuple<int, int>)e.UserState;
            string percent_Label = args.Item1.ToString();
            int percent_ProgressBar = int.Parse(args.Item2.ToString());
            progressBar4.Value = percent_ProgressBar;
            label13.Text = percent_Label;
        }

        private void update_percentDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Process delete file successfull,\npress recheck report file at {Directory.GetCurrentDirectory()}\\Repors\\deleteFile\\Report_{dateTime}.xlsx", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //Button md5 file
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public string Upload(string IP_Address, int port, string username, string password, string transferFile_localPath, string md5file_Windows)
        {
            try
            {
                var client = new SftpClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    var fileStream = new FileStream(transferFile_localPath, FileMode.Open);
                    if (fileStream != null)
                    {
                        client.UploadFile(fileStream, "/home/lavender/" + transferFile_localPath.Replace(" ", "").Split('\\').LastOrDefault(), null);
                    }
                    fileStream.Close();
                    client.Disconnect();
                    client.Dispose();

                    string Resulr_Check_MD5 = Check_MD5_Hash(IP_Address, md5file_Windows, transferFile_localPath);
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

        static string Check_MD5_Hash(string Ip_Address, string md5file_Windows, string md5file_Linux)
        {
            try
            {
                string MD5_Windows = "";
                string MD5_Linux = "";
                MD5_Windows = File.ReadAllText(md5file_Windows);
                SshClient client = new SshClient(Ip_Address, 22, "lavender", "muj,nv,ufvdw,h");
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand($"md5sum {md5file_Linux.Replace(" ", "").Split('\\').LastOrDefault()}");
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

        private void update_percentTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (var p in pump_stations)
                {
                    try
                    {
                        string USERNAME = "lavender";
                        string PASSWORD = "muj,nv,ufvdw,h";
                        int PORT = 22;
                        var IP_ADDRESS = p.ip_sim;
                        SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var Result = Upload(IP_ADDRESS, PORT, USERNAME, PASSWORD, textBox2.Text, textBox3.Text);
                            if (Result == "True")
                            {
                                //Transfer Unsuccess 
                                ResultTransfer(p, "Success", "-");

                                //Update progressBar
                                int type = 1;
                                int percent_ProgressBar = int.Parse(p.id_station);
                                int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                update_percentTransfer.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                            }
                            else
                            {
                                //Transfer Unsuccess 
                                ResultTransfer(p, "Unsuccess", "Check sum not macth");

                                //Update progressBar
                                int type = 1;
                                int percent_ProgressBar = int.Parse(p.id_station);
                                int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                update_percentTransfer.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                            }
                        }
                    }
                    catch (Exception ex)//Error or can't remote lav
                    {
                        //Transfer Unsuccess 
                        ResultTransfer(p, "Unsuccess", ex.Message);

                        //Update progressBar
                        int type = 1;
                        int percent_ProgressBar = int.Parse(p.id_station);
                        int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                        update_percentTransfer.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                    }
                }

                reportTransfer();
            }
            catch (Exception ex)//Error
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void update_percentTransfer_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int type = e.ProgressPercentage;
            var args = (Tuple<int, int>)e.UserState;
            string percent_Label = args.Item1.ToString();
            int percent_ProgressBar = int.Parse(args.Item2.ToString());
            progressBar1.Value = percent_ProgressBar;
            label10.Text = percent_Label;
        }

        private void update_percentTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Process Transfer file successfull,\npress recheck report file at {Directory.GetCurrentDirectory()}\\Reports\\transfereFile\\Report_{dateTime}.xlsx", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void update_percentStatus_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (var p in pump_stations)
                {
                    try
                    {
                        int PORT = 22;
                        char dubleCode = '"';
                        string USERNAME = "lavender";
                        string PASSWORD = "muj,nv,ufvdw,h";
                        var IP_ADDRESS = p.ip_sim;
                        string key = "gdH[,yogvkw;hg]pmyh'fvdw,h";

                        SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            //Get version service
                            string command_exc = "SELECT (key),(value) FROM lavender.site_config WHERE key like '%Version%' ORDER BY config_id ASC";
                            var command = client.CreateCommand("PGPASSWORD=" + dubleCode + key + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{command_exc}" + dubleCode);
                            var result = command.Execute().Split('\n');

                            var serviceVersions = new List<KeyValuePair<string, string>>();
                            foreach (var r in result)
                            {
                                if (r.Contains("Version"))
                                {
                                    if (r.Contains("Dispenser_Version"))
                                    {
                                        //Get version service
                                        string dispenserVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-dispenser";
                                        command = client.CreateCommand(command_exc);
                                        string dispenserStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add("dispenser", $"{dispenserVersion} [{dispenserStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("dispenser", $"{dispenserVersion} [{dispenserStatus}]"));
                                    }
                                    else if (r.Contains("Tank_Gauge_Version"))
                                    {
                                        //Get version service
                                        string tankgaugeVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-tankgauge";
                                        command = client.CreateCommand(command_exc);
                                        string tankgaugeStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{tankgaugeVersion} [{tankgaugeStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("tankgauge", $"{tankgaugeVersion} [{tankgaugeStatus}]"));
                                    }
                                    else if (r.Contains("GAIA_Version"))
                                    {
                                        //Get version service
                                        string gaiaVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-gaia";
                                        command = client.CreateCommand(command_exc);
                                        string gaiaStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{gaiaVersion} [{gaiaStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("gaia", $"{gaiaVersion} [{gaiaStatus}]"));
                                    }
                                    else if (r.Contains("Monitor_Version"))
                                    {
                                        //Get version service
                                        string monitorVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-monitor";
                                        command = client.CreateCommand(command_exc);
                                        string monitorStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{monitorVersion} [{monitorStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("monitor", $"{monitorVersion} [{monitorStatus}]"));
                                    }
                                    else if (r.Contains("WebConfig_Version"))
                                    {
                                        //Get version service
                                        string webconfigVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-webconfig";
                                        command = client.CreateCommand(command_exc);
                                        string webconfigStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{webconfigVersion} [{webconfigStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("webconfig", $"{webconfigVersion} [{webconfigStatus}]"));
                                    }
                                    else if (r.Contains("WebSupport_Version"))
                                    {
                                        //Get version service
                                        string websupportVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-websupport";
                                        command = client.CreateCommand(command_exc);
                                        string websupportStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{websupportVersion} [{websupportStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("websupport", $"{websupportVersion} [{websupportStatus}]"));
                                    }
                                    else if (r.Contains("LavenderUpdate_Version"))
                                    {
                                        //Get version service
                                        string lavupdateVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S systemctl status lavender-update";
                                        command = client.CreateCommand(command_exc);
                                        string lavupdateStatus = command.Execute().Contains("(running)") ? "active" : "inactive";

                                        //Add to list
                                        //serviceVersions.Add($"{lavupdateVersion} [{lavupdateStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("lavupdate", $"{lavupdateVersion} [{lavupdateStatus}]"));
                                    }
                                    else if (r.Contains("API_Version"))
                                    {
                                        //Get version service
                                        string apiVersion = r.Split('|')[1].Replace(" ", "");

                                        //Get status service
                                        command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S pm2 ls";
                                        command = client.CreateCommand(command_exc);
                                        string apiStatus = command.Execute();

                                        //Add to list
                                        //serviceVersions.Add($"{apiVersion} [{apiStatus}]");
                                        serviceVersions.Add(new KeyValuePair<string, string>("api", $"{apiVersion} [{apiStatus}]"));
                                    }
                                }
                            }

                            ResultStatus(p, serviceVersions, "-");

                            //Update progressBar
                            int type = 3;
                            int percent_ProgressBar = int.Parse(p.id_station);
                            int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                            update_percentStatus.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                        }
                    }
                    catch (Exception ex)//Error or can't remote lav 
                    {
                        //Check not work 
                        var serviceVersions = new List<KeyValuePair<string, string>>();
                        serviceVersions.Add(new KeyValuePair<string, string>("dispenser", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("tankgauge", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("gaia", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("monitor", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("webconfig", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("websupport", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("lavupdate", $"-"));
                        serviceVersions.Add(new KeyValuePair<string, string>("api", $"-"));
                        ResultStatus(p, serviceVersions, ex.Message);

                        //Update progressBar
                        int type = 3;
                        int percent_ProgressBar = int.Parse(p.id_station);
                        int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                        update_percentStatus.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                    }
                }

                reportStatus();
            }
            catch (Exception ex)//Error
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_percentStatus_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int type = e.ProgressPercentage;
            var args = (Tuple<int, int>)e.UserState;
            string percent_Label = args.Item1.ToString();
            int percent_ProgressBar = int.Parse(args.Item2.ToString());
            progressBar3.Value = percent_ProgressBar;
            label12.Text = percent_Label;
        }

        private void update_percentStatus_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Process get status file successfull,\npress recheck report file at {Directory.GetCurrentDirectory()}\\Reports\\statusFile\\Report_{dateTime}.xlsx", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //Button deploy file
        private void button4_Click(object sender, EventArgs e)
        {
            Result_Deploy.Clear();
            DialogResult resultPopup_1 = MessageBox.Show("Do you want start process  deploy file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultPopup_1 == DialogResult.Yes)
            {
                //Have path file?
                if (textBox1.Text.Contains('\\') && textBox2.Text.Contains('\\') && textBox3.Text.Contains('\\'))
                {
                    update_percentDeploy.RunWorkerAsync();
                    return;
                }
                MessageBox.Show("Press recheck import station or transfer file.", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void update_percentDeploy_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (var p in pump_stations)
                {
                    try
                    {
                        string USERNAME = "lavender";
                        string PASSWORD = "muj,nv,ufvdw,h";
                        int PORT = 22;
                        var IP_ADDRESS = p.ip_sim;
                        var nameFileUpdate = (textBox2.Text).Replace(" ", "").Split('\\').LastOrDefault();
                        SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S ls /home/lavender/";
                            var command = client.CreateCommand(command_exc);
                            var result = command.Execute();

                            //Do have file yes or no?
                            if (result.Contains(nameFileUpdate)) //Have file 
                            {
                                //Extract file
                                command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S tar -xzf /home/lavender/" + nameFileUpdate;
                                command = client.CreateCommand(command_exc);
                                result = command.Execute();

                                command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S ls /home/lavender/";
                                command = client.CreateCommand(command_exc);
                                result = command.Execute();

                                //Check extract file ?
                                if (result.Contains(nameFileUpdate.Replace(".tar.gz",""))) //extract file success
                                {

                                    command_exc = $"echo 'muj,nv,ufvdw,h' | sudo -S dotnet /home/lavender/{nameFileUpdate.Replace(".tar.gz", "")}/{nameFileUpdate.Replace(".tar.gz", ".dll")}";
                                    command = client.CreateCommand(command_exc);
                                    result = command.Execute(); 

                                    //Deploy Success
                                    ResultDeploy(p, "Success", "-");

                                    //Update progressBar
                                    int type = 2;
                                    int percent_ProgressBar = int.Parse(p.id_station);
                                    int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                    update_percentDeploy.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                                }
                                else //extract file unsuccess
                                {
                                    //Deploy Unsuccess
                                    ResultDeploy(p, "Unsuccess", "Extract file unsuccess");

                                    //Update progressBar
                                    int type = 2;
                                    int percent_ProgressBar = int.Parse(p.id_station);
                                    int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                    update_percentDeploy.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                                }
                            }
                            else //Don't have file
                            {
                                //Deploy Unsuccess 
                                ResultDeploy(p, "Unsuccess", "Don't have file");

                                //Update progressBar
                                int type = 2;
                                int percent_ProgressBar = int.Parse(p.id_station);
                                int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                                update_percentDeploy.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                            }
                        }
                    }
                    catch (Exception ex)//Error or can't remote lav 
                    {
                        //Deploy Unsuccess 
                        ResultDelete(p, "Unsuccess", ex.Message);

                        //Deploy progressBar
                        int type = 2;
                        int percent_ProgressBar = int.Parse(p.id_station);
                        int percent_Label = (int.Parse(p.id_station.Replace(" ", "")) * 100) / pump_stations.Count();
                        update_percentDelete.ReportProgress(type, new Tuple<int, int>(percent_Label, percent_ProgressBar));
                    }
                }

                reportDeploy();
            }
            catch (Exception ex)//Error
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_percentDeploy_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int type = e.ProgressPercentage;
            var args = (Tuple<int, int>)e.UserState;
            string percent_Label = args.Item1.ToString();
            int percent_ProgressBar = int.Parse(args.Item2.ToString());
            progressBar2.Value = percent_ProgressBar;
            label11.Text = percent_Label;
        }

        private void update_percentDeploy_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Process deploy file successfull,\npress recheck report file at {Directory.GetCurrentDirectory()}\\Reports\\deployFile\\Report_{dateTime}.xlsx", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
