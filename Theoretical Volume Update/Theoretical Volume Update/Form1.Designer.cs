namespace Theoretical_Volume_Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.หมายเลขถัง = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ชื่อถัง = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ผลิตภัณฑ์ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ปริมาณวัด = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.หมายเลขถัง,
            this.ชื่อถัง,
            this.ผลิตภัณฑ์,
            this.ปริมาณวัด});
            this.dataGridView1.Location = new System.Drawing.Point(12, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(804, 406);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(591, 84);
            this.label1.TabIndex = 1;
            this.label1.Text = "  Theoretical Volume Update";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(551, 508);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(687, 508);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 39);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // หมายเลขถัง
            // 
            this.หมายเลขถัง.HeaderText = "หมายเลขถัง";
            this.หมายเลขถัง.MinimumWidth = 6;
            this.หมายเลขถัง.Name = "หมายเลขถัง";
            this.หมายเลขถัง.ReadOnly = true;
            // 
            // ชื่อถัง
            // 
            this.ชื่อถัง.HeaderText = "ชื่อถัง";
            this.ชื่อถัง.MinimumWidth = 6;
            this.ชื่อถัง.Name = "ชื่อถัง";
            this.ชื่อถัง.ReadOnly = true;
            this.ชื่อถัง.Width = 150;
            // 
            // ผลิตภัณฑ์
            // 
            this.ผลิตภัณฑ์.HeaderText = "ผลิตภัณฑ์";
            this.ผลิตภัณฑ์.MinimumWidth = 6;
            this.ผลิตภัณฑ์.Name = "ผลิตภัณฑ์";
            this.ผลิตภัณฑ์.ReadOnly = true;
            this.ผลิตภัณฑ์.Width = 150;
            // 
            // ปริมาณวัด
            // 
            this.ปริมาณวัด.HeaderText = "ปริมาณวัด";
            this.ปริมาณวัด.MinimumWidth = 6;
            this.ปริมาณวัด.Name = "ปริมาณวัด";
            this.ปริมาณวัด.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 559);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Theoretical Volume Update";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn หมายเลขถัง;
        private System.Windows.Forms.DataGridViewTextBoxColumn ชื่อถัง;
        private System.Windows.Forms.DataGridViewTextBoxColumn ผลิตภัณฑ์;
        private System.Windows.Forms.DataGridViewTextBoxColumn ปริมาณวัด;
    }
}

