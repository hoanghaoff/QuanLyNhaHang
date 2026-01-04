namespace QuanLyNhaHang
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeThong_DangKy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeThong_DangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeThong_DangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuanLyPhanQuyen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHeThong_Thoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy_NhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuanLy_KhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuanLy_Ban = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuanLy_DanhMucMonAn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy_MonAn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTinhTrangBan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup_GioiThieuVePhanMem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip_Xinchao = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Xinchao = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerDongHo = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip_Xinchao.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLy,
            this.mnuBanHang,
            this.mnuTinhTrangBan,
            this.mnuHoaDon,
            this.mnuThongKe,
            this.mnuTroGiup_GioiThieuVePhanMem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1215, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong_DangKy,
            this.mnuHeThong_DangNhap,
            this.mnuHeThong_DangXuat,
            this.toolStripSeparator1,
            this.mnuQuanLyPhanQuyen,
            this.mnuQuanLyTaiKhoan,
            this.toolStripSeparator5,
            this.mnuHeThong_Thoat});
            this.mnuHeThong.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuHeThong.Image = ((System.Drawing.Image)(resources.GetObject("mnuHeThong.Image")));
            this.mnuHeThong.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuHeThong.ImageTransparentColor = System.Drawing.Color.White;
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(144, 36);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuHeThong_DangKy
            // 
            this.mnuHeThong_DangKy.Name = "mnuHeThong_DangKy";
            this.mnuHeThong_DangKy.Size = new System.Drawing.Size(274, 34);
            this.mnuHeThong_DangKy.Text = "Đăng ký";
            this.mnuHeThong_DangKy.Click += new System.EventHandler(this.mnuHeThong_DangKy_Click);
            // 
            // mnuHeThong_DangNhap
            // 
            this.mnuHeThong_DangNhap.Name = "mnuHeThong_DangNhap";
            this.mnuHeThong_DangNhap.Size = new System.Drawing.Size(274, 34);
            this.mnuHeThong_DangNhap.Text = "Đăng nhập";
            // 
            // mnuHeThong_DangXuat
            // 
            this.mnuHeThong_DangXuat.Name = "mnuHeThong_DangXuat";
            this.mnuHeThong_DangXuat.Size = new System.Drawing.Size(274, 34);
            this.mnuHeThong_DangXuat.Text = "Đăng xuất";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuQuanLyPhanQuyen
            // 
            this.mnuQuanLyPhanQuyen.Name = "mnuQuanLyPhanQuyen";
            this.mnuQuanLyPhanQuyen.Size = new System.Drawing.Size(274, 34);
            this.mnuQuanLyPhanQuyen.Text = "Quản lý phân quyền";
            this.mnuQuanLyPhanQuyen.Click += new System.EventHandler(this.mnuQuanLyPhanQuyen_Click);
            // 
            // mnuQuanLyTaiKhoan
            // 
            this.mnuQuanLyTaiKhoan.Name = "mnuQuanLyTaiKhoan";
            this.mnuQuanLyTaiKhoan.Size = new System.Drawing.Size(274, 34);
            this.mnuQuanLyTaiKhoan.Text = "Quản lý tài khoản";
            this.mnuQuanLyTaiKhoan.Click += new System.EventHandler(this.mnuQuanLyTaiKhoan_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuHeThong_Thoat
            // 
            this.mnuHeThong_Thoat.Name = "mnuHeThong_Thoat";
            this.mnuHeThong_Thoat.Size = new System.Drawing.Size(274, 34);
            this.mnuHeThong_Thoat.Text = "Thoát";
            // 
            // mnuQuanLy
            // 
            this.mnuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuanLy_NhanVien,
            this.toolStripSeparator4,
            this.mnuQuanLy_KhachHang,
            this.toolStripSeparator2,
            this.mnuQuanLy_Ban,
            this.toolStripSeparator3,
            this.mnuQuanLy_DanhMucMonAn,
            this.mnuQuanLy_MonAn});
            this.mnuQuanLy.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuQuanLy.Image = global::QuanLyNhaHang.Properties.Resources.QuanLy;
            this.mnuQuanLy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(129, 36);
            this.mnuQuanLy.Text = "Quản lý";
            // 
            // mnuQuanLy_NhanVien
            // 
            this.mnuQuanLy_NhanVien.BackColor = System.Drawing.SystemColors.Control;
            this.mnuQuanLy_NhanVien.Name = "mnuQuanLy_NhanVien";
            this.mnuQuanLy_NhanVien.Size = new System.Drawing.Size(259, 34);
            this.mnuQuanLy_NhanVien.Text = "Nhân viên";
            this.mnuQuanLy_NhanVien.Click += new System.EventHandler(this.mnuQuanLyNhanVien_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(256, 6);
            // 
            // mnuQuanLy_KhachHang
            // 
            this.mnuQuanLy_KhachHang.Name = "mnuQuanLy_KhachHang";
            this.mnuQuanLy_KhachHang.Size = new System.Drawing.Size(259, 34);
            this.mnuQuanLy_KhachHang.Text = "Khách hàng";
            this.mnuQuanLy_KhachHang.Click += new System.EventHandler(this.mnuQuanLyKhachHang_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // mnuQuanLy_Ban
            // 
            this.mnuQuanLy_Ban.Name = "mnuQuanLy_Ban";
            this.mnuQuanLy_Ban.Size = new System.Drawing.Size(259, 34);
            this.mnuQuanLy_Ban.Text = "Bàn";
            this.mnuQuanLy_Ban.Click += new System.EventHandler(this.mnuQuanLyBan_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(256, 6);
            // 
            // mnuQuanLy_DanhMucMonAn
            // 
            this.mnuQuanLy_DanhMucMonAn.Name = "mnuQuanLy_DanhMucMonAn";
            this.mnuQuanLy_DanhMucMonAn.Size = new System.Drawing.Size(259, 34);
            this.mnuQuanLy_DanhMucMonAn.Text = "Danh mục món ăn";
            this.mnuQuanLy_DanhMucMonAn.Click += new System.EventHandler(this.mnuQuanLyDanhMucMonAn_Click);
            // 
            // mnuQuanLy_MonAn
            // 
            this.mnuQuanLy_MonAn.Name = "mnuQuanLy_MonAn";
            this.mnuQuanLy_MonAn.Size = new System.Drawing.Size(259, 34);
            this.mnuQuanLy_MonAn.Text = "Món ăn";
            this.mnuQuanLy_MonAn.Click += new System.EventHandler(this.mnuQuanLyMonAn_Click);
            // 
            // mnuBanHang
            // 
            this.mnuBanHang.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuBanHang.Image = global::QuanLyNhaHang.Properties.Resources.BanHang;
            this.mnuBanHang.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuBanHang.Name = "mnuBanHang";
            this.mnuBanHang.Size = new System.Drawing.Size(145, 36);
            this.mnuBanHang.Text = "Bán hàng";
            this.mnuBanHang.Click += new System.EventHandler(this.mnuBanHang_Click);
            // 
            // mnuTinhTrangBan
            // 
            this.mnuTinhTrangBan.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuTinhTrangBan.Image = global::QuanLyNhaHang.Properties.Resources.TinhTrangBan;
            this.mnuTinhTrangBan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuTinhTrangBan.Name = "mnuTinhTrangBan";
            this.mnuTinhTrangBan.Size = new System.Drawing.Size(193, 36);
            this.mnuTinhTrangBan.Text = "Tình trạng bàn";
            this.mnuTinhTrangBan.Click += new System.EventHandler(this.mnuTinhTrangBan_Click);
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuHoaDon.Image = global::QuanLyNhaHang.Properties.Resources.HoaDon;
            this.mnuHoaDon.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(138, 36);
            this.mnuHoaDon.Text = "Hóa đơn";
            this.mnuHoaDon.Click += new System.EventHandler(this.mnuHoaDon_Click);
            // 
            // mnuThongKe
            // 
            this.mnuThongKe.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuThongKe.Image = global::QuanLyNhaHang.Properties.Resources.ThongKe;
            this.mnuThongKe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuThongKe.Name = "mnuThongKe";
            this.mnuThongKe.Size = new System.Drawing.Size(143, 36);
            this.mnuThongKe.Text = "Thống kê";
            this.mnuThongKe.Click += new System.EventHandler(this.mnuThongKe_Click);
            // 
            // mnuTroGiup_GioiThieuVePhanMem
            // 
            this.mnuTroGiup_GioiThieuVePhanMem.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.mnuTroGiup_GioiThieuVePhanMem.Image = global::QuanLyNhaHang.Properties.Resources.Help1;
            this.mnuTroGiup_GioiThieuVePhanMem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuTroGiup_GioiThieuVePhanMem.Name = "mnuTroGiup_GioiThieuVePhanMem";
            this.mnuTroGiup_GioiThieuVePhanMem.Size = new System.Drawing.Size(147, 36);
            this.mnuTroGiup_GioiThieuVePhanMem.Text = "Giới thiệu";
            this.mnuTroGiup_GioiThieuVePhanMem.Click += new System.EventHandler(this.mnuTroGiup_GioiThieuVePhanMem_Click);
            // 
            // statusStrip_Xinchao
            // 
            this.statusStrip_Xinchao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Xinchao});
            this.statusStrip_Xinchao.Location = new System.Drawing.Point(0, 571);
            this.statusStrip_Xinchao.Name = "statusStrip_Xinchao";
            this.statusStrip_Xinchao.Size = new System.Drawing.Size(1215, 22);
            this.statusStrip_Xinchao.TabIndex = 3;
            this.statusStrip_Xinchao.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Xinchao
            // 
            this.toolStripStatusLabel_Xinchao.Name = "toolStripStatusLabel_Xinchao";
            this.toolStripStatusLabel_Xinchao.Size = new System.Drawing.Size(62, 17);
            this.toolStripStatusLabel_Xinchao.Text = "statusStrip";
            // 
            // timerDongHo
            // 
            this.timerDongHo.Enabled = true;
            this.timerDongHo.Interval = 1000;
            this.timerDongHo.Tick += new System.EventHandler(this.timerDongHo_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QuanLyNhaHang.Properties.Resources.MAINPICTURE;
            this.ClientSize = new System.Drawing.Size(1215, 593);
            this.Controls.Add(this.statusStrip_Xinchao);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "PHẦN MỀM QUẢN LÝ NHÀ HÀNG HHPC";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip_Xinchao.ResumeLayout(false);
            this.statusStrip_Xinchao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong_DangNhap;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong_DangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong_Thoat;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy_NhanVien;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy_KhachHang;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy_Ban;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy_DanhMucMonAn;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy_MonAn;
        private System.Windows.Forms.ToolStripMenuItem mnuBanHang;
        private System.Windows.Forms.ToolStripMenuItem mnuTinhTrangBan;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKe;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup_GioiThieuVePhanMem;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong_DangKy;
        private System.Windows.Forms.StatusStrip statusStrip_Xinchao;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Xinchao;
        private System.Windows.Forms.Timer timerDongHo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyPhanQuyen;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyTaiKhoan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}