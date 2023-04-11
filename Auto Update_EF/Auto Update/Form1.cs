using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Globalization;

namespace Auto_Update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UserControl_Home Home = new UserControl_Home();
        UserControl_Management_Data_Station Management_Data_Station = new UserControl_Management_Data_Station();
        UserControl_Set_Config_Station Set_Config_Station = new UserControl_Set_Config_Station();
        UserControl_Update_Service Update_Servic = new UserControl_Update_Service();
        UserControl_About About = new UserControl_About();
        UserControl_Help Help = new UserControl_Help();

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel_body.Controls.Clear();
            panel_body.Controls.Add(userControl);
            userControl.BringToFront();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            addUserControl(Home);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button_Home_Click(object sender, EventArgs e)
        {
            addUserControl(Home);
        }

        private void Button_Management_Data_Station_Click(object sender, EventArgs e)
        {
            addUserControl(Management_Data_Station);

            var dataGridView_111 = panel_body.Controls.Find("dataGridView111", true).FirstOrDefault() as DataGridView;
            dataGridView_111.Rows.Clear();
            using (var db = new STATIONDBEntities())
            {
                var station_data = db.station_data.ToList();
                int i = 1;
                foreach (var st in station_data)
                {
                    dataGridView_111.Rows.Add(i, st.station_pbl, st.station_bu, st.station_name, st.station_ip);
                    i++;
                }
            }
        }

        private void Button_set_config_station_Click(object sender, EventArgs e)
        {
            addUserControl(Set_Config_Station);
            addUserControl(Set_Config_Station);

            var dataGridView_1 = panel_body.Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView;
            var dataGridView_2 = panel_body.Controls.Find("dataGridView2", true).FirstOrDefault() as DataGridView;
            var dateTimePicker_1 = panel_body.Controls.Find("dateTimePicker1", true).FirstOrDefault() as DateTimePicker;
            var dateTimePicker_2 = panel_body.Controls.Find("dateTimePicker2", true).FirstOrDefault() as DateTimePicker;
            var toggleButton_1 = panel_body.Controls.Find("toggleButton1", true).FirstOrDefault() as ToggleButton;
            var toggleButton_2 = panel_body.Controls.Find("toggleButton2", true).FirstOrDefault() as ToggleButton;
            var toggleButton_3 = panel_body.Controls.Find("toggleButton3", true).FirstOrDefault() as ToggleButton;
            var toggleButton_4 = panel_body.Controls.Find("toggleButton4", true).FirstOrDefault() as ToggleButton;
            var toggleButton_5 = panel_body.Controls.Find("toggleButton5", true).FirstOrDefault() as ToggleButton;
            var toggleButton_6 = panel_body.Controls.Find("toggleButton6", true).FirstOrDefault() as ToggleButton;
            var toggleButton_7 = panel_body.Controls.Find("toggleButton7", true).FirstOrDefault() as ToggleButton;
            var toggleButton_8 = panel_body.Controls.Find("toggleButton8", true).FirstOrDefault() as ToggleButton;
            var textBox_1 = panel_body.Controls.Find("textBox1", true).FirstOrDefault() as TextBox;
            var label_6 = panel_body.Controls.Find("label6", true).FirstOrDefault() as Label;
            var label_26 = panel_body.Controls.Find("label26", true).FirstOrDefault() as Label;
            var label_27 = panel_body.Controls.Find("label27", true).FirstOrDefault() as Label;

            dataGridView_1.Rows.Clear();
            dataGridView_2.Rows.Clear();

            using (var db = new STATIONDBEntities())
            {
                var station_data = db.station_data.ToList();
                var station_installer = db.station_installer.ToList();

                foreach (var st in station_data)
                {
                    dataGridView_1.Rows.Add(st.station_id, st.station_pbl, st.station_bu, st.station_name, st.station_ip);
                }

                var i = 1;
                foreach (var st in station_installer)
                {
                    dataGridView_2.Rows.Add(i, st.station_pbl, st.station_bu, st.station_name, st.station_ip);
                    i++;
                }
                var setting_config = db.setting_config.ToList();
                var tranfer_time = setting_config[0].data_value;
                var update_time = setting_config[1].data_value;
                var service = setting_config[2].data_value;
                var file_transfer = setting_config[3].data_value;

                //dateTimePicker_1.Value = tranfer_time.ToString("F", CultureInfo.CreateSpecificCulture("en-US"));

                dateTimePicker_1.Value = DateTime.Parse(tranfer_time);
                dateTimePicker_2.Value = DateTime.Parse(update_time);

                textBox_1.Text = file_transfer;
                label_6.Text = dataGridView_2.Rows.Count.ToString();

                if (service == "1")
                {
                    toggleButton_1.Checked = true;
                }
                else if (service == "2")
                {
                    toggleButton_2.Checked = true;
                }
                else if (service == "3")
                {
                    toggleButton_3.Checked = true;
                }
                else if (service == "4")
                {
                    toggleButton_4.Checked = true;
                }
                else if (service == "5")
                {
                    toggleButton_5.Checked = true;

                }
                else if (service == "6")
                {
                    toggleButton_6.Checked = true;
                }
                else if (service == "7")
                {
                    toggleButton_7.Checked = true;
                }
            }

            if (toggleButton_8.Checked == true)
            {
                label_26.BringToFront();
                label_27.BringToFront();
            }
            else if (toggleButton_8.Checked == false)
            {
                label_26.BringToFront();
                label_27.BringToFront();
            }

        }

        private void Button_Update_Service_Click(object sender, EventArgs e)
        {
            addUserControl(Update_Servic);
            var dataGridView_11 = panel_body.Controls.Find("dataGridView11", true).FirstOrDefault() as DataGridView;
            var dateTimePicker_11 = panel_body.Controls.Find("dateTimePicker11", true).FirstOrDefault() as DateTimePicker;
            var dateTimePicker_22 = panel_body.Controls.Find("dateTimePicker22", true).FirstOrDefault() as DateTimePicker;
            var label_33 = panel_body.Controls.Find("label33", true).FirstOrDefault() as Label;
            var part = panel_body.Controls.Find("part", true).FirstOrDefault() as Label;
            dataGridView_11.Rows.Clear();

            dynamic result_transfer = null;
            dynamic result_update = null;
            dynamic Result_Transfer_bool;
            dynamic Result_Update_bool;
            Image image_done = Image.FromFile(Directory.GetCurrentDirectory() + @"\img\icons_done.png");
            Image image_close = Image.FromFile( Directory.GetCurrentDirectory() + @"\img\icons_close.png");
            Image icons_wait = Image.FromFile( Directory.GetCurrentDirectory() + @"\img\icons_wait.png");

            using (var db = new STATIONDBEntities())
            {
                var station_data = db.station_installer.ToList();
                int i = 1;
                foreach (var st in station_data)
                {
                    result_transfer = st.result_transfer == null ? icons_wait : st.result_transfer == true ? image_done : image_close;
                    result_update = st.result_update == null ? icons_wait : st.result_update == true ? image_done : image_close;
                    Result_Transfer_bool = result_transfer == image_done ? "True" : "False";
                    Result_Update_bool = result_transfer == image_done ? "True" : "False";
                    dataGridView_11.Rows.Add(i, st.station_pbl, st.station_bu, st.station_name, st.station_ip, result_transfer, result_update, Result_Transfer_bool, Result_Update_bool);
                    i++;
                }

                var setting_config = db.setting_config.ToList();
                var tranfer_time = setting_config[0].data_value;
                var update_time = setting_config[1].data_value;
                dateTimePicker_11.Value = DateTime.Parse(tranfer_time);
                dateTimePicker_22.Value = DateTime.Parse(update_time);

                var service_id = int.Parse(db.setting_config.Where(x => x.config_id == 3).ToList()[0].data_value);
                var service_name = db.services.Where(x => x.service_id == service_id).ToList()[0].service_name;
                label_33.Text = service_name;

                part.Text = setting_config[3].data_value;
            }
        }

        private void Button_About_Click(object sender, EventArgs e)
        {
            addUserControl(About);
        }

        private void Button_Help_Click(object sender, EventArgs e)
        {
            addUserControl(Help);
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userControl_Update_Service1_Load(object sender, EventArgs e)
        {

        }
    }
}
