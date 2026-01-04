namespace QuanLyNhaHang.Forms
{
    partial class FormBeforeDangKy
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBeforeDangKyThoat = new System.Windows.Forms.Button();
            this.btnBeforeDangKyLogin = new System.Windows.Forms.Button();
            this.txtBeforeDangKyPasswordLogin = new System.Windows.Forms.TextBox();
            this.txtBeforeDangKyUsernameLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBeforeDangKyThoat);
            this.groupBox1.Controls.Add(this.btnBeforeDangKyLogin);
            this.groupBox1.Controls.Add(this.txtBeforeDangKyPasswordLogin);
            this.groupBox1.Controls.Add(this.txtBeforeDangKyUsernameLogin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 295);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vui lòng đăng nhập Tài khoản Chủ nhà hàng để đăng ký!";
            // 
            // btnBeforeDangKyThoat
            // 
            this.btnBeforeDangKyThoat.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeforeDangKyThoat.Location = new System.Drawing.Point(27, 234);
            this.btnBeforeDangKyThoat.Name = "btnBeforeDangKyThoat";
            this.btnBeforeDangKyThoat.Size = new System.Drawing.Size(131, 38);
            this.btnBeforeDangKyThoat.TabIndex = 5;
            this.btnBeforeDangKyThoat.Text = "THOÁT";
            this.btnBeforeDangKyThoat.UseVisualStyleBackColor = true;
            // 
            // btnBeforeDangKyLogin
            // 
            this.btnBeforeDangKyLogin.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeforeDangKyLogin.Location = new System.Drawing.Point(213, 234);
            this.btnBeforeDangKyLogin.Name = "btnBeforeDangKyLogin";
            this.btnBeforeDangKyLogin.Size = new System.Drawing.Size(207, 38);
            this.btnBeforeDangKyLogin.TabIndex = 4;
            this.btnBeforeDangKyLogin.Text = "ĐĂNG NHẬP";
            this.btnBeforeDangKyLogin.UseVisualStyleBackColor = true;
            this.btnBeforeDangKyLogin.Click += new System.EventHandler(this.BtnBeforeDangKyLogin_Click);
            // 
            // txtBeforeDangKyPasswordLogin
            // 
            this.txtBeforeDangKyPasswordLogin.Location = new System.Drawing.Point(134, 165);
            this.txtBeforeDangKyPasswordLogin.Name = "txtBeforeDangKyPasswordLogin";
            this.txtBeforeDangKyPasswordLogin.Size = new System.Drawing.Size(286, 43);
            this.txtBeforeDangKyPasswordLogin.TabIndex = 3;
            this.txtBeforeDangKyPasswordLogin.UseSystemPasswordChar = true;
            // 
            // txtBeforeDangKyUsernameLogin
            // 
            this.txtBeforeDangKyUsernameLogin.Location = new System.Drawing.Point(134, 110);
            this.txtBeforeDangKyUsernameLogin.Name = "txtBeforeDangKyUsernameLogin";
            this.txtBeforeDangKyUsernameLogin.Size = new System.Drawing.Size(286, 43);
            this.txtBeforeDangKyUsernameLogin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // FormBeforeDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 318);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormBeforeDangKy";
            this.Text = "FormBeforeDangKy";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBeforeDangKyThoat;
        private System.Windows.Forms.Button btnBeforeDangKyLogin;
        private System.Windows.Forms.TextBox txtBeforeDangKyPasswordLogin;
        private System.Windows.Forms.TextBox txtBeforeDangKyUsernameLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}