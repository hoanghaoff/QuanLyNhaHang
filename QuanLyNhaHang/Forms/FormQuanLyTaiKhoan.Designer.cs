namespace QuanLyNhaHang.Forms
{
    partial class FormQuanLyTaiKhoan
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
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimeTaoTK = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhauTaiKhoan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMatKhauTaiKhoan = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFullNameNhanVien = new System.Windows.Forms.TextBox();
            this.txtDienThoaiNhanVien = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbHoatDong_Khong = new System.Windows.Forms.RadioButton();
            this.rbHoatDong_Co = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUsernameTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtIDTaiKhoan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbQuyenHan = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboSapXep
            // 
            this.cboSapXep.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Location = new System.Drawing.Point(560, 61);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(198, 33);
            this.cboSapXep.TabIndex = 60;
            this.cboSapXep.SelectedIndexChanged += new System.EventHandler(this.cboSapXep_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(420, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 25);
            this.label3.TabIndex = 58;
            this.label3.Text = "Sắp xếp theo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(840, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 59;
            this.label2.Text = "Tìm kiếm TK:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(977, 62);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(225, 31);
            this.txtTimKiem.TabIndex = 68;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(270, 450);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(93, 36);
            this.btnXoa.TabIndex = 67;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(156, 450);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(93, 36);
            this.btnSua.TabIndex = 66;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(9, 450);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(109, 35);
            this.btnThem.TabIndex = 65;
            this.btnThem.Text = "Thêm TK";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(9, 491);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(354, 31);
            this.btnThoat.TabIndex = 64;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // dgvTaiKhoan
            // 
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column6});
            this.dgvTaiKhoan.Location = new System.Drawing.Point(369, 105);
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.Size = new System.Drawing.Size(674, 417);
            this.dgvTaiKhoan.TabIndex = 63;
            this.dgvTaiKhoan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaiKhoan_CellClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "ID Tài khoản";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Username";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tên Nhân viên";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Điện thoại";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Đang hoạt động";
            this.Column5.Name = "Column5";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Đã bị xóa?";
            this.Column7.Name = "Column7";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Tạo vào lúc";
            this.Column6.Name = "Column6";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTimeTaoTK);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtXacNhanMatKhauTaiKhoan);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMatKhauTaiKhoan);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtFullNameNhanVien);
            this.groupBox1.Controls.Add(this.txtDienThoaiNhanVien);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rbHoatDong_Khong);
            this.groupBox1.Controls.Add(this.rbHoatDong_Co);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtUsernameTaiKhoan);
            this.groupBox1.Controls.Add(this.txtIDTaiKhoan);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 350);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tài khoản:";
            // 
            // txtTimeTaoTK
            // 
            this.txtTimeTaoTK.Location = new System.Drawing.Point(149, 268);
            this.txtTimeTaoTK.Name = "txtTimeTaoTK";
            this.txtTimeTaoTK.ReadOnly = true;
            this.txtTimeTaoTK.Size = new System.Drawing.Size(204, 35);
            this.txtTimeTaoTK.TabIndex = 67;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 270);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 30);
            this.label11.TabIndex = 66;
            this.label11.Text = "Tạo vào lúc:";
            // 
            // txtXacNhanMatKhauTaiKhoan
            // 
            this.txtXacNhanMatKhauTaiKhoan.Location = new System.Drawing.Point(149, 147);
            this.txtXacNhanMatKhauTaiKhoan.Name = "txtXacNhanMatKhauTaiKhoan";
            this.txtXacNhanMatKhauTaiKhoan.Size = new System.Drawing.Size(204, 35);
            this.txtXacNhanMatKhauTaiKhoan.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 30);
            this.label10.TabIndex = 64;
            this.label10.Text = "Xác nhận MK:";
            // 
            // txtMatKhauTaiKhoan
            // 
            this.txtMatKhauTaiKhoan.Location = new System.Drawing.Point(149, 108);
            this.txtMatKhauTaiKhoan.Name = "txtMatKhauTaiKhoan";
            this.txtMatKhauTaiKhoan.Size = new System.Drawing.Size(204, 35);
            this.txtMatKhauTaiKhoan.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 30);
            this.label8.TabIndex = 62;
            this.label8.Text = "Mật khẩu:";
            // 
            // txtFullNameNhanVien
            // 
            this.txtFullNameNhanVien.Location = new System.Drawing.Point(149, 188);
            this.txtFullNameNhanVien.Name = "txtFullNameNhanVien";
            this.txtFullNameNhanVien.ReadOnly = true;
            this.txtFullNameNhanVien.Size = new System.Drawing.Size(204, 35);
            this.txtFullNameNhanVien.TabIndex = 61;
            // 
            // txtDienThoaiNhanVien
            // 
            this.txtDienThoaiNhanVien.Location = new System.Drawing.Point(149, 227);
            this.txtDienThoaiNhanVien.Name = "txtDienThoaiNhanVien";
            this.txtDienThoaiNhanVien.ReadOnly = true;
            this.txtDienThoaiNhanVien.Size = new System.Drawing.Size(204, 35);
            this.txtDienThoaiNhanVien.TabIndex = 54;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 30);
            this.label9.TabIndex = 53;
            this.label9.Text = "Điện thoại:";
            // 
            // rbHoatDong_Khong
            // 
            this.rbHoatDong_Khong.AutoSize = true;
            this.rbHoatDong_Khong.Location = new System.Drawing.Point(259, 303);
            this.rbHoatDong_Khong.Name = "rbHoatDong_Khong";
            this.rbHoatDong_Khong.Size = new System.Drawing.Size(94, 34);
            this.rbHoatDong_Khong.TabIndex = 50;
            this.rbHoatDong_Khong.TabStop = true;
            this.rbHoatDong_Khong.Text = "Không";
            this.rbHoatDong_Khong.UseVisualStyleBackColor = true;
            // 
            // rbHoatDong_Co
            // 
            this.rbHoatDong_Co.AutoSize = true;
            this.rbHoatDong_Co.Location = new System.Drawing.Point(185, 303);
            this.rbHoatDong_Co.Name = "rbHoatDong_Co";
            this.rbHoatDong_Co.Size = new System.Drawing.Size(57, 34);
            this.rbHoatDong_Co.TabIndex = 49;
            this.rbHoatDong_Co.TabStop = true;
            this.rbHoatDong_Co.Text = "Có";
            this.rbHoatDong_Co.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 30);
            this.label7.TabIndex = 47;
            this.label7.Text = "Tên NV:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 30);
            this.label6.TabIndex = 10;
            this.label6.Text = "Đang hoạt động:";
            // 
            // txtUsernameTaiKhoan
            // 
            this.txtUsernameTaiKhoan.Location = new System.Drawing.Point(149, 69);
            this.txtUsernameTaiKhoan.Name = "txtUsernameTaiKhoan";
            this.txtUsernameTaiKhoan.Size = new System.Drawing.Size(204, 35);
            this.txtUsernameTaiKhoan.TabIndex = 9;
            // 
            // txtIDTaiKhoan
            // 
            this.txtIDTaiKhoan.Location = new System.Drawing.Point(149, 30);
            this.txtIDTaiKhoan.Name = "txtIDTaiKhoan";
            this.txtIDTaiKhoan.ReadOnly = true;
            this.txtIDTaiKhoan.Size = new System.Drawing.Size(204, 35);
            this.txtIDTaiKhoan.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "ID Tài khoản:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(434, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 45);
            this.label1.TabIndex = 61;
            this.label1.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // lsbQuyenHan
            // 
            this.lsbQuyenHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbQuyenHan.FormattingEnabled = true;
            this.lsbQuyenHan.ItemHeight = 24;
            this.lsbQuyenHan.Location = new System.Drawing.Point(1049, 153);
            this.lsbQuyenHan.Name = "lsbQuyenHan";
            this.lsbQuyenHan.Size = new System.Drawing.Size(156, 364);
            this.lsbQuyenHan.TabIndex = 69;
            this.lsbQuyenHan.SelectedIndexChanged += new System.EventHandler(this.lsbQuyenHan_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1049, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 25);
            this.label12.TabIndex = 70;
            this.label12.Text = "Quyền hạn:";
            // 
            // FormQuanLyTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 566);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lsbQuyenHan);
            this.Controls.Add(this.cboSapXep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.dgvTaiKhoan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormQuanLyTaiKhoan";
            this.Text = "FormQuanLyTaiKhoan";
            this.Load += new System.EventHandler(this.FormQuanLyTaiKhoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDienThoaiNhanVien;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbHoatDong_Khong;
        private System.Windows.Forms.RadioButton rbHoatDong_Co;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFullNameNhanVien;
        private System.Windows.Forms.TextBox txtUsernameTaiKhoan;
        private System.Windows.Forms.TextBox txtIDTaiKhoan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtXacNhanMatKhauTaiKhoan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMatKhauTaiKhoan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTimeTaoTK;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lsbQuyenHan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Label label12;
    }
}