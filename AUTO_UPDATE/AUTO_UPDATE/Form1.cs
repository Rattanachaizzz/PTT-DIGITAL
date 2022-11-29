using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ganss.Excel;
using Renci.SshNet;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using Microsoft.Office.Core;
using System.Data;

namespace AUTO_UPDATE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
#if DEBUG
        public string token = "CkZQSE4uG27BeEtbw0sIjRTDrPwECM9W9Zdu2nud7SX ";
#else
        public string token = "0FXVmWTALncaj474rRv2r5X3pa6AZKYHEe2oVhOPiIs ";
#endif
        public DateTime DateTime = DateTime.Now;
        public string username = "lavender";
        public string password = "muj,nv,ufvdw,h";
        public int port = 22;
        #region local_Transfer_file
        public string localPath_LavenderDispenser = Directory.GetCurrentDirectory() + @"\LavenderDispenser\File_Transfer";
        public string localPath_LavenderTankgauge = Directory.GetCurrentDirectory() + @"\LavenderTankgauge\File_Transfer";
        public string localPath_LavenderMonitor = Directory.GetCurrentDirectory() + @"\LavenderMonitor\File_Transfer";
        public string localPath_LavenderConfig = Directory.GetCurrentDirectory() + @"\LavenderConfig\File_Transfer";
        public string localPath_LavenderAPI = Directory.GetCurrentDirectory() + @"\LavenderAPI\File_Transfer";
        public string localPath_LavenderGAIA = Directory.GetCurrentDirectory() + @"\LavenderGAIA\File_Transfer";
        public string localPath_LavenderOther = Directory.GetCurrentDirectory() + @"\LavenderOther\File_Transfer";
        #endregion
        #region local_MD5_file
        public string localPath_MD5_LavenderDispenser = Directory.GetCurrentDirectory() + @"\LavenderDispenser\MD5\KEY.txt";
        public string localPath_MD5_LavenderTankgauge = Directory.GetCurrentDirectory() + @"\LavenderTankgauge\MD5\KEY.txt";
        public string localPath_MD5_LavenderMonitor = Directory.GetCurrentDirectory() + @"\LavenderMonitor\MD5\KEY.txt";
        public string localPath_MD5_LavenderConfig = Directory.GetCurrentDirectory() + @"\LavenderConfig\MD5\KEY.txt";
        public string localPath_MD5_LavenderAPI = Directory.GetCurrentDirectory() + @"\LavenderAPI\MD5\KEY.txt";
        public string localPath_MD5_LavenderGAIA = Directory.GetCurrentDirectory() + @"\LavenderGAIA\MD5\KEY.txt";
        public string localPath_MD5_LavenderOther = Directory.GetCurrentDirectory() + @"\LavenderOther\MD5\KEY.txt";
        #endregion
        public List<Config_Stations> config_stations = new List<Config_Stations>();
        public List<string> Result_Status_Transfer = new List<string>();
        public List<string> Result_Status_Update = new List<string>();
        public int Result_Sum_Transfer_Success;
        public int Result_Sum_Update_Success;
        public int Result_Station_Totals;
        public int Result_Select_Sum_Station = 0;
        public int Result_Select_Sum_Station_Success;
        public delegate void OnBntHandler(object sender, EventArgs e);
        public event OnBntHandler Bnt_Event;
        public void OnBnt_Event()
        {
            if (Bnt_Event != null)
                Bnt_Event(this, EventArgs.Empty);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void btnTransferFile_Click(object sender, EventArgs e)
        {
            if (circlelb2.BackColor != Color.Green)
            {
                DialogResult result = MessageBox.Show("Do you want start transfer file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (label5.Text == "")
                    {
                        MessageBox.Show("Press Import File Config", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        btnTransferFile.Text = "WAIT...";
                        btnTransferFile.Enabled = false;
                        Transfer_File();


                        this.Bnt_Event += new OnBntHandler(Callback_Transfer);
                        this.OnBnt_Event();
                    }
                }
            }
            else
            {
                MessageBox.Show("You Transfer is Successed!!!", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void btnUpdateFile_Click(object sender, EventArgs e)
        {
            if (circlelb1.BackColor == Color.Green && circlelb2.BackColor == Color.Green && circlelb3.BackColor != Color.Green && circlelb4.BackColor != Color.Green)
            {
                DialogResult result = MessageBox.Show("Do you want start update file?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.White;
                    }

                    if (label5.Text == "")
                    {
                        MessageBox.Show("Press Import File Config", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        btnUpdateFile.Text = "WAIT...";
                        btnUpdateFile.Enabled = false;
                        Update_File();
                    }
                }
            }
            else if (circlelb1.BackColor == Color.Green && circlelb2.BackColor != Color.Green && circlelb3.BackColor != Color.Green && circlelb4.BackColor != Color.Green)
            {
                MessageBox.Show("Must Transfer the file first.!!!", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (circlelb1.BackColor == Color.Green && circlelb2.BackColor == Color.Green && circlelb3.BackColor == Color.Green && circlelb4.BackColor == Color.Green)
            {
                MessageBox.Show("Process is Succesed or press recheck config.!!!", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                btnTransferFile.Enabled = true;
                btnUpdateFile.Enabled = true;
                OpenFileDialog choofdlog = new OpenFileDialog();
                choofdlog.Filter = "All Files (*.*)|*.*";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = true;
                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    circlelb1.BackColor = Color.Green;
                    dataGridView1.Rows.Clear();
                    config_stations.Clear();
                    Result_Status_Transfer.Clear();


                    string sFileName = choofdlog.FileName;
                    label5.Text = sFileName;
                    string path = sFileName;
                    var Config_Station = new ExcelMapper(path).Fetch<Config_Stations>();
                    foreach (var row in Config_Station)
                    {
                        Config_Stations config_station = new Config_Stations();
                        config_station.Station_id = row.Station_id;
                        config_station.Station_code = row.Station_code;
                        config_station.Station_name = row.Station_name;
                        config_station.Station_IP = row.Station_IP;
                        config_station.Dispenser = row.Dispenser;
                        config_station.Tankgauge = row.Tankgauge;
                        config_station.Monitor = row.Monitor;
                        config_station.Web = row.Web;
                        config_station.API = row.API;
                        config_station.Gaia = row.Gaia;
                        config_station.Other = row.Other;
                        config_station.Time_Transfer_File = row.Time_Transfer_File;
                        config_station.Time_Update_File = row.Time_Update_File;
                        config_station.Status_Transfer_File = row.Status_Transfer_File;
                        config_station.Status_Update_File = row.Status_Update_File;
                        config_stations.Add(config_station);
                    }
                    foreach (var row in config_stations)
                    {

                        row.Dispenser = row.Dispenser == "True" ? true.ToString() : false.ToString(); 
                        row.Tankgauge = row.Tankgauge == "True" ? true.ToString() : false.ToString();
                        row.Monitor = row.Monitor == "True" ? true.ToString() : false.ToString();
                        row.API = row.API == "True" ? true.ToString() : false.ToString();
                        row.Web = row.Web == "True" ? true.ToString() : false.ToString();
                        row.Gaia = row.Gaia == "True" ? true.ToString() : false.ToString();
                        row.Other = row.Other == "True" ? true.ToString() : false.ToString();
                        dataGridView1.Rows.Add(row.Station_id, row.Station_code, row.Station_name, row.Station_IP, row.Dispenser, row.Tankgauge, row.Monitor, row.Web, row.API, row.Gaia, row.Other, row.Time_Transfer_File, row.Time_Update_File);
                        if (row.Status_Update_File == "False")
                        {
                            Result_Select_Sum_Station = row.Dispenser == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.Tankgauge == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.Monitor == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.API == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.Web == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.Gaia == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                            Result_Select_Sum_Station = row.Other == "True" ? Result_Select_Sum_Station + 1 : Result_Select_Sum_Station;
                        }
                    }
                    Result_Station_Totals = 0;
                    Result_Sum_Transfer_Success = 0;
                    Result_Sum_Update_Success = 0;
                    foreach (var c in Config_Station)
                    {
                        Result_Status_Transfer.Add(c.Status_Transfer_File.ToUpper());
                        if (c.Status_Transfer_File.ToUpper() == "FALSE" || c.Status_Update_File.ToUpper() == "FALSE")
                        {
                            Result_Station_Totals++;
                        }
                    }

                    if (Result_Status_Transfer.Contains("FALSE"))
                    {
                        circlelb2.BackColor = Color.FromArgb(180, 180, 180);
                    }
                    else
                    {
                        circlelb2.BackColor = Color.Green;
                    }

                    foreach (var c in Config_Station)
                    {
                        Result_Status_Update.Add(c.Status_Update_File.ToUpper());
                    }
                    if (Result_Status_Update.Contains("FALSE"))
                    {
                        circlelb3.BackColor = Color.FromArgb(180, 180, 180);
                        circlelb4.BackColor = Color.FromArgb(180, 180, 180);
                    }
                    else
                    {
                        circlelb3.BackColor = Color.Green;
                        circlelb4.BackColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                circlelb1.BackColor = Color.FromArgb(180, 180, 180);
                circlelb2.BackColor = Color.FromArgb(180, 180, 180);
                circlelb3.BackColor = Color.FromArgb(180, 180, 180);
                circlelb4.BackColor = Color.FromArgb(180, 180, 180);
                MessageBox.Show(ex.ToString(), "MessageBox Waning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label5.Text = "";
            }
        }
        public void Callback_Transfer(object sender, EventArgs e)
        {
            btnTransferFile.Enabled = true;
            btnTransferFile.Text = "Transfer file";
            circlelb2.BackColor = Color.Green;
            lineNotify("Transfer File Successed");
            MessageBox.Show("Transfer File Success", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Bnt_Event = null;
        }
        public void Callback_Update()
        {
            btnUpdateFile.Invoke(new System.Action(() =>
            {
                lineNotify("Update File Successed");
                btnUpdateFile.Enabled = true;
                btnUpdateFile.Text = "Update file";
                circlelb3.BackColor = Color.Green;
                circlelb4.BackColor = Color.Green;
                DialogResult result = MessageBox.Show("Update File Success", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Thread thread = new Thread(() => Save_Status_Update_File_cl_Total());
                    thread.Start();
                }
            }));
        }
        public void Save_Status_Update_File_cl_Total()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isCheck_Dispenser = Convert.ToBoolean(dataGridView1.Rows[i].Cells[4].Value);
                bool isCheck_Tankgauge = Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value);
                bool isCheck_Moniter = Convert.ToBoolean(dataGridView1.Rows[i].Cells[6].Value);
                bool isCheck_Web = Convert.ToBoolean(dataGridView1.Rows[i].Cells[7].Value);
                bool isCheck_Api = Convert.ToBoolean(dataGridView1.Rows[i].Cells[8].Value);
                bool isCheck_Gaia = Convert.ToBoolean(dataGridView1.Rows[i].Cells[9].Value);
                bool isCheck_Othere = Convert.ToBoolean(dataGridView1.Rows[i].Cells[10].Value);
                if (isCheck_Dispenser == true)
                {
                    Color Color_Dispenser = dataGridView1.Rows[i].Cells[4].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 5, Color_Dispenser);
                }
                if (isCheck_Tankgauge == true)
                {
                    Color Color_Tankgauge = dataGridView1.Rows[i].Cells[5].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 6, Color_Tankgauge);
                }
                if (isCheck_Moniter == true)
                {
                    Color Color_Moniter = dataGridView1.Rows[i].Cells[6].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 7, Color_Moniter);
                }
                if (isCheck_Web == true)
                {
                    Color Color_Web = dataGridView1.Rows[i].Cells[7].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 8, Color_Web);
                }
                if (isCheck_Api == true)
                {
                    Color Color_Api = dataGridView1.Rows[i].Cells[8].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 9, Color_Api);
                }
                if (isCheck_Gaia == true)
                {
                    Color Color_Gaia = dataGridView1.Rows[i].Cells[9].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 10, Color_Gaia);
                }
                if (isCheck_Othere == true)
                {
                    Color Color_Othere = dataGridView1.Rows[i].Cells[10].Style.BackColor;
                    Save_Status_Transfer_File_cl(i + 2, 11, Color_Othere);
                }
            }
        }
        public void Save_Status_Transfer_File_cl(int id, int cl, Color color)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb;
            Excel.Worksheet ws;
            wb = excel.Workbooks.Open(label5.Text);
            ws = (Worksheet)wb.Worksheets[1];
            excel.Cells[id, cl].Interior.Color = Color.FromArgb(color.R, color.G, color.B);
            excel.Cells[id, cl].Font.Color = Color.FromArgb(color.R, color.G, color.B);
            wb.Save();
            wb.Close();
        }
        public void Save_Status_Transfer_File_cl(int id, int cl, Excel.XlRgbColor bg_color, Excel.XlRgbColor fb_color, string filePath)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb;
            Excel.Worksheet ws;
            wb = excel.Workbooks.Open(filePath);
            ws = (Worksheet)wb.Worksheets[1];
            excel.Cells[id + 1, cl].Interior.Color = bg_color;
            excel.Cells[id + 1, cl].Font.Color = fb_color;
            wb.Save();
            wb.Close();
        }
        public void Save_Status_Update_File_cl(int id, int cl, Color color)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb;
            Excel.Worksheet ws;
            wb = excel.Workbooks.Open(label5.Text);
            ws = (Worksheet)wb.Worksheets[1];
            excel.Cells[id, cl].Interior.Color = Color.FromArgb(color.R, color.G, color.B);
            excel.Cells[id, cl].Font.Color = Color.FromArgb(color.R, color.G, color.B);
            wb.Save();
            wb.Close();
        }
        public void Save_Status_Transfer_File(int id, string filePath)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb;
            Excel.Worksheet ws;
            wb = excel.Workbooks.Open(filePath);
            ws = (Worksheet)wb.Worksheets[1];
            ws.Cells[id + 1, 14] = "TRUE";
            wb.Save();
            wb.Close();
        }
        public void Save_Status_Update_File(int id, string filePath)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb;
            Excel.Worksheet ws;
            wb = excel.Workbooks.Open(filePath);
            ws = (Worksheet)wb.Worksheets[1];
            ws.Cells[id + 1, 15] = "TRUE";
            wb.Save();
            wb.Close();
        }
        public void Transfer_File()
        {
        agian:
            List<string> Result_Sum_Transfer = new List<string>();
            foreach (var c in config_stations)
            {
                if (c.Status_Transfer_File.ToUpper() == "FALSE")  //Check Status 
                {
                    int dd = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[0].Split('/')[0]);
                    int MM = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[0].Split('/')[1]);
                    int yy = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[0].Split('/')[2]);
                    int hh = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[1].Split(':')[0]);
                    int mm = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[1].Split(':')[1]);
                    int ss = Convert.ToInt32(c.Time_Transfer_File.ToString().Split('-')[1].Split(':')[2]);
                    DateTime Time_Transfer_File = new DateTime(yy, MM, dd, hh, mm, ss);
                    string Result_Upload;
                    if (Time_Transfer_File <= DateTime) //DateTime Check 
                    {
                        if (c.Station_id == "30" || c.Station_id == "60" || c.Station_id == "90" || c.Station_id == "120" || c.Station_id == "150" || c.Station_id == "180")
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = int.Parse(c.Station_id) + 1; //dataGridView1.RowCount - 1
                        }
                        if (c.Dispenser == "True") //Transfer Normal
                        {
                            Console.WriteLine($"{c.Station_IP} : dispenser");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderDispenser, localPath_MD5_LavenderDispenser, "LavenderDispenser.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 5, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[4].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 5, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[4].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.Tankgauge == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : tankgauge");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderTankgauge, localPath_MD5_LavenderTankgauge, "LavenderTankgauge.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 6, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[5].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 6, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[5].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.Monitor == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : moniter");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderMonitor, localPath_MD5_LavenderMonitor, "LavenderMonitor.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 7, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[6].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 7, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[6].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.Web == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : web");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderConfig, localPath_MD5_LavenderConfig, "LavenderConfig.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 8, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[7].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 8, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[7].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.API == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : API");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderAPI, localPath_MD5_LavenderAPI, "LavenderAPI.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 9, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[8].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 9, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[8].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.Gaia == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : gaia");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderGAIA, localPath_MD5_LavenderGAIA, "LavenderGAIA.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 10, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[9].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 10, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[9].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        if (c.Other == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : gaia");
                            Result_Upload = Upload(c.Station_IP, port, username, password, localPath_LavenderOther, localPath_MD5_LavenderOther, "LavenderOther.tar.gz");
                            if (Result_Upload == "True")
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbYellow;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbYellow;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 11, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[10].Style.BackColor = Color.Yellow);
                                thread.Start();
                            }
                            else
                            {
                                Excel.XlRgbColor bg_color = Excel.XlRgbColor.rgbRed;
                                Excel.XlRgbColor fb_color = Excel.XlRgbColor.rgbRed;
                                Save_Status_Transfer_File_cl(int.Parse(c.Station_id), 11, bg_color, fb_color, label5.Text);
                                Thread thread = new Thread(() => dataGridView1.Rows[int.Parse(c.Station_id) - 1].Cells[10].Style.BackColor = Color.Red);
                                thread.Start();
                            }
                            Thread.Sleep(1000);
                        }
                        Save_Status_Transfer_File(int.Parse(c.Station_id), label5.Text);
                    }
                }
            }
            string path = label5.Text;
            var Config_Station = new ExcelMapper(path).Fetch<Config_Stations>();
            foreach (var c in Config_Station)
            {
                Result_Sum_Transfer.Add(c.Status_Transfer_File.ToUpper());
            }
            if (Result_Sum_Transfer.Contains("FALSE"))
            {
                goto agian;
            }
        }
        public void Update_File()
        {
        agian:
            List<string> Result_Sum_Update = new List<string>();
            foreach (var c in config_stations)
            {
                if (c.Status_Update_File.ToUpper() == "FALSE")  //Check Status 
                {
                    int dd = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[0].Split('/')[0]);
                    int MM = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[0].Split('/')[1]);
                    int yy = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[0].Split('/')[2]);
                    int hh = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[1].Split(':')[0]);
                    int mm = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[1].Split(':')[1]);
                    int ss = Convert.ToInt32(c.Time_Update_File.ToString().Split('-')[1].Split(':')[2]);
                    DateTime Time_Update_File = new DateTime(yy, MM, dd, hh, mm, ss);
                    string Result_Update;
                    if (Time_Update_File <= DateTime) //DateTime Check 
                    {
                        if (c.Station_id == "30" || c.Station_id == "60" || c.Station_id == "90" || c.Station_id == "120" || c.Station_id == "150" || c.Station_id == "180")
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = int.Parse(c.Station_id) + 1; //dataGridView1.RowCount - 1
                        }
                        if (c.Dispenser == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : dispenser");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderDispenser", c.Station_id, "4"));
                            thread.Start();
                        }
                        if (c.Tankgauge == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : tankgauge");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderTankgauge", c.Station_id, "5"));
                            thread.Start();
                        }
                        if (c.Monitor == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : moniter");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderMonitor", c.Station_id, "6"));
                            thread.Start();
                        }
                        if (c.Web == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : web");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderWebConfig", c.Station_id, "7"));
                            thread.Start();
                        }
                        if (c.API == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : API");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderAPI", c.Station_id, "8"));
                            thread.Start();
                        }
                        if (c.Gaia == "True")
                        {
                            Console.WriteLine($"{c.Station_IP} : gaia");
                            Thread thread = new Thread(() => Update(c.Station_IP, port, username, password, "LavenderGAIA", c.Station_id, "9"));
                            thread.Start();
                        }
                        Save_Status_Update_File(int.Parse(c.Station_id), label5.Text);
                    }
                    Thread.Sleep(1000);
                }
            }
            string path = label5.Text;
            var Config_Station = new ExcelMapper(path).Fetch<Config_Stations>();
            foreach (var cs in Config_Station)
            {
                Result_Sum_Update.Add(cs.Status_Update_File.ToUpper());
            }
            if (Result_Sum_Update.Contains("FALSE"))
            {
                goto agian;
            }
        }
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
                    if (fileName_Linux == "LavenderAPI.tar.gz")
                    {
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r dailCreate.sh");
                        command.Execute();
                        command = client_rm.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r DeleteLogDB.sh");
                        command.Execute();
                    }
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
        public void Update(string IP_Address, int port, string username, string password, string NAME, string Station_id, string Cell)
        {
            string key = "";
            int count = 0;
            string Service_version_new = "";
            string Sevice_version_old = "";
#if DEBUG
            string web_config = "http://10.195.2.64/lavenderconfig";
#else
            string web_config = "http://192.168.201/lavenderconfig";
#endif
            try
            {
                switch (NAME)
                {
                    case "LavenderDispenser":
                        key = "Dispenser_Version";
                        break;
                    case "LavenderTankgauge":
                        key = "Tank_Gauge_Version";
                        break;
                    case "LavenderMonitor":
                        key = "Monitor_Version";
                        break;
                    case "LavenderWebConfig":
                        key = "WebConfig_Version";
                        break;
                    case "LavenderAPI":
                        key = "API_Version";
                        break;
                    case "LavenderGAIA":
                        key = "GAIA_Version";
                        break;
                }
                char dubleCode = '"';
                SshClient client = new SshClient(IP_Address, port, username, password);
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand("ls");
                    var result = command.Execute();

                    if (NAME == "LavenderDispenser" || NAME == "LavenderTankgauge" || NAME == "LavenderMonitor")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/{NAME}_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/{NAME}.tar.gz");
                        result = command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/{NAME} /usr/share/{NAME}_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/{NAME}.tar.gz -C /usr/share/");
                        command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r {NAME}.tar.gz");
                        command.Execute();
                    }
                    else if (NAME == "LavenderAPI")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 stop all");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/{NAME}_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /usr/share/{NAME}.tar.gz");
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

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /usr/share/{NAME} /usr/share/{NAME}_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/{NAME}.tar.gz -C /usr/share/");
                        command.Execute();

                        //Move File bash
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/dailCreate.sh /root/AutomateScript/");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/DeleteLogDB.sh /root/AutomateScript/");
                        result = command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S pm2 start all");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r {NAME}.tar.gz");
                        command.Execute();
                    }
                    else if (NAME == "LavenderWebConfig")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{NAME.Replace("Web", "")}_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{NAME.Replace("Web", "")}.tar.gz");
                        result = command.Execute();

                        //Get Version Old
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key}'" + dubleCode);
                        result = command.Execute();
                        Sevice_version_old = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /var/www/{NAME.Replace("Web", "")} /var/www/{NAME.Replace("Web", "")}_BK_V{Sevice_version_old.Replace(".", "")}");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/{NAME.Replace("Web", "")}.tar.gz -C /var/www/");
                        command.Execute();

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        command.Execute();

                        //Delete File Transfer
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r {NAME.Replace("Web", "")}.tar.gz");
                        command.Execute();

                        //CallWeb
                        if (NAME == "LavenderWebConfig")
                        {
                            Thread.Sleep(3000);
                            command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S curl {web_config}");
                            result = command.Execute();
                        }
                    }
                    else if (NAME == "LavenderWebSupport")
                    {
                        //Stop Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl stop lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        result = command.Execute();

                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{NAME.Replace("Web", "")}_*");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /var/www/{NAME.Replace("Web", "")}.tar.gz");
                        result = command.Execute();

                        //BackUp lavenderXXX >>> lavenderXXX_BK_V[Version Old]
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /var/www/{NAME.Replace("Web", "")} /var/www/{NAME.Replace("Web", "")}_BK_V100");
                        command.Execute();

                        //Unzip File 
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S  tar -xzf /home/lavender/{NAME.Replace("Web", "")}.tar.gz -C /var/www/");
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
                        //reload nginx
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl reload nginx");
                        result = command.Execute();

                        //Server : Apache
                        //Move File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S mv /home/lavender/000-lavenderwebconfig.conf /etc/apache2/sites-available");
                        result = command.Execute();
                        //reload nginx
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl reload apache2");
                        result = command.Execute();

                        //Delete File
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/{NAME.Replace("Web", "")}.tar.gz");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/default");
                        result = command.Execute();
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S rm -r /home/lavender/000-lavenderwebconfig.conf");
                        result = command.Execute();

                        //Save Version
                        command = client.CreateCommand($"PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"INSERT INTO lavender.site_config(config_id, key, value, type) VALUES (49,'WebSupport_Version','1.0.0','string')" + dubleCode);
                        command.Execute();

                        goto end;
                    }
                    Thread.Sleep(5000);

                again:
                    //Get Version New
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"SELECT (key),(value) FROM lavender.site_config where key = '{key}'" + dubleCode);
                    result = command.Execute();
                    Service_version_new = result.Split('\n')[2].Split('|')[1].Replace(" ", "");

                    if (NAME == "LavenderDispenser")
                    {
                        if (Service_version_new == "1.10.5")
                        {
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"CREATE TABLE lavender.config_fleet_fraud  (pump_id integer NOT NULL,counting integer NOT NULL,time_waiting integer NOT NULL,duration integer NOT NULL,update_time timestamp without time zone NOT NULL,PRIMARY KEY (pump_id))" + dubleCode);
                            result = command.Execute();
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE lavender.config_fleet_fraud TO lav_application_role" + dubleCode);
                            result = command.Execute();
                        }
                        else if (Service_version_new == "1.10.4")
                        {
                            command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"DROP TABLE lavender.config_fleet_fraud" + dubleCode);
                            result = command.Execute();
                        }

                        //Start Service
                        command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S systemctl start lavender-{NAME.Replace("Lavender", "").ToLower()}");
                        command.Execute();
                    }
                    if (NAME == "LavenderAPI")
                    {
                        if (Service_version_new == "2.2.0")
                        {
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

                    //Check Version
                    count++;
                    if (count >= 3)
                    {
                        dataGridView1.Rows[int.Parse(Station_id) - 1].Cells[int.Parse(Cell)].Style.BackColor = Color.Red;
                        goto end;
                    }
                    if (Sevice_version_old != Service_version_new)
                    {
                        dataGridView1.Rows[int.Parse(Station_id) - 1].Cells[int.Parse(Cell)].Style.BackColor = Color.ForestGreen;
                    }
                    else
                    {
                        Thread.Sleep(5000);
                        goto again;
                    }
                end:
                    client.Dispose();
                }

                Result_Select_Sum_Station_Success++;
                Check_Result_Sum_Update_Success(Result_Select_Sum_Station, Result_Select_Sum_Station_Success);
            }
            catch (Exception ex)
            {
                Result_Select_Sum_Station_Success++;
                Check_Result_Sum_Update_Success( Result_Select_Sum_Station, Result_Select_Sum_Station_Success);
            }
        }
        public void Check_Result_Sum_Update_Success( int Result_Select_Sum_Station, int Result_Select_Sum_Station_Success)
        {
            if (Result_Select_Sum_Station == Result_Select_Sum_Station_Success)
            {
                    Callback_Update();
            }
        }
        public string Check_MD5_Hash(string Ip_Address, string fileName_Windows, string fileName_Linux)
        {
            try
            {
                string MD5_Windows = "";
                string MD5_Linux = "";

                MD5_Windows = File.ReadAllText(fileName_Windows);

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
        private void lineNotify(string msg)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void circlelb1_Click(object sender, EventArgs e)
        {
            SshClient client = new SshClient("10.195.2.64", port, username, password);
            client.Connect();
            if (client.IsConnected)
            {
                var command = client.CreateCommand($"echo 'muj,nv,ufvdw,h' | sudo -S CREATE TABLE lavender.config_fleet_fraud pump_id integer NOT NULL,(pump_id integer NOT NULL,counting integer NOT NULL,time_waiting integer NOT NULL,duration integer NOT NULL,update_time timestamp without time zone NOT NULL,PRIMARY KEY (pump_id));");
                var result = command.Execute();
            }
        }
    }
}
