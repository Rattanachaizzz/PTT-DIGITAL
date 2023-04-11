namespace Auto_Update
{
    partial class UserControl_Update_Service
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_Update_Service));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView11 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.station_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result_Transfer = new System.Windows.Forms.DataGridViewImageColumn();
            this.Result_Upadate = new System.Windows.Forms.DataGridViewImageColumn();
            this.Result_Transfer_bool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result_Update_bool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker11 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker22 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.part = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.rjButton2 = new Auto_Update.RJButton();
            this.rjButton1 = new Auto_Update.RJButton();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("SimSun-ExtB", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(113, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 37);
            this.label1.TabIndex = 12;
            this.label1.Text = "Update Service";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView11
            // 
            this.dataGridView11.AllowUserToAddRows = false;
            this.dataGridView11.AllowUserToDeleteRows = false;
            this.dataGridView11.AllowUserToResizeColumns = false;
            this.dataGridView11.AllowUserToResizeRows = false;
            this.dataGridView11.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView11.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView11.ColumnHeadersHeight = 40;
            this.dataGridView11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView11.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.pbl,
            this.bu,
            this.station_name,
            this.station_ip,
            this.Result_Transfer,
            this.Result_Upadate,
            this.Result_Transfer_bool,
            this.Result_Update_bool});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView11.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridView11.EnableHeadersVisualStyles = false;
            this.dataGridView11.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView11.Location = new System.Drawing.Point(60, 86);
            this.dataGridView11.Name = "dataGridView11";
            this.dataGridView11.ReadOnly = true;
            this.dataGridView11.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView11.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView11.RowHeadersVisible = false;
            this.dataGridView11.RowHeadersWidth = 51;
            this.dataGridView11.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView11.Size = new System.Drawing.Size(1191, 410);
            this.dataGridView11.TabIndex = 14;
            this.dataGridView11.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView11.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView11_CellDoubleClick);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id.Frozen = true;
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // pbl
            // 
            this.pbl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pbl.Frozen = true;
            this.pbl.HeaderText = "Pbl";
            this.pbl.MinimumWidth = 6;
            this.pbl.Name = "pbl";
            this.pbl.ReadOnly = true;
            this.pbl.Width = 80;
            // 
            // bu
            // 
            this.bu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bu.Frozen = true;
            this.bu.HeaderText = "Bu";
            this.bu.MinimumWidth = 6;
            this.bu.Name = "bu";
            this.bu.ReadOnly = true;
            this.bu.Width = 150;
            // 
            // station_name
            // 
            this.station_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.station_name.Frozen = true;
            this.station_name.HeaderText = "Station_Name";
            this.station_name.MinimumWidth = 6;
            this.station_name.Name = "station_name";
            this.station_name.ReadOnly = true;
            this.station_name.Width = 250;
            // 
            // station_ip
            // 
            this.station_ip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.station_ip.Frozen = true;
            this.station_ip.HeaderText = "Station_IP";
            this.station_ip.MinimumWidth = 6;
            this.station_ip.Name = "station_ip";
            this.station_ip.ReadOnly = true;
            this.station_ip.Width = 250;
            // 
            // Result_Transfer
            // 
            this.Result_Transfer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Result_Transfer.Frozen = true;
            this.Result_Transfer.HeaderText = "Result_Transfer";
            this.Result_Transfer.MinimumWidth = 6;
            this.Result_Transfer.Name = "Result_Transfer";
            this.Result_Transfer.ReadOnly = true;
            this.Result_Transfer.Width = 200;
            // 
            // Result_Upadate
            // 
            this.Result_Upadate.Frozen = true;
            this.Result_Upadate.HeaderText = "Result_Upadate";
            this.Result_Upadate.MinimumWidth = 6;
            this.Result_Upadate.Name = "Result_Upadate";
            this.Result_Upadate.ReadOnly = true;
            this.Result_Upadate.Width = 200;
            // 
            // Result_Transfer_bool
            // 
            this.Result_Transfer_bool.HeaderText = "Result_Transfer_bool";
            this.Result_Transfer_bool.Name = "Result_Transfer_bool";
            this.Result_Transfer_bool.ReadOnly = true;
            // 
            // Result_Update_bool
            // 
            this.Result_Update_bool.HeaderText = "Result_Update_bool";
            this.Result_Update_bool.Name = "Result_Update_bool";
            this.Result_Update_bool.ReadOnly = true;
            // 
            // dateTimePicker11
            // 
            this.dateTimePicker11.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dateTimePicker11.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker11.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker11.Location = new System.Drawing.Point(1001, 15);
            this.dateTimePicker11.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker11.Name = "dateTimePicker11";
            this.dateTimePicker11.Size = new System.Drawing.Size(250, 29);
            this.dateTimePicker11.TabIndex = 17;
            // 
            // dateTimePicker22
            // 
            this.dateTimePicker22.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dateTimePicker22.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker22.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker22.Location = new System.Drawing.Point(1001, 48);
            this.dateTimePicker22.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker22.Name = "dateTimePicker22";
            this.dateTimePicker22.Size = new System.Drawing.Size(250, 29);
            this.dateTimePicker22.TabIndex = 18;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(807, 50);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(192, 24);
            this.label15.TabIndex = 20;
            this.label15.Text = "Update Time  :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(806, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(192, 24);
            this.label14.TabIndex = 19;
            this.label14.Text = "Tranfer Time :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(51, 518);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 24);
            this.label2.TabIndex = 21;
            this.label2.Text = "Service Installer :";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(312, 518);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(101, 24);
            this.label33.TabIndex = 22;
            this.label33.Text = "Service";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(55, 15);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // part
            // 
            this.part.AutoSize = true;
            this.part.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.part.ForeColor = System.Drawing.Color.Transparent;
            this.part.Location = new System.Drawing.Point(410, 96);
            this.part.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.part.Name = "part";
            this.part.Size = new System.Drawing.Size(192, 24);
            this.part.TabIndex = 24;
            this.part.Text = "Update Time  :";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // rjButton2
            // 
            this.rjButton2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.rjButton2.BackgroundColor = System.Drawing.Color.MediumSeaGreen;
            this.rjButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton2.BorderRadius = 15;
            this.rjButton2.BorderSize = 0;
            this.rjButton2.FlatAppearance.BorderSize = 0;
            this.rjButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton2.Font = new System.Drawing.Font("SimSun-ExtB", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton2.ForeColor = System.Drawing.Color.White;
            this.rjButton2.Location = new System.Drawing.Point(1097, 505);
            this.rjButton2.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton2.Name = "rjButton2";
            this.rjButton2.Size = new System.Drawing.Size(154, 53);
            this.rjButton2.TabIndex = 16;
            this.rjButton2.Text = "Update";
            this.rjButton2.TextColor = System.Drawing.Color.White;
            this.rjButton2.UseVisualStyleBackColor = false;
            this.rjButton2.Click += new System.EventHandler(this.rjButton2_Click);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.rjButton1.BackgroundColor = System.Drawing.Color.MediumSeaGreen;
            this.rjButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton1.BorderRadius = 15;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("SimSun-ExtB", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(939, 505);
            this.rjButton1.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(154, 53);
            this.rjButton1.TabIndex = 15;
            this.rjButton1.Text = "Transfer";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // UserControl_Update_Service
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dateTimePicker22);
            this.Controls.Add(this.dateTimePicker11);
            this.Controls.Add(this.rjButton2);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.dataGridView11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.part);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserControl_Update_Service";
            this.Size = new System.Drawing.Size(1300, 574);
            this.Load += new System.EventHandler(this.UserControl_Update_Service_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView11;
        private RJButton rjButton1;
        private RJButton rjButton2;
        private System.Windows.Forms.DateTimePicker dateTimePicker11;
        private System.Windows.Forms.DateTimePicker dateTimePicker22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label part;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn bu;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_ip;
        private System.Windows.Forms.DataGridViewImageColumn Result_Transfer;
        private System.Windows.Forms.DataGridViewImageColumn Result_Upadate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result_Transfer_bool;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result_Update_bool;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }
}
