using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Theoretical_Volume_Update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<tanks> Tanks = new List<tanks>();
        public string IP_ADDRESS;
        public string USERNAME = "lavender";
        public string PASSWORD = "muj,nv,ufvdw,h";
        public int PORT = 22;
        public string commandSql;
        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var CurrentDirectory = Directory.GetCurrentDirectory();
                StreamReader r = new StreamReader(CurrentDirectory + @"\ConnectionString.json");
                string jsonString = r.ReadToEnd();
                JObject rss = JObject.Parse(jsonString);
                IP_ADDRESS = (string)rss["ConnectionStrings"]["IP_ADDRESS"];
                USERNAME = "lavender";
                PASSWORD = "muj,nv,ufvdw,h";
                PORT = 22;

                SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
                client.Connect();
                if (client.IsConnected)
                {
                    var command = client.CreateCommand("ls");
                    var result = command.Execute();
                    char dubleCode = '"';

                    //Get tb.lavender.tanks
                    commandSql = "SELECT tank_id, tank_name, profile_name, theoretical_volume FROM lavender.tanks INNER JOIN lavender.price_profiles ON grade_id = profile_id ORDER BY tank_id ASC ;";
                    command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{commandSql}" + dubleCode);
                    var tank_detail = command.Execute().Split('\n').ToList();
                    tank_detail.RemoveRange(0, 2);
                    tank_detail.Reverse();
                    tank_detail.RemoveRange(0, 3);
                    tank_detail.Reverse();

                    //Map tanks
                    foreach (var t in tank_detail)
                    {
                        tanks tank = new tanks();
                        tank.tank_id = int.Parse(t.Split('|')[0]);
                        tank.tank_name = t.Split('|')[1].Replace(" ", "");
                        tank.profile_name = t.Split('|')[2].Replace(" ", "");
                        tank.theoretical_volume = double.Parse(t.Split('|')[3]);
                        Tanks.Add(tank);
                    }

                    foreach (var t in Tanks)
                    {
                        dataGridView1.Rows.Add(t.tank_id, t.tank_name, t.profile_name, t.theoretical_volume);
                    }
                }
                client.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Update Button
        {
            SshClient client = new SshClient(IP_ADDRESS, PORT, USERNAME, PASSWORD);
            client.Connect();

            var command = client.CreateCommand("ls");
            var result = command.Execute();
            char dubleCode = '"';

            if (client.IsConnected)
            {
                DialogResult results = MessageBox.Show("Do you want update data?", "MessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        int tank_id = int.Parse(dataGridView1.Rows[i].Cells["หมายเลขถัง"].Value.ToString());
                        double theoretical_volume = double.Parse(dataGridView1.Rows[i].Cells["ปริมาณวัด"].Value.ToString());

                        commandSql = $"UPDATE lavender.tanks SET theoretical_volume = { theoretical_volume } WHERE tank_id = { tank_id };";
                        command = client.CreateCommand("PGPASSWORD=" + dubleCode + "gdH[,yogvkw;hg]pmyh'fvdw,h" + dubleCode + " psql -h localhost -U postgres -p 5432 -d LAVENDERDB -c " + dubleCode + $"{commandSql}" + dubleCode);
                        result = command.Execute();
                    }
                    MessageBox.Show("Update Success", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            client.Dispose();
        }

        private void button2_Click(object sender, EventArgs e) // Cancle Button
        {
            Application.Exit();
        }
    }
}


