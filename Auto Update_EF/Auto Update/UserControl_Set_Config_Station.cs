using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Update
{
    public partial class UserControl_Set_Config_Station : UserControl
    {
        public UserControl_Set_Config_Station()
        {
            InitializeComponent();
        }

        private void UserControl_Set_Config_Station_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now.AddMinutes(180);

            textBox1.ReadOnly = true;
            //using (var db = new STATIONDBEntities())
            //{
            //    var station_data = db.station_data.ToList();
            //    foreach (var st in station_data)
            //    {
            //        dataGridView1.Rows.Add(st.station_id, st.station_pbl, st.station_bu, st.station_name, st.station_ip);
            //    }

            //    var setting_config = db.setting_config.ToList();
            //    var tranfer_time = setting_config[0].data_value;
            //    var update_time = setting_config[1].data_value;
            //    var service = setting_config[2].data_value;

            //    dateTimePicker1.Value = DateTime.Parse(tranfer_time);
            //    dateTimePicker2.Value = DateTime.Parse(update_time);
            //    if (service == "1")
            //    {
            //        toggleButton1.Checked = true;
            //    }
            //    else if (service == "2")
            //    {
            //        toggleButton2.Checked = true;
            //    }
            //    else if (service == "3")
            //    {
            //        toggleButton3.Checked = true;
            //    }
            //    else if (service == "4")
            //    {
            //        toggleButton4.Checked = true;
            //    }
            //    else if (service == "5")
            //    {
            //        toggleButton5.Checked = true;

            //    }
            //    else if (service == "6")
            //    { 
            //        toggleButton6.Checked = true;
            //    }
            //    else if (service == "7")
            //    {
            //        toggleButton7.Checked = true;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                if (dataGridView2.DefaultCellStyle.SelectionBackColor == Color.DeepSkyBlue)
                {
                    dataGridView2.Rows.Remove(row);
                    label6.Text = dataGridView2.Rows.Count.ToString();
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You need access config ?", "Wanning", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton1.Checked == true)
            {
                panel6.BackColor = Color.MediumSeaGreen;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;

                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton1.Checked == false)
            {
                panel6.BackColor = Color.Tomato;
            }
        }

        private void rjTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton2.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.MediumSeaGreen;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;

                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton2.Checked == false)
            {
                panel7.BackColor = Color.Tomato;
            }
        }

        private void toggleButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton3.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.MediumSeaGreen;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;

                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton3.Checked == false)
            {
                panel8.BackColor = Color.Tomato;
            }
        }

        private void toggleButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton4.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.MediumSeaGreen;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.MediumSeaGreen;

                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton4.Checked == false)
            {
                panel9.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;
            }
        }

        private void toggleButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton5.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.MediumSeaGreen;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;

                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton5.Checked == false)
            {
                panel10.BackColor = Color.Tomato;
            }
        }

        private void toggleButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton6.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.MediumSeaGreen;
                panel12.BackColor = Color.Tomato;
                panel14.BackColor = Color.Tomato;

                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton6.Checked == false)
            {
                panel11.BackColor = Color.Tomato;
            }
        }

        private void toggleButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton7.Checked == true)
            {
                panel6.BackColor = Color.Tomato;
                panel7.BackColor = Color.Tomato;
                panel8.BackColor = Color.Tomato;
                panel9.BackColor = Color.Tomato;
                panel10.BackColor = Color.Tomato;
                panel11.BackColor = Color.Tomato;
                panel12.BackColor = Color.MediumSeaGreen;
                panel14.BackColor = Color.Tomato;

                toggleButton2.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton3.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton4.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton5.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton6.CheckState = System.Windows.Forms.CheckState.Unchecked;
                toggleButton1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            else if (toggleButton7.Checked == false)
            {
                panel12.BackColor = Color.Tomato;
            }
        }

        private void panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (dataGridView1.DefaultCellStyle.SelectionBackColor == Color.DeepSkyBlue)
                {
                    int id = int.Parse(dataGridView1.Rows[row.Index].Cells[0].Value.ToString().Replace(" ", ""));
                    string pbl = dataGridView1.Rows[row.Index].Cells[1].Value.ToString().Replace(" ", "");
                    string bu = dataGridView1.Rows[row.Index].Cells[2].Value.ToString().Replace(" ", "");
                    string station_name = dataGridView1.Rows[row.Index].Cells[3].Value.ToString().Replace(" ", "");
                    string station_ip = dataGridView1.Rows[row.Index].Cells[4].Value.ToString().Replace(" ", "");
                    dataGridView2.Rows.Add(dataGridView2.RowCount+1, pbl, bu, station_name, station_ip);
                    label6.Text = dataGridView2.Rows.Count.ToString();
                }
            }
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void RemoveAll_station_installer()
        {
            using (var db = new STATIONDBEntities())
            {
                db.station_installer.RemoveRange(db.station_installer);
                db.SaveChanges();
            }
        }
        private void rjButton1_Click(object sender, EventArgs e)
        {
            string Service_Install = "";
            if (toggleButton1.Checked == true)
            {
                Service_Install = "dispenser";
            }
            else if (toggleButton2.Checked == true)
            {
                Service_Install = "tankgauge";
            }
            else if (toggleButton3.Checked == true)
            {
                Service_Install = "monitor";
            }
            else if (toggleButton4.Checked == true)
            {
                Service_Install = "gaia";
            }
            else if (toggleButton5.Checked == true)
            {
                Service_Install = "api";
            }
            else if (toggleButton6.Checked == true)
            {
                Service_Install = "web config";
            }
            else if (toggleButton7.Checked == true)
            {
                Service_Install = "web support";
            }

            string msg = $"Do you want save config? \n Station Install : {label6.Text} \n Service Install : {Service_Install} \n Path File : {textBox1.Text} \n Transfer Time : {dateTimePicker1.Value} \n Update Time : {dateTimePicker2.Value}";
            DialogResult result = MessageBox.Show(msg, "Save Config", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                using (var db = new STATIONDBEntities())
                {
                    RemoveAll_station_installer();

                    ////tb.station_installer
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        station_installer station_Installer = new station_installer();
                        station_Installer.station_id = int.Parse(row.Cells[0].Value.ToString());
                        station_Installer.station_pbl = row.Cells[1].Value.ToString();
                        station_Installer.station_bu = row.Cells[2].Value.ToString();
                        station_Installer.station_name = row.Cells[3].Value.ToString();
                        station_Installer.station_ip = row.Cells[4].Value.ToString();
                        db.station_installer.Add(station_Installer);
                    }

                    //tb.setting_config
                    var transfer_time = db.setting_config.ToList().SingleOrDefault(x => x.data_key == "transfer_time");
                    var update_time = db.setting_config.ToList().SingleOrDefault(x => x.data_key == "update_time");
                    var service = db.setting_config.ToList().SingleOrDefault(x => x.data_key == "service");
                    var file_transfer = db.setting_config.ToList().SingleOrDefault(x => x.data_key == "file_transfer");
                    if (transfer_time != null && update_time != null)
                    {
                        transfer_time.data_value = dateTimePicker1.Value.ToString();
                        update_time.data_value = dateTimePicker2.Value.ToString();
                        file_transfer.data_value = textBox1.Text;
                    }
                    if (toggleButton1.Checked == true)
                    {
                        service.data_value = "1";
                    }
                    else if (toggleButton2.Checked == true)
                    {
                        service.data_value = "2";
                    }
                    else if (toggleButton3.Checked == true)
                    {
                        service.data_value = "3";
                    }
                    else if (toggleButton4.Checked == true)
                    {
                        service.data_value = "4";
                    }
                    else if (toggleButton5.Checked == true)
                    {
                        service.data_value = "5";
                    }
                    else if (toggleButton6.Checked == true)
                    {
                        service.data_value = "6";
                    }
                    else if (toggleButton7.Checked == true)
                    {
                        service.data_value = "7";
                    }
                    db.SaveChanges();
                }
            }
        }

        private void UserControl_Set_Config_Station_MouseHover(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void toggleButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleButton8.Checked == true)
            {
                label26.BringToFront();
                label27.BringToFront();
            }
            else if (toggleButton8.Checked == false)
            {
                dateTimePicker1.BringToFront();
                dateTimePicker2.BringToFront();
            }
        }
    }
}
