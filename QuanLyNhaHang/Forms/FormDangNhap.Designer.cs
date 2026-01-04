namespace QuanLyNhaHang.Forms
{
    partial class FormDangNhap
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linklabel_DangKyTaiKhoan = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnThoatLogin = new System.Windows.Forms.Button();
            this.btnDangNhapLogin = new System.Windows.Forms.Button();
            this.txtPasswordLogin = new System.Windows.Forms.TextBox();
            this.txtUsernameLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1001, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chào mừng đến với Phần mềm quản lý nhà hàng HHPC !";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linklabel_DangKyTaiKhoan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnThoatLogin);
            this.groupBox1.Controls.Add(this.btnDangNhapLogin);
            this.groupBox1.Controls.Add(this.txtPasswordLogin);
            this.groupBox1.Controls.Add(this.txtUsernameLogin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(371, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 324);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vui lòng đăng nhập!";
            // 
            // linklabel_DangKyTaiKhoan
            // 
            this.linklabel_DangKyTaiKhoan.AutoSize = true;
            this.linklabel_DangKyTaiKhoan.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklabel_DangKyTaiKhoan.Location = new System.Drawing.Point(271, 292);
            this.linklabel_DangKyTaiKhoan.Name = "linklabel_DangKyTaiKhoan";
            this.linklabel_DangKyTaiKhoan.Size = new System.Drawing.Size(48, 22);
            this.linklabel_DangKyTaiKhoan.TabIndex = 7;
            this.linklabel_DangKyTaiKhoan.TabStop = true;
            this.linklabel_DangKyTaiKhoan.Text = "đây.";
            this.linklabel_DangKyTaiKhoan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Linklabel_DangKyTaiKhoan_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chưa có tài khoản? Đăng ký tại";
            // 
            // btnThoatLogin
            // 
            this.btnThoatLogin.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoatLogin.Location = new System.Drawing.Point(140, 254);
            this.btnThoatLogin.Name = "btnThoatLogin";
            this.btnThoatLogin.Size = new System.Drawing.Size(160, 26);
            this.btnThoatLogin.TabIndex = 5;
            this.btnThoatLogin.Text = "THOÁT PHẦN MỀM";
            this.btnThoatLogin.UseVisualStyleBackColor = true;
            this.btnThoatLogin.Click += new System.EventHandler(this.BtnThoatLogin_Click);
            // 
            // btnDangNhapLogin
            // 
            this.btnDangNhapLogin.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhapLogin.Location = new System.Drawing.Point(116, 198);
            this.btnDangNhapLogin.Name = "btnDangNhapLogin";
            this.btnDangNhapLogin.Size = new System.Drawing.Size(207, 38);
            this.btnDangNhapLogin.TabIndex = 4;
            this.btnDangNhapLogin.Text = "ĐĂNG NHẬP";
            this.btnDangNhapLogin.UseVisualStyleBackColor = true;
            this.btnDangNhapLogin.Click += new System.EventHandler(this.BtnDangNhapLogin_Click);
            // 
            // txtPasswordLogin
            // 
            this.txtPasswordLogin.Location = new System.Drawing.Point(134, 134);
            this.txtPasswordLogin.Name = "txtPasswordLogin";
            this.txtPasswordLogin.Size = new System.Drawing.Size(286, 43);
            this.txtPasswordLogin.TabIndex = 3;
            this.txtPasswordLogin.UseSystemPasswordChar = true;
            // 
            // txtUsernameLogin
            // 
            this.txtUsernameLogin.Location = new System.Drawing.Point(134, 79);
            this.txtUsernameLogin.Name = "txtUsernameLogin";
            this.txtUsernameLogin.Size = new System.Drawing.Size(286, 43);
            this.txtUsernameLogin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // FormDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 548);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormDangNhap";
            this.Text = "FormDangNhap";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPasswordLogin;
        private System.Windows.Forms.TextBox txtUsernameLogin;
        private System.Windows.Forms.LinkLabel linklabel_DangKyTaiKhoan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThoatLogin;
        private System.Windows.Forms.Button btnDangNhapLogin;
    }
}