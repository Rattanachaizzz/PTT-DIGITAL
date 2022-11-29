using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace API_CHECKER
{
    public partial class Form1 : Form
    {
#if DEBUG
        static string url = "http://10.195.2.115";
#else
        static string url = "http://192.168.0.201";
#endif
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "WAIT...";
            button1.Enabled = false;
            for (int i = 0; i <= 12; i++)
            {
                switch (i)
                {
                    case 0:
                        Command();
                        break;
                    case 1:
                        GetGrade();
                        break;
                    case 2:
                        GetPos();
                        break;
                    case 3:
                        GetPrice();
                        break;
                    case 4:
                        Realtime();
                        break;
                    case 5:
                        GetStack();
                        break;
                    case 6:
                        GetTanks();
                        break;
                    case 7:
                        GetTotalizer();
                        break;
                    case 8:
                        Transection();
                        break;
                    case 9:
                        Initialize();
                        break;
                    case 10:
                        Login_Logout();
                        break;
                    case 11:
                        Setting();
                        break;
                    default:
                        break;
                }
            }
            button1.Text = "CHECKER";
            button1.Enabled = true;
            string message = "Application Run Success!!!";
            string title = "Application Status";
            DialogResult result = MessageBox.Show(message, title);
        }
        public void button2_Click(object sender, EventArgs e)
        {
            label43.Text = "Null";
            label45.Text = "Null";
            label46.Text = "Null";
            label47.Text = "Null";
            label48.Text = "Null";
            label49.Text = "Null";
            label50.Text = "Null";
            label51.Text = "Null";
            label52.Text = "Null";
            label53.Text = "Null";
            label54.Text = "Null";
            label55.Text = "Null";

            label44.Text = "Ready";
            label56.Text = "Ready";
            label57.Text = "Ready";
            label58.Text = "Ready";
            label59.Text = "Ready";
            label60.Text = "Ready";
            label61.Text = "Ready";
            label62.Text = "Ready";
            label63.Text = "Ready";
            label64.Text = "Ready";
            label65.Text = "Ready";
            label66.Text = "Ready";

            label44.BackColor = Color.Black;
            label56.BackColor = Color.Black;
            label57.BackColor = Color.Black;
            label58.BackColor = Color.Black;
            label59.BackColor = Color.Black;
            label60.BackColor = Color.Black;
            label61.BackColor = Color.Black;
            label62.BackColor = Color.Black;
            label63.BackColor = Color.Black;
            label64.BackColor = Color.Black;
            label65.BackColor = Color.Black;
            label66.BackColor = Color.Black;

            label44.Font = new Font("Microsoft Sans Serif", 8);
            label56.Font = new Font("Microsoft Sans Serif", 8);
            label57.Font = new Font("Microsoft Sans Serif", 8);
            label58.Font = new Font("Microsoft Sans Serif", 8);
            label59.Font = new Font("Microsoft Sans Serif", 8);
            label60.Font = new Font("Microsoft Sans Serif", 8);
            label61.Font = new Font("Microsoft Sans Serif", 8);
            label62.Font = new Font("Microsoft Sans Serif", 8);
            label63.Font = new Font("Microsoft Sans Serif", 8);
            label64.Font = new Font("Microsoft Sans Serif", 8);
            label65.Font = new Font("Microsoft Sans Serif", 8);
            label66.Font = new Font("Microsoft Sans Serif", 8);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Do you want to close this Application?";
            string title = "Close Application";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        public void Command()
        {
            try
            {
                var client = new RestClient(url + @":3006/Lavender/StartPump");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label43.Text = ((int)response.StatusCode).ToString();
                    label44.Text = "Normal";
                    label44.Margin = new Padding(2, 2, 2, 2);
                    label44.ForeColor = Color.White;
                    label44.Font = new Font("Impact", 8);
                    label44.BackColor = Color.Lime;
                }
                else
                {
                    label43.Text = ((int)response.StatusCode).ToString();
                    label44.Text = "Unnormal";
                    label44.Margin = new Padding(2, 2, 2, 2);
                    label44.ForeColor = Color.White;
                    label44.Font = new Font("Impact", 8);
                    label44.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetGrade()
        {
            try
            {
                var client = new RestClient(url + @":3002/Lavender/Grade");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label45.Text = ((int)response.StatusCode).ToString();
                    label56.Text = "Normal";
                    label56.Margin = new Padding(2, 2, 2, 2);
                    label56.ForeColor = Color.White;
                    label56.Font = new Font("Impact", 8);
                    label56.BackColor = Color.Lime;
                }
                else
                {
                    label45.Text = ((int)response.StatusCode).ToString();
                    label56.Text = "Unnormal";
                    label56.Margin = new Padding(2, 2, 2, 2);
                    label56.ForeColor = Color.White;
                    label56.Font = new Font("Impact", 8);
                    label56.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetPos()
        {
            try
            {
                var client = new RestClient(url + @":3010/Lavender/HealthCheck");
                RestRequest request = new RestRequest("", Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("terminal_id", "999");
                request.AddParameter("datetime", "2022-04-04 12:55:15");
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label46.Text = ((int)response.StatusCode).ToString();
                    label57.Text = "Normal";
                    label57.Margin = new Padding(2, 2, 2, 2);
                    label57.ForeColor = Color.White;
                    label57.Font = new Font("Impact", 8);
                    label57.BackColor = Color.Lime;
                }
                else
                {
                    label46.Text = ((int)response.StatusCode).ToString();
                    label57.Text = "Unnormal";
                    label57.Margin = new Padding(2, 2, 2, 2);
                    label57.ForeColor = Color.White;
                    label57.Font = new Font("Impact", 8);
                    label57.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetPrice()
        {
            try
            {
                var client = new RestClient(url + @":3003/Lavender/GetPriceByPump/:pumpid/:hosenumber");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label47.Text = ((int)response.StatusCode).ToString();
                    label58.Text = "Normal";
                    label58.Margin = new Padding(2, 2, 2, 2);
                    label58.ForeColor = Color.White;
                    label58.Font = new Font("Impact", 8);
                    label58.BackColor = Color.Lime;
                }
                else
                {
                    label47.Text = ((int)response.StatusCode).ToString();
                    label58.Text = "Unnormal";
                    label58.Margin = new Padding(2, 2, 2, 2);
                    label58.ForeColor = Color.White;
                    label58.Font = new Font("Impact", 8);
                    label58.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void Realtime()
        {
            try
            {
                var client = new RestClient(url + @":3001/Lavender/CurrentStatus-v2");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label48.Text = ((int)response.StatusCode).ToString();
                    label59.Text = "Normal";
                    label59.Margin = new Padding(2, 2, 2, 2);
                    label59.ForeColor = Color.White;
                    label59.Font = new Font("Impact", 8);
                    label59.BackColor = Color.Lime;
                }
                else
                {
                    label48.Text = ((int)response.StatusCode).ToString();
                    label59.Text = "Unnormal";
                    label59.Margin = new Padding(2, 2, 2, 2);
                    label59.ForeColor = Color.White;
                    label59.Font = new Font("Impact", 8);
                    label59.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetStack()
        {
            try
            {
                var client = new RestClient(url + @":3008/Lavender/GetStackByPump/:pumpid");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label49.Text = ((int)response.StatusCode).ToString();
                    label60.Text = "Normal";
                    label60.Margin = new Padding(2, 2, 2, 2);
                    label60.ForeColor = Color.White;
                    label60.Font = new Font("Impact", 8);
                    label60.BackColor = Color.Lime;
                }
                else
                {
                    label49.Text = ((int)response.StatusCode).ToString();
                    label60.Text = "Unnormal";
                    label60.Margin = new Padding(2, 2, 2, 2);
                    label60.ForeColor = Color.White;
                    label60.Font = new Font("Impact", 8);
                    label60.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetTanks()
        {
            try
            {
                var client = new RestClient(url + @":3007/Lavender/TankInventory");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label50.Text = ((int)response.StatusCode).ToString();
                    label61.Text = "Normal";
                    label61.Margin = new Padding(2, 2, 2, 2);
                    label61.ForeColor = Color.White;
                    label61.Font = new Font("Impact", 8);
                    label61.BackColor = Color.Lime;
                }
                else
                {
                    label50.Text = ((int)response.StatusCode).ToString();
                    label61.Text = "Unnormal";
                    label61.Margin = new Padding(2, 2, 2, 2);
                    label61.ForeColor = Color.White;
                    label61.Font = new Font("Impact", 8);
                    label61.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void GetTotalizer()
        {
            try
            {
                var client = new RestClient(url + @":3005/Lavender/Totalizer");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label51.Text = ((int)response.StatusCode).ToString();
                    label62.Text = "Normal";
                    label62.Margin = new Padding(2, 2, 2, 2);
                    label62.ForeColor = Color.White;
                    label62.Font = new Font("Impact", 8);
                    label62.BackColor = Color.Lime;
                }
                else
                {
                    label51.Text = ((int)response.StatusCode).ToString();
                    label62.Text = "Unnormal";
                    label62.Margin = new Padding(2, 2, 2, 2);
                    label62.ForeColor = Color.White;
                    label62.Font = new Font("Impact", 8);
                    label62.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void Transection()
        {
            try
            {
                var client = new RestClient(url + @":3004/Lavender/CurrentTransaction/:pump_id");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label52.Text = ((int)response.StatusCode).ToString();
                    label63.Text = "Normal";
                    label63.Margin = new Padding(2, 2, 2, 2);
                    label63.ForeColor = Color.White;
                    label63.Font = new Font("Impact", 8);
                    label63.BackColor = Color.Lime;
                }
                else
                {
                    label52.Text = ((int)response.StatusCode).ToString();
                    label63.Text = "Unnormal";
                    label63.Margin = new Padding(2, 2, 2, 2);
                    label63.ForeColor = Color.White;
                    label63.Font = new Font("Impact", 8);
                    label63.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void Initialize()
        {
            try
            {
                var client = new RestClient(url + @":3009/Lavender/GetPumps");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label53.Text = ((int)response.StatusCode).ToString();
                    label64.Text = "Normal";
                    label64.Margin = new Padding(2, 2, 2, 2);
                    label64.ForeColor = Color.White;
                    label64.Font = new Font("Impact", 8);
                    label64.BackColor = Color.Lime;
                }
                else
                {
                    label53.Text = ((int)response.StatusCode).ToString();
                    label64.Text = "Unnormal";
                    label64.Margin = new Padding(2, 2, 2, 2);
                    label64.ForeColor = Color.White;
                    label64.Font = new Font("Impact", 8);
                    label64.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void Login_Logout()
        {
            try
            {
                var client = new RestClient(url + @":3000/Lavender/Login");
                RestRequest request = new RestRequest("", Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("terminal_id", "999");
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 200)
                {
                    label54.Text = ((int)response.StatusCode).ToString();
                    label65.Text = "Normal";
                    label65.Margin = new Padding(2, 2, 2, 2);
                    label65.ForeColor = Color.White;
                    label65.Font = new Font("Impact", 8);
                    label65.BackColor = Color.Lime;
                }
                else
                {

                    label54.Text = ((int)response.StatusCode).ToString();
                    label65.Text = "Unnormal";
                    label65.Margin = new Padding(2, 2, 2, 2);
                    label65.ForeColor = Color.White;
                    label65.Font = new Font("Impact", 8);
                    label65.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
        public void Setting()
        {
            try
            {
                var client = new RestClient(url + @":4000/Lavender/Processing");
                RestRequest request = new RestRequest();
                request.AddHeader("Authorization", "Basic Og==");
                var response = client.Execute(request);
                if ((int)response.StatusCode == 401)
                {
                    label55.Text = ((int)response.StatusCode).ToString();
                    label66.Text = "Normal";
                    label66.Margin = new Padding(2, 2, 2, 2);
                    label66.ForeColor = Color.White;
                    label66.Font = new Font("Impact", 8);
                    label66.BackColor = Color.Lime;
                }
                else
                {
                    label55.Text = ((int)response.StatusCode).ToString();
                    label66.Text = "Unnormal";
                    label66.Margin = new Padding(2, 2, 2, 2);
                    label66.ForeColor = Color.White;
                    label66.Font = new Font("Impact", 8);
                    label66.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex);
            }
        }
    }
}
