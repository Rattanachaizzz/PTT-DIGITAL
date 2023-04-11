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
    public partial class Dialog_Delect : Form
    {
        public Dialog_Delect()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //string station_name = "TestADD";
            using (var db = new STATIONDBEntities())
            {
                var station_rm = db.station_data.Where(x => x.station_id == 1 || x.station_name == "TestADD" ).ToList();
                //var file_transfer = db.setting_config.ToList().SingleOrDefault(x => x.data_key == "file_transfer");
                //db.station_data.Remove(station_rm);
                db.SaveChanges();
            }
        }
    }
}
