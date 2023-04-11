namespace Auto_Update
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Update_Service = new Guna.UI2.WinForms.Guna2Button();
            this.Button_About = new Guna.UI2.WinForms.Guna2Button();
            this.Button_Home = new Guna.UI2.WinForms.Guna2Button();
            this.Button_set_config_station = new Guna.UI2.WinForms.Guna2Button();
            this.Button_Management_Data_Station = new Guna.UI2.WinForms.Guna2Button();
            this.Button_Help = new Guna.UI2.WinForms.Guna2Button();
            this.panel_body = new System.Windows.Forms.Panel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl3 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.ellipseTool1 = new Auto_Update.EllipseTool();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_close.ForeColor = System.Drawing.Color.White;
            this.button_close.Location = new System.Drawing.Point(1231, 19);
            this.button_close.Margin = new System.Windows.Forms.Padding(2);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(46, 34);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "X";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1180, 19);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelHeader.Controls.Add(this.button_close);
            this.panelHeader.Controls.Add(this.button1);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1300, 71);
            this.panelHeader.TabIndex = 2;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(87, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "AUTO UPDATE";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.Button_Update_Service, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Button_About, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.Button_Home, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Button_set_config_station, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Button_Management_Data_Station, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Button_Help, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 636);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1300, 85);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // Button_Update_Service
            // 
            this.Button_Update_Service.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_Update_Service.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_Update_Service.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_Update_Service.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_Update_Service.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_Update_Service.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Update_Service.FillColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_Update_Service.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Update_Service.ForeColor = System.Drawing.Color.White;
            this.Button_Update_Service.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_Update_Service.Image = ((System.Drawing.Image)(resources.GetObject("Button_Update_Service.Image")));
            this.Button_Update_Service.ImageSize = new System.Drawing.Size(40, 40);
            this.Button_Update_Service.Location = new System.Drawing.Point(648, 0);
            this.Button_Update_Service.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Update_Service.Name = "Button_Update_Service";
            this.Button_Update_Service.Size = new System.Drawing.Size(216, 85);
            this.Button_Update_Service.TabIndex = 7;
            this.Button_Update_Service.Text = "Update Service";
            this.Button_Update_Service.Click += new System.EventHandler(this.Button_Update_Service_Click);
            // 
            // Button_About
            // 
            this.Button_About.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_About.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_About.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_About.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_About.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_About.FillColor = System.Drawing.Color.DodgerBlue;
            this.Button_About.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_About.ForeColor = System.Drawing.Color.White;
            this.Button_About.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_About.Image = ((System.Drawing.Image)(resources.GetObject("Button_About.Image")));
            this.Button_About.ImageSize = new System.Drawing.Size(55, 55);
            this.Button_About.Location = new System.Drawing.Point(864, 0);
            this.Button_About.Margin = new System.Windows.Forms.Padding(0);
            this.Button_About.Name = "Button_About";
            this.Button_About.Size = new System.Drawing.Size(216, 85);
            this.Button_About.TabIndex = 8;
            this.Button_About.Text = "About";
            this.Button_About.Click += new System.EventHandler(this.Button_About_Click);
            // 
            // Button_Home
            // 
            this.Button_Home.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_Home.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_Home.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_Home.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_Home.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_Home.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Home.FillColor = System.Drawing.Color.DodgerBlue;
            this.Button_Home.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Home.ForeColor = System.Drawing.Color.White;
            this.Button_Home.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_Home.Image = ((System.Drawing.Image)(resources.GetObject("Button_Home.Image")));
            this.Button_Home.ImageSize = new System.Drawing.Size(40, 40);
            this.Button_Home.Location = new System.Drawing.Point(0, 0);
            this.Button_Home.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Home.Name = "Button_Home";
            this.Button_Home.Size = new System.Drawing.Size(216, 85);
            this.Button_Home.TabIndex = 4;
            this.Button_Home.Text = "Home";
            this.Button_Home.Click += new System.EventHandler(this.Button_Home_Click);
            // 
            // Button_set_config_station
            // 
            this.Button_set_config_station.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_set_config_station.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_set_config_station.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_set_config_station.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_set_config_station.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_set_config_station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_set_config_station.FillColor = System.Drawing.Color.DodgerBlue;
            this.Button_set_config_station.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_set_config_station.ForeColor = System.Drawing.Color.White;
            this.Button_set_config_station.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_set_config_station.Image = ((System.Drawing.Image)(resources.GetObject("Button_set_config_station.Image")));
            this.Button_set_config_station.ImageSize = new System.Drawing.Size(60, 60);
            this.Button_set_config_station.Location = new System.Drawing.Point(432, 0);
            this.Button_set_config_station.Margin = new System.Windows.Forms.Padding(0);
            this.Button_set_config_station.Name = "Button_set_config_station";
            this.Button_set_config_station.Size = new System.Drawing.Size(216, 85);
            this.Button_set_config_station.TabIndex = 6;
            this.Button_set_config_station.Text = "Setting Config ";
            this.Button_set_config_station.Click += new System.EventHandler(this.Button_set_config_station_Click);
            // 
            // Button_Management_Data_Station
            // 
            this.Button_Management_Data_Station.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_Management_Data_Station.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_Management_Data_Station.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_Management_Data_Station.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_Management_Data_Station.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_Management_Data_Station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Management_Data_Station.FillColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_Management_Data_Station.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Management_Data_Station.ForeColor = System.Drawing.Color.White;
            this.Button_Management_Data_Station.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_Management_Data_Station.Image = ((System.Drawing.Image)(resources.GetObject("Button_Management_Data_Station.Image")));
            this.Button_Management_Data_Station.ImageSize = new System.Drawing.Size(40, 40);
            this.Button_Management_Data_Station.Location = new System.Drawing.Point(216, 0);
            this.Button_Management_Data_Station.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Management_Data_Station.Name = "Button_Management_Data_Station";
            this.Button_Management_Data_Station.Size = new System.Drawing.Size(216, 85);
            this.Button_Management_Data_Station.TabIndex = 5;
            this.Button_Management_Data_Station.Text = "Management";
            this.Button_Management_Data_Station.Click += new System.EventHandler(this.Button_Management_Data_Station_Click);
            // 
            // Button_Help
            // 
            this.Button_Help.CustomBorderThickness = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.Button_Help.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_Help.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_Help.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_Help.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_Help.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Help.FillColor = System.Drawing.Color.DeepSkyBlue;
            this.Button_Help.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Help.ForeColor = System.Drawing.Color.White;
            this.Button_Help.HoverState.CustomBorderColor = System.Drawing.Color.Blue;
            this.Button_Help.Image = ((System.Drawing.Image)(resources.GetObject("Button_Help.Image")));
            this.Button_Help.ImageSize = new System.Drawing.Size(55, 55);
            this.Button_Help.Location = new System.Drawing.Point(1080, 0);
            this.Button_Help.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Help.Name = "Button_Help";
            this.Button_Help.Size = new System.Drawing.Size(220, 85);
            this.Button_Help.TabIndex = 9;
            this.Button_Help.Text = "Help";
            this.Button_Help.Click += new System.EventHandler(this.Button_Help_Click);
            // 
            // panel_body
            // 
            this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_body.Location = new System.Drawing.Point(0, 71);
            this.panel_body.Margin = new System.Windows.Forms.Padding(2);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(1300, 565);
            this.panel_body.TabIndex = 4;
            this.panel_body.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_body_Paint);
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelHeader;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl2.TargetControl = this.label1;
            this.guna2DragControl2.UseTransparentDrag = true;
            // 
            // guna2DragControl3
            // 
            this.guna2DragControl3.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl3.TargetControl = this.pictureBox1;
            this.guna2DragControl3.UseTransparentDrag = true;
            // 
            // ellipseTool1
            // 
            this.ellipseTool1.CornerRadius = 20;
            this.ellipseTool1.TargetControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 721);
            this.Controls.Add(this.panel_body);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private EllipseTool ellipseTool1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button Button_Home;
        private Guna.UI2.WinForms.Guna2Button Button_Update_Service;
        private Guna.UI2.WinForms.Guna2Button Button_About;
        private Guna.UI2.WinForms.Guna2Button Button_set_config_station;
        private Guna.UI2.WinForms.Guna2Button Button_Management_Data_Station;
        private Guna.UI2.WinForms.Guna2Button Button_Help;
        private System.Windows.Forms.Panel panel_body;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl3;
    }
}

