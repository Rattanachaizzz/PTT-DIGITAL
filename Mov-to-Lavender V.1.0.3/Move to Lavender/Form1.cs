using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Move_to_Lavender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string ConnectionString = "";
        string exportPath = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();
            StreamReader r = new StreamReader(CurrentDirectory + @"\ConnectionString.json");
            string jsonString = r.ReadToEnd();
            JObject rss = JObject.Parse(jsonString);
            ConnectionString = (string)rss["ConnectionStrings"]["ConnectionString"];
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            label3.Text = "0";
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                backgroundWorker1.ReportProgress(i);
                Thread.Sleep(10);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.BeginInvoke(new Action(() =>
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            ));
            label1.BeginInvoke(new Action(() =>
            {
                label3.Text = e.ProgressPercentage.ToString();
            }
            ));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Export Sucessfull.", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            progressBar1.Value = 0;
            label3.Text = "0";
            exportPath = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            try
            {
                connect.Open();
            }
            catch
            {
                MessageBox.Show("Database connection unsuccessful.");
                System.Environment.Exit(1);
            }

            if (connect.State == ConnectionState.Open)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Products list|*.json";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //---------------------------------------------------Validation--------------------------------------------------------------
                    #region Validation
                    //tank_type
                    DataTable dataTable_Validation_tank_type = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Tank_Type_ID] FROM [ENABLERDB].[dbo].[Tanks]";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dataTable_Validation_tank_type.Load(reader);
                            dataTable_Validation_tank_type.Columns["Tank_Type_ID"].ColumnName = "tank_type_id";
                            foreach (DataRow row in dataTable_Validation_tank_type.Rows)
                            {
                                string xxxx = row["tank_type_id"].ToString();
                                //Console.WriteLine(row["tank_type"].ToString());
                                if (row["tank_type_id"].ToString() == "1" || row["tank_type_id"].ToString() == "2")
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Warning: Cannot exprot json file , please recheck configs databast Table[Tanks],colume[Tank_Type] and try again.", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    System.Environment.Exit(1);
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Tanks] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //---------------------------------------------------site_config-------------------------------------------------------------
                    #region site_config
                    var Global_Settings_Pump_Stack_Size = "";
                    DataTable dataTable_site_config = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Site_Name],[Authorized_Timeout],[Delivery_Idle_TO],[Delivery_Age_TO],[Pump_Stack_Size] FROM [ENABLERDB].[dbo].[Global_Settings]";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        //var Global_Settings_Pump_Stack_Size = "";
                        if (reader.HasRows)
                        {
                            dataTable_site_config.Load(reader);
                            foreach (DataRow row in dataTable_site_config.Rows)
                            {
                                foreach (DataColumn column in dataTable_site_config.Columns)
                                {
                                    string pump_stack_size = row["Pump_Stack_Size"].ToString();
                                    Global_Settings_Pump_Stack_Size = pump_stack_size;
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Global_Settings] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------------Grade----------------------------------------------------------------
                    #region Grade
                    DataTable dataTable_Grade = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Grade_ID]
                                  ,[Grade_Name]
                                  ,[Grade_Number]
                                  ,[Price_Profile_ID]
                              FROM [ENABLERDB].[dbo].[Grades] WHERE [ENABLERDB].[dbo].[Grades].Deleted = 0";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dataTable_Grade.Load(reader);
                            dataTable_Grade.Columns["Grade_ID"].ColumnName = "grade_id";
                            dataTable_Grade.Columns["Grade_Name"].ColumnName = "grade_name";
                            dataTable_Grade.Columns["Grade_Number"].ColumnName = "grade_number";
                            dataTable_Grade.Columns["Price_Profile_ID"].ColumnName = "price_profile_id";
                            dataTable_Grade.Columns.Add("product_color", typeof(System.String));
                        }
                        foreach (DataRow row in dataTable_Grade.Rows)
                        {
                            row["product_color"] = "";
                            row["grade_name"] = row["grade_name"].ToString().Replace(" ", String.Empty);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Grades] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //--------------------------------------------------price_profiles-----------------------------------------------------------
                    #region price_profiles  
                    dynamic dataTable_price_profiles = null;
                    try
                    {
                        DataTable dt_price_profiles = new DataTable();
                        string sqlText = @"SELECT [ENABLERDB].[dbo].[Grades].Price_Profile_ID                      
                                  ,[Grade_Name] 
                                  ,[Grade_Price]
                              FROM [ENABLERDB].[dbo].[Price_Levels] INNER JOIN [ENABLERDB].[dbo].[Grades] 
                              ON [ENABLERDB].[dbo].[Price_Levels].Price_Profile_ID = [ENABLERDB].[dbo].[Grades].Price_Profile_ID 
                              WHERE Price_Level =1 OR Price_Level = 2 ";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        DataTable dataTable_price_profiles1 = new DataTable();
                        if (reader.HasRows)
                        {
                            dt_price_profiles.Load(reader);
                            dt_price_profiles.Columns["Price_Profile_ID"].ColumnName = "profile_id";
                            dt_price_profiles.Columns["Grade_Name"].ColumnName = "profile_name";
                            dt_price_profiles.Columns["Grade_Price"].ColumnName = "grade_price";
                            dt_price_profiles.Columns.Add("enable", typeof(System.String));
                            dt_price_profiles.Columns.Add("date_update", typeof(System.DateTime));
                            dt_price_profiles.Columns.Add("price_level_2", typeof(System.Decimal));
                        }
                        foreach (DataRow row in dt_price_profiles.Rows)
                        {
                            row["profile_name"] = row["profile_name"].ToString().Replace(" ", String.Empty);
                        }
                        dataTable_price_profiles = from row in dt_price_profiles.AsEnumerable()
                                                   group row by row.Field<int>("profile_id") into Group
                                                   let row = Group.First()
                                                   select new
                                                   {
                                                       profile_id = row.Field<int>("profile_id"),
                                                       profile_name = row.Field<string>("profile_name"),
                                                       price_level_1 = (Group.Select(r => r.Field<decimal>("grade_price"))).First(),
                                                       price_level_2 = (Group.Select(r => r.Field<decimal>("grade_price"))).Last(),
                                                       enable = "true",
                                                       date_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                   };
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Grades] or Table[Price_Levels] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------------tanks----------------------------------------------------------------
                    #region Tanks
                    DataTable dataTable_Tanks = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Tank_ID],[Grade_ID],[Tank_Name],[Tank_Number],[Probe_Number],[Tank_Description],[Physical_Label],[Capacity],
                                        [Theoretical_Volume],[Diameter] ,[Tank_Type_ID],[Low_Volume_Warning],[Low_Volume_Alarm],[Hi_Volume_Warning] ,
                                        [Hi_Volume_Alarm],[Hi_Temperature],[Low_Temperature] ,[Hi_Water_Alarm],[Loss_Tolerance_Vol],[Gain_Tolerance_Vol] 
                                   FROM [ENABLERDB].[dbo].[Tanks]  WHERE [ENABLERDB].[dbo].[Tanks].Deleted = 0";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        dataTable_Tanks.Columns.Add("tank_id", typeof(System.Int32));
                        if (reader.HasRows)
                        {
                            dataTable_Tanks.Load(reader);
                            dataTable_Tanks.Columns["Tank_ID"].ColumnName = "tank_id";
                            dataTable_Tanks.Columns["Grade_ID"].ColumnName = "grade_id";
                            dataTable_Tanks.Columns["Tank_Name"].ColumnName = "tank_name";
                            dataTable_Tanks.Columns["Tank_Number"].ColumnName = "tank_number";
                            dataTable_Tanks.Columns["Probe_Number"].ColumnName = "probe_number";
                            dataTable_Tanks.Columns["Tank_Description"].ColumnName = "tank_description";
                            dataTable_Tanks.Columns["Physical_Label"].ColumnName = "physical_label";
                            dataTable_Tanks.Columns["Capacity"].ColumnName = "capacity";
                            dataTable_Tanks.Columns["Theoretical_Volume"].ColumnName = "theoretical_volume";
                            dataTable_Tanks.Columns["Diameter"].ColumnName = "diameter";
                            dataTable_Tanks.Columns["Tank_Type_ID"].ColumnName = "tank_type";
                            dataTable_Tanks.Columns["Low_Volume_Warning"].ColumnName = "low_volume_warning";
                            dataTable_Tanks.Columns["Low_Volume_Alarm"].ColumnName = "low_volume_alarm";
                            dataTable_Tanks.Columns["Hi_Volume_Warning"].ColumnName = "high_volume_warning";
                            dataTable_Tanks.Columns["Hi_Volume_Alarm"].ColumnName = "high_volume_alarm";
                            dataTable_Tanks.Columns["Hi_Temperature"].ColumnName = "high_temp_alarm";
                            dataTable_Tanks.Columns["Low_Temperature"].ColumnName = "low_temp_alarm";
                            dataTable_Tanks.Columns["Hi_Water_Alarm"].ColumnName = "high_water_alarm";
                            dataTable_Tanks.Columns["Loss_Tolerance_Vol"].ColumnName = "loss_tolerance_volume";
                            dataTable_Tanks.Columns["Gain_Tolerance_Vol"].ColumnName = "gain_tolerance_volume";
                            dataTable_Tanks.Columns.Add("high_water_warning", typeof(System.Decimal));
                            foreach (DataRow row in dataTable_Tanks.Rows)
                            {
                                row["tank_description"] = row["tank_description"].ToString().Replace(" ", String.Empty);
                                row["physical_label"] = row["physical_label"].ToString().Replace(" ", String.Empty);
                                row["tank_name"] = row["tank_name"].ToString();
                                row["high_water_warning"] = row["high_water_alarm"];
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Tanks] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------------loops----------------------------------------------------------------
                    #region loops
                    DataTable dataTable_loops = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Loop_ID]
                                                  ,[Port_Name]
                                                  ,[Protocol_Type_ID]
                                                  ,[Protocol_Name] 
                                      FROM [ENABLERDB].[dbo].[Loops] INNER JOIN [ENABLERDB].[dbo].[Pump_Protocol]
                                      ON [ENABLERDB].[dbo].[Loops].Protocol_ID = [ENABLERDB].[dbo].[Pump_Protocol].Protocol_ID 
                                      INNER JOIN [ENABLERDB].[dbo].[Pump_Type] 
                                      ON [ENABLERDB].[dbo].[Pump_Protocol].Protocol_ID = [ENABLERDB].[dbo].[Pump_Type].Pump_Type_ID
                                      WHERE [ENABLERDB].[dbo].[Pump_Protocol].Protocol_Type_ID = 1 OR [ENABLERDB].[dbo].[Pump_Protocol].Protocol_Type_ID = 2 ";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        dataTable_loops.Columns.Add("loop_id", typeof(System.Int32));
                        if (reader.HasRows)
                        {
                            dataTable_loops.Load(reader);
                            dataTable_loops.Columns["Port_Name"].ColumnName = "loop_name";
                            dataTable_loops.Columns.Add("protocol_id", typeof(System.Int32));
                            dataTable_loops.Columns.Add("baudrate", typeof(System.Int32));
                            dataTable_loops.Columns.Add("read_timeout", typeof(System.Int32));
                            dataTable_loops.Columns.Add("readdata_timeout", typeof(System.Int32));
                            dataTable_loops.Columns.Add("time_delay", typeof(System.Int32));
                            foreach (DataRow row in dataTable_loops.Rows)
                            {
                                row["loop_name"] = row["loop_name"].ToString().Replace(" ", String.Empty);

                            }
                            dataTable_loops.DefaultView.Sort = "loop_name ASC";
                            dataTable_loops = dataTable_loops.DefaultView.ToTable();
                            int i = 1;
                            int j = 1;
                            foreach (DataRow row in dataTable_loops.Rows)
                            {
                                row["loop_name"] = row["loop_name"].ToString().Replace(" ", String.Empty);
                                string loop_name = row["loop_name"].ToString();
                                string loop_type = row["Protocol_Type_ID"].ToString();

                                if (loop_type == "1")
                                {
                                    row["Protocol_Type_ID"] = 0;
                                    row["loop_name"] = "LAV-LOOP " + i.ToString();
                                    string protocol_id = row["Protocol_Name"].ToString();
                                    string[] word = protocol_id.Split(' ');
                                    string Gilbarco_bool = (word.Any(s => s.Contains("Gilbarco"))).ToString(); //True,False
                                    string Tatsuno_bool = (word.Any(s => s.Contains("Tatsuno"))).ToString(); //True,False
                                    string Wayne_bool = (word.Any(s => s.Contains("Wayne"))).ToString(); //True,False
                                    if (Gilbarco_bool == "True") //Gilbarco
                                    {
                                        row["baudrate"] = 5787;
                                        row["read_timeout"] = 300;
                                        row["readdata_timeout"] = 800;
                                        row["time_delay"] = 80;
                                        row["protocol_id"] = 1;
                                    }
                                    else if (Tatsuno_bool == "True") //Tatsuno_bool 
                                    {
                                        row["baudrate"] = 19200;
                                        row["read_timeout"] = 300;
                                        row["readdata_timeout"] = 800;
                                        row["time_delay"] = 20;
                                        row["protocol_id"] = 3;
                                    }
                                    else if (Wayne_bool == "True") //Wayne
                                    {
                                        row["baudrate"] = 9600;
                                        row["read_timeout"] = 300;
                                        row["readdata_timeout"] = 800;
                                        row["time_delay"] = 40;
                                        row["protocol_id"] = 6;
                                    }
                                    i++;
                                }
                                else if (loop_type == "2") //ATG
                                {
                                    row["Protocol_Type_ID"] = 2;
                                    row["loop_name"] = "COM " + j.ToString();
                                    row["baudrate"] = 9600;
                                    row["read_timeout"] = 700;
                                    row["readdata_timeout"] = 1500;
                                    row["time_delay"] = 1000;
                                    row["protocol_id"] = 2;
                                    j++;
                                }
                            }
                            dataTable_loops.Columns["Protocol_Type_ID"].ColumnName = "loop_type";
                            dataTable_loops.Columns.Remove("Protocol_Name");
                        }
                        dataTable_loops.DefaultView.Sort = "loop_id DESC";
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Loops],Table[Pump_Protocol] or Table[Pump_Type] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------------pumps----------------------------------------------------------------
                    #region pumps
                    DataTable dataTable_pumps = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Pump_ID]
                              ,[Loop_ID]
                              ,[Pump_Type_Name]
                              ,[Pump_Display_ID]
                              ,[Pump_Name]
                              ,[Logical_Number]
                              ,[Polling_Address]
                          FROM [ENABLERDB].[dbo].[Pumps] INNER JOIN [ENABLERDB].[dbo].[Pump_Type] 
                          ON [ENABLERDB].[dbo].[Pumps].Pump_Type_ID = [ENABLERDB].[dbo].[Pump_Type].Pump_Type_ID WHERE [ENABLERDB].[dbo].[Pumps].Deleted = 0";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dataTable_pumps.Load(reader);
                            dataTable_pumps.Columns["Pump_ID"].ColumnName = "pump_id";
                            dataTable_pumps.Columns["Loop_ID"].ColumnName = "loop_id";
                            dataTable_pumps.Columns["Pump_Type_Name"].ColumnName = "pump_type";
                            dataTable_pumps.Columns["Pump_Display_ID"].ColumnName = "pump_display_id";
                            dataTable_pumps.Columns["Pump_Name"].ColumnName = "pump_name";
                            dataTable_pumps.Columns["Logical_Number"].ColumnName = "logical_address";
                            dataTable_pumps.Columns["Polling_Address"].ColumnName = "physical_address";
                            dataTable_pumps.Columns.Add("stack_limit", typeof(System.Int32));
                            dataTable_pumps.Columns.Add("auto_authorize", typeof(System.String));
                            dataTable_pumps.Columns.Add("self_mode", typeof(System.String));
                            foreach (DataRow row in dataTable_pumps.Rows)
                            {
                                row["pump_name"] = row["pump_name"].ToString().Replace(" ", String.Empty);
                                row["pump_name"] = row["pump_name"].ToString().Replace(" ", String.Empty);
                                row["stack_limit"] = Convert.ToInt32(Global_Settings_Pump_Stack_Size) + 1;
                                row["auto_authorize"] = "false";
                                row["self_mode"] = "true";
                                row["pump_display_id"] = row["pump_id"];
                                string pump_type = row["pump_type"].ToString();
                                Console.WriteLine(pump_type);
                                string Gilbarco = "Gilbarco";
                                string[] word = pump_type.Split(' ');
                                string Gilbarco_bool = (word.Any(s => s.Contains(Gilbarco))).ToString(); //True,False
                                string digit_bool1 = (word.Any(s => s.Contains("(5"))).ToString(); //True,False
                                string digit_bool2 = (word.Any(s => s.Contains("(6"))).ToString(); //True,False
                                if (Gilbarco_bool == "True")
                                {
                                    if (digit_bool1 == "True")
                                    {
                                        row["pump_type"] = "5";
                                    }
                                    if (digit_bool2 == "True")
                                    {
                                        row["pump_type"] = "6";
                                    }
                                }
                                else
                                {
                                    row["pump_type"] = "5";
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Pump_Type],Table[Pumps] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //-------------------------------------------------advances_setting-----------------------------------------------------------
                    #region advances_setting
                    DataTable dataTable_advances_setting = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Pump_ID] FROM [ENABLERDB].[dbo].[Pumps]";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        var Global_Pump_Description = "";
                        if (reader.HasRows)
                        {
                            dataTable_advances_setting.Load(reader);
                            dataTable_advances_setting.Columns["Pump_ID"].ColumnName = "advance_id";
                            dataTable_advances_setting.Columns.Add("pump_id", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_preset_volume", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_preset_value", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_transaction_volume", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_transaction_value", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_total_volume", typeof(System.Int32));
                            dataTable_advances_setting.Columns.Add("multipier_total_value", typeof(System.Int32));
                            foreach (DataRow row in dataTable_advances_setting.Rows)
                            {
                                foreach (DataColumn column in dataTable_advances_setting.Columns) //Vol_T3
                                {
                                    string multipier_total_volume = row["multipier_total_volume"].ToString();
                                    Global_Pump_Description = multipier_total_volume;
                                }
                            }
                            foreach (DataRow row in dataTable_advances_setting.Rows)
                            {
                                row["pump_id"] = row["advance_id"];
                                row["multipier_preset_volume"] = 1;
                                row["multipier_preset_value"] = 1;
                                row["multipier_preset_volume"] = 1;
                                row["multipier_transaction_volume"] = 1;
                                row["multipier_transaction_value"] = 1;
                                row["multipier_total_volume"] = 1;
                                row["multipier_total_value"] = 1;
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Pumps] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //--------------------------------------------------pumps_display-------------------------------------------------------------
                    #region pumps_display
                    DataTable dataTable_pumps_display = new DataTable();
                    try
                    {   //Gillbarco : ตาม Display + Condition
                        //Tutsuno   : ตาม Display
                        //Wyne      : ตาม Display ***but value = 2 digit ,valume = 3 digit***

                        string sqlText = @"SELECT TOP 1000 [Pump_ID]  
                              ,[Pump_Value_Format]
                              ,[Pump_Volume_Format]
                              ,[Pump_Price_Format]
                              ,[Pump_Description]
                              ,[Protocol_Name]
                          FROM [ENABLERDB].[dbo].[Pump_Display] INNER JOIN [ENABLERDB].[dbo].[Pumps]
                          ON  [ENABLERDB].[dbo].[Pump_Display].Pump_Display_ID = [ENABLERDB].[dbo].[Pumps].Pump_Display_ID
                          INNER JOIN [ENABLERDB].[dbo].[Pump_Type]
                          ON  [ENABLERDB].[dbo].[Pumps].Pump_Type_ID = [ENABLERDB].[dbo].[Pump_Type].Pump_Type_ID
                          INNER JOIN [ENABLERDB].[dbo].[Pump_Protocol]
                          ON  [ENABLERDB].[dbo].[Pump_Type].Protocol_ID = [ENABLERDB].[dbo].[Pump_Protocol].Protocol_ID";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dataTable_pumps_display.Load(reader);
                            dataTable_pumps_display.Columns.Add("display_id", typeof(System.Int32));
                            dataTable_pumps_display.Columns.Add("value_decimal_digit", typeof(System.Int32));
                            dataTable_pumps_display.Columns.Add("volume_decimal_digit", typeof(System.Int32));
                            dataTable_pumps_display.Columns.Add("price_decimal_digit", typeof(System.Int32));
                            dataTable_pumps_display.Columns.Add("value_total_decimal_digit", typeof(System.Int32));
                            dataTable_pumps_display.Columns.Add("volume_total_decimal_digit", typeof(System.Int32));
                            foreach (DataRow row in dataTable_pumps_display.Rows)
                            {
                                row["display_id"] = row["Pump_ID"];

                                string Protocol_Name = row["Protocol_Name"].ToString().Replace(" ", String.Empty);

                                string point = ".";
                                string value_decimal_digit = row["Pump_Value_Format"].ToString().Replace(" ", String.Empty);
                                string[] word = value_decimal_digit.Split(' ');
                                string value_decimal_digit_bool = (word.Any(s => s.Contains(point))).ToString(); //True,False
                                if (value_decimal_digit_bool == "True")
                                {
                                    int length = value_decimal_digit.Substring(value_decimal_digit.IndexOf(".")).Length - 1;
                                    row["value_decimal_digit"] = length;
                                    row["value_total_decimal_digit"] = length;
                                }
                                else
                                {
                                    row["value_decimal_digit"] = 0;
                                    row["value_total_decimal_digit"] = 0;
                                }

                                string volume_decimal_digit = row["Pump_Volume_Format"].ToString().Replace(" ", String.Empty);
                                word = volume_decimal_digit.Split(' ');
                                string volume_decimal_digit_bool = (word.Any(s => s.Contains(point))).ToString(); //True,False
                                if (volume_decimal_digit_bool == "True")
                                {
                                    int length = volume_decimal_digit.Substring(volume_decimal_digit.IndexOf(".")).Length - 1;
                                    row["volume_decimal_digit"] = length;
                                }
                                else
                                {
                                    row["volume_decimal_digit"] = 0;
                                }

                                string price_decimal_digit = row["Pump_Price_Format"].ToString().Replace(" ", String.Empty);
                                word = price_decimal_digit.Split(' ');
                                string price_decimal_digit_bool = (word.Any(s => s.Contains(point))).ToString(); //True,False
                                if (price_decimal_digit_bool == "True")
                                {
                                    int length = price_decimal_digit.Substring(price_decimal_digit.IndexOf(".")).Length - 1;
                                    row["price_decimal_digit"] = length;
                                }
                                else
                                {
                                    row["price_decimal_digit"] = 0;
                                }

                                string Pump_Description = row["Pump_Description"].ToString().Replace(" ", String.Empty);
                                //string Protocol_Name = row["Protocol_Name"].ToString().Replace(" ", String.Empty);
                                string Gilbarco = "Gilbarco";
                                string[] Protocol_Name_world = Protocol_Name.Split(' ');
                                string Gilbarco_bool = (Protocol_Name_world.Any(s => s.Contains(Gilbarco))).ToString(); //True,False
                                if (Pump_Description == "")
                                {
                                    if (Gilbarco_bool == "True")
                                    {
                                        string volume_decimal_digit_data = row["volume_decimal_digit"].ToString();
                                        if (volume_decimal_digit_data == "3")
                                        {
                                            row["volume_total_decimal_digit"] = 2;
                                        }
                                        else
                                        {
                                            row["volume_total_decimal_digit"] = row["volume_decimal_digit"];
                                        }
                                    }
                                    else
                                    {
                                        row["volume_total_decimal_digit"] = row["volume_decimal_digit"];
                                    }
                                }
                                else
                                {
                                    if (Pump_Description.Length > 0)                      //Pump_Description = PRESET_VOL_DP=1;VOLUME_TOT_DP=2
                                    {
                                        string[] Pairs = Pump_Description.Split(';');     //Pairs = | PRESET_VOL_DP = 1| VOLUME_TOT_DP = 2|
                                        foreach (string Pair in Pairs)                    //Pair = PRESET_VOL_DP = 1
                                        {
                                            string[] Values = Pair.Split('=');            //Values = | PRESET_VOL_DP | 1 |
                                            if (Values[0].Replace(" ", "").ToUpper() == "VOLUME_TOT_DP" && Values.Length > 1)
                                            {
                                                row["volume_total_decimal_digit"] = Values[1];
                                            }
                                            else if (Values[0].Replace(" ", "").ToUpper() == "AMOUNT_TOT_DP" && Values.Length > 1)
                                            {
                                                row["value_total_decimal_digit"] = Values[1];
                                            }
                                            else
                                            {
                                                if (Gilbarco_bool == "True")
                                                {
                                                    string volume_decimal_digit_data = row["volume_decimal_digit"].ToString();
                                                    if (volume_decimal_digit_data == "3")
                                                    {
                                                        row["volume_total_decimal_digit"] = 2;
                                                    }
                                                    else
                                                    {
                                                        row["volume_total_decimal_digit"] = row["volume_decimal_digit"];
                                                    }
                                                }
                                                else
                                                {
                                                    row["volume_total_decimal_digit"] = row["volume_decimal_digit"];
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Protocol_Name.Substring(0, 1) == "W" || Protocol_Name.Substring(0, 1) == "w")
                                {
                                    row["value_total_decimal_digit"] = 2;
                                    row["volume_total_decimal_digit"] = 3;
                                }
                            }
                        }
                        dataTable_pumps_display.Columns.Remove("Pump_ID");
                        dataTable_pumps_display.Columns.Remove("Pump_Value_Format");
                        dataTable_pumps_display.Columns.Remove("Pump_Volume_Format");
                        dataTable_pumps_display.Columns.Remove("Pump_Price_Format");
                        dataTable_pumps_display.Columns.Remove("Pump_Description");
                        dataTable_pumps_display.Columns.Remove("Protocol_Name");
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Pumps],Table[Pump_Display],Table[Pump_Type],Table[Pump_Protocol] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------------hoses-----------------------------------------------------------------
                    #region hoses
                    DataTable dataTable_hoses = new DataTable();
                    try
                    {
                        string sqlText = @"SELECT [Hose_ID],[Pump_ID],[Hose_number],[ENABLERDB].[dbo].[Hoses].Grade_ID
                              ,[Price_Profile_ID],[Tank_ID],[Volume_Total],[Money_Total] ,[Mechanical_Total]
                          FROM [ENABLERDB].[dbo].[Hoses] INNER JOIN [ENABLERDB].[dbo].[Grades]
                          ON   [ENABLERDB].[dbo].[Hoses].Grade_ID = [ENABLERDB].[dbo].[Grades].Grade_ID 
                          WHERE [ENABLERDB].[dbo].[Hoses].Deleted = 0 ";
                        SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                        SqlDataReader reader = sqlSelect.ExecuteReader();
                        dataTable_hoses.Columns.Add("hose_id", typeof(System.Int32));
                        if (reader.HasRows)
                        {
                            dataTable_hoses.Load(reader);
                            dataTable_hoses.Columns["Hose_ID"].ColumnName = "hose_id";
                            dataTable_hoses.Columns["Pump_ID"].ColumnName = "pump_id";
                            dataTable_hoses.Columns["Hose_number"].ColumnName = "hose_number";
                            dataTable_hoses.Columns["Grade_ID"].ColumnName = "grade_id";
                            dataTable_hoses.Columns["Price_Profile_ID"].ColumnName = "price_profile_id";
                            dataTable_hoses.Columns["Tank_ID"].ColumnName = "tank_id";
                            dataTable_hoses.Columns["Volume_Total"].ColumnName = "total_meter_volume";
                            dataTable_hoses.Columns["Money_Total"].ColumnName = "total_meter_value";
                            dataTable_hoses.Columns["Mechanical_Total"].ColumnName = "total_meter_machanical";
                            dataTable_hoses.Columns.Add("active_price_level", typeof(System.Int32));
                            foreach (DataRow row in dataTable_hoses.Rows)
                            {
                                row["active_price_level"] = 1;
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Hoses] or Table[Grades] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //---------------------------------------------------transactions-------------------------------------------------------------
                    #region transactions_id
                    DataTable dataTable_transactions_id = new DataTable();
                    try
                    {
                        try
                        {
                            string sqlText = @"SELECT TOP 1 [Delivery_ID]  FROM [ENABLERDB].[dbo].[Hose_Delivery] ORDER BY [Delivery_ID] DESC";
                            SqlCommand sqlSelect = new SqlCommand(sqlText, connect);
                            SqlDataReader reader = sqlSelect.ExecuteReader();
                            if (reader.HasRows)
                            {
                                dataTable_transactions_id.Load(reader);
                                dataTable_transactions_id.Columns.Add("transactions_id", typeof(System.Int32));
                                foreach (DataRow row in dataTable_transactions_id.Rows)
                                {
                                    int transactions_id = Convert.ToInt32(row["Delivery_ID"]) + 1;
                                    row["transactions_id"] = transactions_id;
                                }
                            }
                            dataTable_transactions_id.Columns.Remove("Delivery_ID");
                            reader.Close();
                        }
                        catch
                        {
                            dataTable_transactions_id.Columns.Add("transactions_id", typeof(System.Int32));
                            DataRow row = dataTable_transactions_id.NewRow();
                            int transactions_id = 1;
                            row["transactions_id"] = transactions_id;
                            dataTable_transactions_id.Rows.Add(row);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: Please recheck configs databast Table[Hose_Delivery] and try again." + " [" + ex + "]", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }
                    #endregion
                    //------------------------------------------------tank_gauge_features----------------------------------------------------------
                    #region tank_gauge_features
                    dynamic dataTable_tank_gauge_features = null;
                    try
                    {
                        try
                        {
                            var versionInfo = FileVersionInfo.GetVersionInfo(@"C:\\PI-THUNDER\\ThunderServer.exe"); ;
                            var version = versionInfo.ProductVersion;
                            if (version == "1.7.1.0")
                            {
                                dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="true" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="true"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="true"},
                                                            new { feature_id = 4, protocol_id = 2,group = "Tank Delivery Clean",enable="true"},
                                                            new { feature_id = 5, protocol_id = 2,group = "Reconcilation",enable="true"},
                                                            new { feature_id = 6, protocol_id = 2,group = "GAIA",enable="false"}
                                                };
                            }
                            else if (version == "1.7.2.0")
                            {
                                dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="true" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="true"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="true"},
                                                            new { feature_id = 4, protocol_id = 2 ,group = "Tank Delivery Clean",enable="false"},
                                                            new { feature_id = 5, protocol_id = 2 ,group = "Reconcilation",enable="true"},
                                                            new { feature_id = 6, protocol_id = 2 ,group = "GAIA",enable="false"}
                                                };
                            }
                            else if (version == "1.7.6.1")
                            {
                                string[] lines = File.ReadAllLines(@"C:\PI-THUNDER\pi.ini");
                                string line = lines[15];
                                string[] ToF = line.Split('=');
                                Console.WriteLine(ToF[1]);
                                if (ToF[1] == "true") /// 1.7.1
                                {
                                    //Console.WriteLine("status : true");
                                    dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="true" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="true"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="true"},
                                                            new { feature_id = 4, protocol_id = 2,group = "Tank Delivery Clean",enable="true"},
                                                            new { feature_id = 5, protocol_id = 2,group = "Reconcilation",enable="true"},
                                                            new { feature_id = 6, protocol_id = 2,group = "GAIA",enable="false"}
                                                    };
                                }
                                else // 1.7.2
                                {
                                    //Console.WriteLine("status : false");
                                    dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="true" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="true"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="true"},
                                                            new { feature_id = 4, protocol_id = 2 ,group = "Tank Delivery Clean",enable="false"},
                                                            new { feature_id = 5, protocol_id = 2 ,group = "Reconcilation",enable="true"},
                                                            new { feature_id = 6, protocol_id = 2 ,group = "GAIA",enable="false"}
                                                    };
                                }
                            }
                            else
                            {
                                dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="false" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="false"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="false"},
                                                            new { feature_id = 4, protocol_id = 2 ,group = "Tank Delivery Clean",enable="false"},
                                                            new { feature_id = 5, protocol_id = 2 ,group = "Reconcilation",enable="false"},
                                                            new { feature_id = 6, protocol_id = 2 ,group = "GAIA",enable="false"}
                                                    };
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Warning: Cannot get file version of PI - THUNDER, please recheck and try again.", "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dataTable_tank_gauge_features = new object[] { new { feature_id = 1, protocol_id = 2,group = "Tank Inventory",enable="false" },
                                                            new { feature_id = 2, protocol_id = 2 ,group = "System Status",enable="false"} ,
                                                            new { feature_id = 3, protocol_id = 2 ,group = "Tank Delivery",enable="false"},
                                                            new { feature_id = 4, protocol_id = 2 ,group = "Tank Delivery Clean",enable="false"},
                                                            new { feature_id = 5, protocol_id = 2 ,group = "Reconcilation",enable="false"},
                                                            new { feature_id = 6, protocol_id = 2 ,group = "GAIA",enable="false"}
                                                };
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Warning: " + ex, "Warning MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Environment.Exit(1);
                    }

                    #endregion


                    //###########################################################################################################################//

                    object collectionWrapper = new
                    {
                        site_config = new[] { new { key = "Site_Code", value =  (dataTable_site_config.Rows[0][0]).ToString().Replace(" ", String.Empty) },
                                              new { key = "Authorize Timeout", value = (dataTable_site_config.Rows[0][1]).ToString().Replace(" ", String.Empty) } ,
                                              new { key = "Delivery Timeout", value =  (dataTable_site_config.Rows[0][2]).ToString().Replace(" ", String.Empty) },
                                              new { key = "No Flow Timeout", value =  (dataTable_site_config.Rows[0][3]).ToString().Replace(" ", String.Empty) }
                                            },                               //***success***
                        grades = dataTable_Grade,                            //***success***
                        price_profiles = dataTable_price_profiles,           //***success*** 
                        tanks = dataTable_Tanks,                             //***success***                                                            
                        loops = dataTable_loops,                             //***success***                          
                        pumps = dataTable_pumps,                             //***success*** 
                        advances_setting = dataTable_advances_setting,       //***success***                                                          
                        pumps_display = dataTable_pumps_display,             //***success***       
                        hoses = dataTable_hoses,                             //***success***
                        transactions = dataTable_transactions_id,            //***success***
                        tank_gauge_features = dataTable_tank_gauge_features
                    };

                    string jsonString = JsonConvert.SerializeObject(collectionWrapper, Formatting.Indented);
                    Console.WriteLine(jsonString);


                    exportPath = saveFileDialog.FileName;
                    label5.Text = exportPath;
                    File.WriteAllText(exportPath, jsonString);
                    backgroundWorker1.RunWorkerAsync();
                }
                connect.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}