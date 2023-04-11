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
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string station_Name = rjTextBox5.Texts;
            string station_Ip = rjTextBox6.Texts;
            string station_Pbl = rjTextBox7.Texts;
            string station_bu = comboBox1.Text.ToString();
            using (var db = new STATIONDBEntities())
            {
                station_data station_Data = new station_data();
                station_Data.station_name = station_Name;
                station_Data.station_ip = station_Ip;
                station_Data.station_bu = station_bu;
                station_Data.station_pbl = station_Pbl;
                db.station_data.Add(station_Data);
                db.SaveChanges();
            }

            this.Visible = false;
            Dialog_OK dialog_OK = new Dialog_OK();
            dialog_OK.ShowDialog();
            this.Close();
        }
    }
}
