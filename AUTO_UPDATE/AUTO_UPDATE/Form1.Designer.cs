namespace AUTO_UPDATE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.station_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispenser = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tankgauge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.moniter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.web = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.API = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gaia = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Other = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Time_Transfer_File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_Update_File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTransferFile = new System.Windows.Forms.Button();
            this.btnUpdateFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.circlelb4 = new AUTO_UPDATE.Circlelb();
            this.circlelb3 = new AUTO_UPDATE.Circlelb();
            this.circlelb2 = new AUTO_UPDATE.Circlelb();
            this.circlelb1 = new AUTO_UPDATE.Circlelb();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.station_id,
            this.station_code,
            this.station_name,
            this.station_IP,
            this.dispenser,
            this.tankgauge,
            this.moniter,
            this.web,
            this.API,
            this.gaia,
            this.Other,
            this.Time_Transfer_File,
            this.Time_Update_File});
            this.dataGridView1.Location = new System.Drawing.Point(402, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(772, 636);
            this.dataGridView1.TabIndex = 0;
            // 
            // station_id
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.station_id.DefaultCellStyle = dataGridViewCellStyle1;
            this.station_id.HeaderText = "Station_Id";
            this.station_id.MinimumWidth = 6;
            this.station_id.Name = "station_id";
            this.station_id.Width = 70;
            // 
            // station_code
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.station_code.DefaultCellStyle = dataGridViewCellStyle2;
            this.station_code.HeaderText = "Station_Code";
            this.station_code.MinimumWidth = 6;
            this.station_code.Name = "station_code";
            this.station_code.Width = 125;
            // 
            // station_name
            // 
            this.station_name.HeaderText = "Station_Name";
            this.station_name.MinimumWidth = 6;
            this.station_name.Name = "station_name";
            this.station_name.Width = 150;
            // 
            // station_IP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.station_IP.DefaultCellStyle = dataGridViewCellStyle3;
            this.station_IP.HeaderText = "Station_IP";
            this.station_IP.MinimumWidth = 6;
            this.station_IP.Name = "station_IP";
            this.station_IP.Width = 150;
            // 
            // dispenser
            // 
            this.dispenser.FalseValue = "False";
            this.dispenser.HeaderText = "Dispenser";
            this.dispenser.IndeterminateValue = "";
            this.dispenser.MinimumWidth = 6;
            this.dispenser.Name = "dispenser";
            this.dispenser.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dispenser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dispenser.TrueValue = "True";
            this.dispenser.Width = 125;
            // 
            // tankgauge
            // 
            this.tankgauge.FalseValue = "False";
            this.tankgauge.HeaderText = "Tankgauge";
            this.tankgauge.MinimumWidth = 6;
            this.tankgauge.Name = "tankgauge";
            this.tankgauge.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tankgauge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.tankgauge.TrueValue = "True";
            this.tankgauge.Width = 125;
            // 
            // moniter
            // 
            this.moniter.FalseValue = "False";
            this.moniter.HeaderText = "Moniter";
            this.moniter.MinimumWidth = 6;
            this.moniter.Name = "moniter";
            this.moniter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.moniter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.moniter.TrueValue = "True";
            this.moniter.Width = 125;
            // 
            // web
            // 
            this.web.FalseValue = "False";
            this.web.HeaderText = "Web";
            this.web.MinimumWidth = 6;
            this.web.Name = "web";
            this.web.TrueValue = "True";
            this.web.Width = 125;
            // 
            // API
            // 
            this.API.FalseValue = "False";
            this.API.HeaderText = "API";
            this.API.MinimumWidth = 6;
            this.API.Name = "API";
            this.API.TrueValue = "True";
            this.API.Width = 125;
            // 
            // gaia
            // 
            this.gaia.FalseValue = "False";
            this.gaia.HeaderText = "Gaia";
            this.gaia.MinimumWidth = 6;
            this.gaia.Name = "gaia";
            this.gaia.TrueValue = "True";
            this.gaia.Width = 125;
            // 
            // Other
            // 
            this.Other.FalseValue = "False";
            this.Other.HeaderText = "Other";
            this.Other.MinimumWidth = 6;
            this.Other.Name = "Other";
            this.Other.TrueValue = "True";
            this.Other.Width = 125;
            // 
            // Time_Transfer_File
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Time_Transfer_File.DefaultCellStyle = dataGridViewCellStyle4;
            this.Time_Transfer_File.HeaderText = "Time Transfer File";
            this.Time_Transfer_File.MinimumWidth = 6;
            this.Time_Transfer_File.Name = "Time_Transfer_File";
            this.Time_Transfer_File.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Time_Transfer_File.Width = 125;
            // 
            // Time_Update_File
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Time_Update_File.DefaultCellStyle = dataGridViewCellStyle5;
            this.Time_Update_File.HeaderText = "Time Update File";
            this.Time_Update_File.MinimumWidth = 6;
            this.Time_Update_File.Name = "Time_Update_File";
            this.Time_Update_File.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Time_Update_File.Width = 125;
            // 
            // btnTransferFile
            // 
            this.btnTransferFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTransferFile.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferFile.Location = new System.Drawing.Point(8, 559);
            this.btnTransferFile.Name = "btnTransferFile";
            this.btnTransferFile.Size = new System.Drawing.Size(389, 37);
            this.btnTransferFile.TabIndex = 7;
            this.btnTransferFile.Text = "Transfer File";
            this.btnTransferFile.UseVisualStyleBackColor = true;
            this.btnTransferFile.Click += new System.EventHandler(this.btnTransferFile_Click);
            // 
            // btnUpdateFile
            // 
            this.btnUpdateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateFile.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateFile.Location = new System.Drawing.Point(8, 599);
            this.btnUpdateFile.Name = "btnUpdateFile";
            this.btnUpdateFile.Size = new System.Drawing.Size(389, 37);
            this.btnUpdateFile.TabIndex = 8;
            this.btnUpdateFile.Text = "Update File";
            this.btnUpdateFile.UseVisualStyleBackColor = true;
            this.btnUpdateFile.Click += new System.EventHandler(this.btnUpdateFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Process Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Import File Config ";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(349, 210);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(42, 36);
            this.btnBrowseFile.TabIndex = 11;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label5.Location = new System.Drawing.Point(15, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(374, 35);
            this.label5.TabIndex = 12;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Font = new System.Drawing.Font("Bahnschrift Condensed", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 72);
            this.label1.TabIndex = 13;
            this.label1.Text = "AUTO UPDATE";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(9, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(388, 132);
            this.label2.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(143, 404);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 24);
            this.label6.TabIndex = 18;
            this.label6.Text = "Transfer File ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(141, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 24);
            this.label7.TabIndex = 19;
            this.label7.Text = "Start Process";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(143, 459);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 24);
            this.label8.TabIndex = 20;
            this.label8.Text = "Update File ";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label9.Location = new System.Drawing.Point(82, 349);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 180);
            this.label9.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label10.Location = new System.Drawing.Point(9, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(388, 51);
            this.label10.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(9, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(388, 70);
            this.label11.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label12.Location = new System.Drawing.Point(9, 262);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(388, 51);
            this.label12.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label13.Location = new System.Drawing.Point(9, 313);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(388, 243);
            this.label13.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label14.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(143, 515);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 24);
            this.label14.TabIndex = 26;
            this.label14.Text = "End Process";
            // 
            // circlelb4
            // 
            this.circlelb4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.circlelb4.Location = new System.Drawing.Point(67, 502);
            this.circlelb4.Name = "circlelb4";
            this.circlelb4.Size = new System.Drawing.Size(40, 40);
            this.circlelb4.TabIndex = 27;
            // 
            // circlelb3
            // 
            this.circlelb3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.circlelb3.Location = new System.Drawing.Point(67, 447);
            this.circlelb3.Name = "circlelb3";
            this.circlelb3.Size = new System.Drawing.Size(40, 40);
            this.circlelb3.TabIndex = 17;
            // 
            // circlelb2
            // 
            this.circlelb2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.circlelb2.Location = new System.Drawing.Point(67, 394);
            this.circlelb2.Name = "circlelb2";
            this.circlelb2.Size = new System.Drawing.Size(40, 40);
            this.circlelb2.TabIndex = 16;
            // 
            // circlelb1
            // 
            this.circlelb1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.circlelb1.Location = new System.Drawing.Point(67, 340);
            this.circlelb1.Name = "circlelb1";
            this.circlelb1.Size = new System.Drawing.Size(40, 40);
            this.circlelb1.TabIndex = 15;
            this.circlelb1.Click += new System.EventHandler(this.circlelb1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1176, 641);
            this.Controls.Add(this.circlelb4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.circlelb3);
            this.Controls.Add(this.circlelb2);
            this.Controls.Add(this.circlelb1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUpdateFile);
            this.Controls.Add(this.btnTransferFile);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AUTO UPDATE ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnUpdateFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Circlelb circlelb1;
        private Circlelb circlelb2;
        private Circlelb circlelb3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private Circlelb circlelb4;
        public System.Windows.Forms.Button btnTransferFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_IP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dispenser;
        private System.Windows.Forms.DataGridViewCheckBoxColumn tankgauge;
        private System.Windows.Forms.DataGridViewCheckBoxColumn moniter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn web;
        private System.Windows.Forms.DataGridViewCheckBoxColumn API;
        private System.Windows.Forms.DataGridViewCheckBoxColumn gaia;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Other;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Transfer_File;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_Update_File;
    }
}

