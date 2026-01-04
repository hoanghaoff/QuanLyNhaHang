namespace RestaurantApp
{
    partial class FormTinhTrangBan
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
            this.flpBan = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBan = new System.Windows.Forms.Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTenBan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flpBan.SuspendLayout();
            this.pnlBan.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpBan
            // 
            this.flpBan.Controls.Add(this.pnlBan);
            this.flpBan.Location = new System.Drawing.Point(12, 49);
            this.flpBan.Name = "flpBan";
            this.flpBan.Size = new System.Drawing.Size(1187, 492);
            this.flpBan.TabIndex = 0;
            // 
            // pnlBan
            // 
            this.pnlBan.Controls.Add(this.lblTrangThai);
            this.pnlBan.Controls.Add(this.comboBox1);
            this.pnlBan.Controls.Add(this.lblTenBan);
            this.pnlBan.Location = new System.Drawing.Point(3, 3);
            this.pnlBan.Name = "pnlBan";
            this.pnlBan.Size = new System.Drawing.Size(216, 128);
            this.pnlBan.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Location = new System.Drawing.Point(17, 96);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(62, 15);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Trống",
            "Có khách",
            "Đã đặt",
            "Bảo trì"});
            this.comboBox1.Location = new System.Drawing.Point(80, 86);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 33);
            this.comboBox1.TabIndex = 1;
            // 
            // lblTenBan
            // 
            this.lblTenBan.AutoSize = true;
            this.lblTenBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenBan.Location = new System.Drawing.Point(60, 29);
            this.lblTenBan.Name = "lblTenBan";
            this.lblTenBan.Size = new System.Drawing.Size(86, 25);
            this.lblTenBan.TabIndex = 0;
            this.lblTenBan.Text = "Bàn 01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(467, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 37);
            this.label1.TabIndex = 14;
            this.label1.Text = "TÌNH TRẠNG BÀN";
            // 
            // FormTinhTrangBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 553);
            this.Controls.Add(this.flpBan);
            this.Controls.Add(this.label1);
            this.Name = "FormTinhTrangBan";
            this.Text = "FormTinhTrangBan";
            this.flpBan.ResumeLayout(false);
            this.pnlBan.ResumeLayout(false);
            this.pnlBan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpBan;
        private System.Windows.Forms.Panel pnlBan;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTenBan;
        private System.Windows.Forms.Label label1;
    }
}