namespace ROM
{
    partial class frmReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnPrintPdf = new Guna.UI2.WinForms.Guna2Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.btnPrintPdfToDay = new Guna.UI2.WinForms.Guna2Button();
            this.btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Athiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimePicker1.Location = new System.Drawing.Point(442, 372);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 28);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Athiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimePicker2.Location = new System.Drawing.Point(442, 422);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(180, 28);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // btnPrintPdf
            // 
            this.btnPrintPdf.AutoRoundedCorners = true;
            this.btnPrintPdf.BackColor = System.Drawing.Color.Transparent;
            this.btnPrintPdf.BorderRadius = 21;
            this.btnPrintPdf.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrintPdf.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrintPdf.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrintPdf.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrintPdf.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrintPdf.Font = new System.Drawing.Font("Athiti", 9.75F);
            this.btnPrintPdf.ForeColor = System.Drawing.Color.White;
            this.btnPrintPdf.Location = new System.Drawing.Point(442, 477);
            this.btnPrintPdf.Name = "btnPrintPdf";
            this.btnPrintPdf.Size = new System.Drawing.Size(180, 45);
            this.btnPrintPdf.TabIndex = 2;
            this.btnPrintPdf.Text = "สร้างรายงานรายได้รายเดือน";
            this.btnPrintPdf.UseTransparentBackground = true;
            this.btnPrintPdf.Click += new System.EventHandler(this.btnPrintPdf_Click_1);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CalendarFont = new System.Drawing.Font("Athiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker3.Font = new System.Drawing.Font("Athiti", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimePicker3.Location = new System.Drawing.Point(442, 206);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(180, 28);
            this.dateTimePicker3.TabIndex = 3;
            // 
            // btnPrintPdfToDay
            // 
            this.btnPrintPdfToDay.AutoRoundedCorners = true;
            this.btnPrintPdfToDay.BackColor = System.Drawing.Color.Transparent;
            this.btnPrintPdfToDay.BorderRadius = 21;
            this.btnPrintPdfToDay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrintPdfToDay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrintPdfToDay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrintPdfToDay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrintPdfToDay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrintPdfToDay.Font = new System.Drawing.Font("Athiti", 9.75F);
            this.btnPrintPdfToDay.ForeColor = System.Drawing.Color.White;
            this.btnPrintPdfToDay.Location = new System.Drawing.Point(442, 251);
            this.btnPrintPdfToDay.Name = "btnPrintPdfToDay";
            this.btnPrintPdfToDay.Size = new System.Drawing.Size(180, 45);
            this.btnPrintPdfToDay.TabIndex = 4;
            this.btnPrintPdfToDay.Text = "สร้างรายงานรายได้รายวัน";
            this.btnPrintPdfToDay.UseTransparentBackground = true;
            this.btnPrintPdfToDay.Click += new System.EventHandler(this.btnPrintPdfToDay_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.CustomClick = true;
            this.btnExit.FillColor = System.Drawing.Color.Red;
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(651, 9);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 15;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.Image = global::ROM.Properties.Resources.Special_Menu_Sushi_Sale_Instagram_Post2;
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(-7, 0);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(710, 700);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox3.TabIndex = 16;
            this.guna2PictureBox3.TabStop = false;
            this.guna2PictureBox3.UseTransparentBackground = true;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 691);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrintPdfToDay);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.btnPrintPdf);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.guna2PictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReport";
            this.Text = "frmReport";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private Guna.UI2.WinForms.Guna2Button btnPrintPdf;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private Guna.UI2.WinForms.Guna2Button btnPrintPdfToDay;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
    }
}