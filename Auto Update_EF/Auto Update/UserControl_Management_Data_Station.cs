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
    public partial class UserControl_Management_Data_Station : UserControl
    {
        public UserControl_Management_Data_Station()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl_Management_Data_Station_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView111_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           Add add = new Add();
           DialogResult result = add.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Edit edit = new Edit();
            DialogResult result = edit.ShowDialog();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Do you want delect this station ?", "DELECT STATION", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            //if (result == DialogResult.OK)
            //{
            //    Console.WriteLine("aaaa");
            //}


            Dialog_Delect dialog_Delect = new Dialog_Delect();
            DialogResult result = dialog_Delect.ShowDialog();
            if (result == DialogResult.OK)
            { 
            
            }

            

            //DialogResult result = MessageBox.Show("Do you want delect this station ?", "DELECT STATION", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (result == DialogResult.OK)
            //{
            //    Console.WriteLine("aaaa");
            //}
        }
    }
}
