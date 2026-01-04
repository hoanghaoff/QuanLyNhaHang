using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormHoaDon : Form
    {
        private readonly string connStr =
            @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        public FormHoaDon()
        {
            InitializeComponent();

            Load += FormHoaDon_Load;
            dgvHoaDon.CellClick += dgvHoaDon_CellClick;
            btnXacNhanThanhToan.Click += btnXacNhanThanhToan_Click;
            btnThoat.Click += btnThoat_Click;
            btnInHoaDon.Click += btnInHoaDon_Click;
            cboLoaiKH.SelectedIndexChanged += cboLoaiKH_SelectedIndexChanged;
            txtKhuyenMai.TextChanged += txtKhuyenMai_TextChanged;
            cboHinhThucThanhToan.SelectedIndexChanged += cboHinhThucThanhToan_SelectedIndexChanged;

            // filter lọc
            dtpTuNgay.ValueChanged += (s, e) => LoadDanhSachHoaDon();
            dtpDenNgay.ValueChanged += (s, e) => LoadDanhSachHoaDon();
            cboTrangThaiThanhToan.SelectedIndexChanged += (s, e) => LoadDanhSachHoaDon();
            cboNhanVienLap.SelectedIndexChanged += (s, e) => LoadDanhSachHoaDon();
            cboBanSo.SelectedIndexChanged += (s, e) => LoadDanhSachHoaDon();
            cboSapXep.SelectedIndexChanged += (s, e) => LoadDanhSachHoaDon();
            txtTimKiem.TextChanged += (s, e) => LoadDanhSachHoaDon();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            // load trạng thái hóa đơn
            cboTrangThaiThanhToan.Items.AddRange(new string[]
            { "Tất cả", "Chưa thanh toán", "Đã thanh toán" });
            cboTrangThaiThanhToan.SelectedIndex = 0;

            // load cbo loại khách hàng
            cboLoaiKH.Items.AddRange(new string[] { "Khách thường", "Khách VIP" });
            cboLoaiKH.SelectedIndex = 0;

            // cbo hình thức thanh toán
            cboHinhThucThanhToan.Items.AddRange(new string[] { "Tiền mặt", "Chuyển khoản" });
            cboHinhThucThanhToan.SelectedIndex = 0;

            // sắp xếp
            InitSapXep();

            
            LoadNhanVien();
            LoadBan();

            LoadDanhSachHoaDon();
            ResetForm();
        }

        private void InitSapXep()
        {
            cboSapXep.Items.AddRange(new string[]
            {
                "Mặc định (Ngày mới nhất)",
                "Mã HD tăng dần",
                "Mã HD giảm dần",
                "Tên NV A-Z",
                "Tên NV Z-A",
                "Tổng tiền tăng dần",
                "Tổng tiền giảm dần"
            });
            cboSapXep.SelectedIndex = 0;
        }

        // load nv
        private void LoadNhanVien()
        {
            cboNhanVienLap.Items.Clear();
            cboNhanVienLap.Items.Add("Tất cả");
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT MaNV, TenNV FROM NhanVien 
                    WHERE TrangThai = N'Còn làm' ORDER BY TenNV", conn);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cboNhanVienLap.Items.Add(new NhanVienItem
                        {
                            MaNV = rd.GetInt32(0),
                            TenNV = rd.GetString(1)
                        });
                    }
                }
            }
            cboNhanVienLap.SelectedIndex = 0;
        }

        //load bàn
        private void LoadBan()
        {
            cboBanSo.Items.Clear();
            cboBanSo.Items.Add("Tất cả");
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT MaBan, TenBan FROM Ban ORDER BY TenBan", conn);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cboBanSo.Items.Add(new BanItem
                        {
                            MaBan = rd.GetInt32(0),
                            TenBan = rd.GetString(1)
                        });
                    }
                }
            }
            cboBanSo.SelectedIndex = 0;
        }

        // load ds hóa đơn
        private void LoadDanhSachHoaDon()
        {
            dgvHoaDon.Rows.Clear();
            dgvHoaDon.Columns.Clear();

            dgvHoaDon.Columns.Add("MaHD", "Mã HD");
            dgvHoaDon.Columns.Add("NgayLap", "Ngày lập");
            dgvHoaDon.Columns.Add("TenBan", "Bàn");
            dgvHoaDon.Columns.Add("LoaiBan", "Loại bàn");
            dgvHoaDon.Columns.Add("TenNV", "Nhân viên");
            dgvHoaDon.Columns.Add("TenKH", "Khách hàng");
            dgvHoaDon.Columns.Add("TongTien", "Tổng tiền");
            dgvHoaDon.Columns.Add("KhuyenMai", "Khuyến mãi (%)");
            dgvHoaDon.Columns.Add("TrangThai", "Trạng thái");
            dgvHoaDon.Columns.Add("HinhThuc", "Hình thức");

            string whereClause = "WHERE 1=1";

            // date
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            whereClause += " AND h.NgayLap BETWEEN @tu AND @den";

            // trang thai thanh toan
            string trangThai = cboTrangThaiThanhToan.SelectedItem?.ToString();
            if (trangThai == "Chưa thanh toán")
                whereClause += " AND h.TrangThaiThanhToan = N'Chưa thanh toán'";
            else if (trangThai == "Đã thanh toán")
                whereClause += " AND h.TrangThaiThanhToan = N'Đã thanh toán'";

            // NV
            if (cboNhanVienLap.SelectedIndex > 0)
            {
                int maNV = ((NhanVienItem)cboNhanVienLap.SelectedItem).MaNV;
                whereClause += " AND h.MaNV = @manv";
            }

            // bàn
            if (cboBanSo.SelectedIndex > 0)
            {
                int maBan = ((BanItem)cboBanSo.SelectedItem).MaBan;
                whereClause += " AND h.MaBan = @maban";
            }

            // tìm kiếm
            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
                whereClause += " AND (CAST(h.MaHoaDon AS NVARCHAR) LIKE @kw OR nv.TenNV LIKE @kw OR b.TenBan LIKE @kw)";

            // sắp xếp
            string orderBy = "h.NgayLap DESC";
            switch (cboSapXep.SelectedIndex)
            {
                case 1: orderBy = "h.MaHoaDon ASC"; break;
                case 2: orderBy = "h.MaHoaDon DESC"; break;
                case 3: orderBy = "nv.TenNV ASC"; break;
                case 4: orderBy = "nv.TenNV DESC"; break;
                case 5: orderBy = "h.TongTien ASC"; break;
                case 6: orderBy = "h.TongTien DESC"; break;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $@"
            SELECT
                h.MaHoaDon,
                h.NgayLap,
                b.TenBan,
                b.LoaiBan,
                nv.TenNV,
                ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                h.TongTien,
                h.KhuyenMai,
                h.TrangThaiThanhToan,
                ISNULL(h.HinhThucThanhToan, N'Tiền mặt') AS HinhThuc
            FROM HoaDon h
            INNER JOIN Ban b ON h.MaBan = b.MaBan
            INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
            LEFT JOIN KhachHang kh ON h.MaKH = kh.MaKH
            {whereClause}
            ORDER BY {orderBy}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tu", tuNgay);
                    cmd.Parameters.AddWithValue("@den", denNgay);
                    if (cboNhanVienLap.SelectedIndex > 0)
                        cmd.Parameters.AddWithValue("@manv", ((NhanVienItem)cboNhanVienLap.SelectedItem).MaNV);
                    if (cboBanSo.SelectedIndex > 0)
                        cmd.Parameters.AddWithValue("@maban", ((BanItem)cboBanSo.SelectedItem).MaBan);
                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int maHD = rd.GetInt32(0);
                            string ngayLap = rd.GetDateTime(1).ToString("dd/MM/yyyy HH:mm");
                            string tenBan = rd.GetString(2);
                            string loaiBan = rd.GetString(3);
                            string tenNV = rd.GetString(4);
                            object tenKHObj = rd.GetValue(5);
                            string tenKH = tenKHObj == DBNull.Value ? "Khách vãng lai" : tenKHObj.ToString();
                            decimal tongTien = Convert.ToDecimal(rd.GetValue(6));
                            object khuyenMaiObj = rd.GetValue(7);
                            string khuyenMaiStr = khuyenMaiObj == DBNull.Value ? "" : Convert.ToDecimal(khuyenMaiObj).ToString("N0") + "%";
                            string trangThaiTT = rd.GetString(8);
                            object hinhThucObj = rd.GetValue(9);
                            string hinhThuc = hinhThucObj == DBNull.Value ? "Tiền mặt" : hinhThucObj.ToString();

                            int idx = dgvHoaDon.Rows.Add(
                                maHD,
                                ngayLap,
                                tenBan,
                                loaiBan,
                                tenNV,
                                tenKH,
                                tongTien.ToString("N0") + " VND",
                                khuyenMaiStr,
                                trangThaiTT,
                                hinhThuc
                            );

                            var row = dgvHoaDon.Rows[idx];
                            row.DefaultCellStyle.BackColor = trangThaiTT == "Chưa thanh toán"
                                ? Color.LightPink : Color.LightGreen;
                        }
                    }
                }
            }
        }

        // EVENT load CTHD (khi click vào dòng dgvHoaDon)
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvHoaDon.Rows[e.RowIndex];
            int maHD = Convert.ToInt32(row.Cells[0].Value);
            LoadChiTietHoaDon(maHD);
            LoadThongTinKhachHang(maHD);
            btnXacNhanThanhToan.Enabled = (row.Cells[8].Value?.ToString() == "Chưa thanh toán");
        }

        private void LoadChiTietHoaDon(int maHD)
        {
            dgvHoaDonChiTiet.Rows.Clear();
            dgvHoaDonChiTiet.Columns.Clear();
            dgvHoaDonChiTiet.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "TenMon", HeaderText = "Tên món" },
                new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "Số lượng" },
                new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn giá" },
                new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành tiền" }
            });

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT m.TenMon, c.SoLuong, c.DonGia, c.SoLuong * c.DonGia
                    FROM HoaDonChiTiet c
                    JOIN MonAn m ON c.MaMon = m.MaMon
                    WHERE c.MaHoaDon = @hd", conn);
                cmd.Parameters.AddWithValue("@hd", maHD);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    decimal tong = 0;
                    while (rd.Read())
                    {
                        decimal gia = Convert.ToDecimal(rd.GetValue(2));
                        decimal tt = Convert.ToDecimal(rd.GetValue(3));
                        tong += tt;
                        dgvHoaDonChiTiet.Rows.Add(
                            rd.GetString(0),
                            rd.GetInt32(1),
                            gia.ToString("N0") + " VND",
                            tt.ToString("N0") + " VND");
                    }
                    lblTongTien.Text = tong.ToString("N0") + " VND";
                }
            }
        }

        private void LoadThongTinKhachHang(int maHD)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                kh.TenKH, 
                kh.DienThoai, 
                kh.LoaiKH, 
                h.HinhThucThanhToan, 
                h.KhuyenMai, 
                h.TrangThaiThanhToan
            FROM HoaDon h
            LEFT JOIN KhachHang kh ON h.MaKH = kh.MaKH
            WHERE h.MaHoaDon = @hd", conn);
                cmd.Parameters.AddWithValue("@hd", maHD);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        txtTenKH.Text = rd.IsDBNull(0) ? "" : rd.GetString(0);
                        txtDienThoaiKH.Text = rd.IsDBNull(1) ? "" : rd.GetString(1);
                        string loai = rd.IsDBNull(2) ? "Khách thường" : rd.GetString(2);
                        cboLoaiKH.SelectedItem = loai;
                        string ht = rd.IsDBNull(3) ? "Tiền mặt" : rd.GetString(3);
                        cboHinhThucThanhToan.SelectedItem = ht;

                        object kmObj = rd.GetValue(4);
                        string trangThaiTT = rd.GetString(5);

                        if (kmObj != DBNull.Value)
                        {
                            decimal km = Convert.ToDecimal(kmObj);
                            txtKhuyenMai.Text = km.ToString("N0");
                        }
                        else
                        {
                            txtKhuyenMai.Text = "";
                        }

                        // bật tắt cboKhuyenMai nếu là KH VIP chưa thanh toán
                        bool isVIP = (loai == "Khách VIP");
                        bool isChuaThanhToan = (trangThaiTT == "Chưa thanh toán");

                        txtKhuyenMai.Enabled = isVIP && isChuaThanhToan;
                    }
                    else
                    {
                        ResetThongTinKH();
                    }
                }
            }
        }

        private void ResetThongTinKH()
        {
            txtTenKH.Text = "";
            txtDienThoaiKH.Text = "";
            cboLoaiKH.SelectedIndex = 0;
            cboHinhThucThanhToan.SelectedIndex = 0;
            txtKhuyenMai.Text = "";
            txtKhuyenMai.Enabled = false;
        }

        private void ResetForm()
        {
            ResetThongTinKH();
            lblTongTien.Text = "0 VND";
            dgvHoaDonChiTiet.Rows.Clear();
            btnXacNhanThanhToan.Enabled = false;
        }

        // event loai kh
        private void cboLoaiKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKhuyenMai.Enabled = (cboLoaiKH.SelectedItem?.ToString() == "Khách VIP");
            if (!txtKhuyenMai.Enabled) txtKhuyenMai.Text = "";
        }

        //event km
        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            
        }

        // event btnXacNhanThanhToan_Click
        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;
            int maHD = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells[0].Value);
            if (dgvHoaDon.CurrentRow.Cells[8].Value?.ToString() != "Chưa thanh toán") return; // ← index 8 là TrangThai

            if (MessageBox.Show("Xác nhận thanh toán hóa đơn này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string tenKH = string.IsNullOrWhiteSpace(txtTenKH.Text) ? "Khách vãng lai" : txtTenKH.Text.Trim();
            string sdt = string.IsNullOrWhiteSpace(txtDienThoaiKH.Text) ? null : txtDienThoaiKH.Text.Trim();
            string loaiKH = cboLoaiKH.SelectedItem?.ToString() ?? "Khách thường";
            string hinhThuc = cboHinhThucThanhToan.SelectedItem?.ToString() ?? "Tiền mặt";

            // check khuyến mãi hợp lệ hay k
            decimal khuyenMai = 0;
            if (txtKhuyenMai.Enabled && decimal.TryParse(txtKhuyenMai.Text, out decimal km))
            {
                if (km < 0 || km > 100)
                {
                    MessageBox.Show("Khuyến mãi phải từ 0% đến 100%!");
                    return;
                }
                khuyenMai = km;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    int? maKH = null;
                    if (tenKH != "Khách vãng lai" || sdt != null)
                    {
                        SqlCommand cmdCheck = new SqlCommand(@"
                    SELECT MaKH FROM KhachHang 
                    WHERE TenKH = @ten AND (@sdt IS NULL OR DienThoai = @sdt)", conn, trans);
                        cmdCheck.Parameters.AddWithValue("@ten", tenKH);
                        cmdCheck.Parameters.AddWithValue("@sdt", sdt ?? (object)DBNull.Value);
                        object res = cmdCheck.ExecuteScalar();
                        if (res != null)
                        {
                            maKH = Convert.ToInt32(res);
                        }
                        else
                        {
                            SqlCommand cmdIns = new SqlCommand(@"
                        INSERT INTO KhachHang (TenKH, DienThoai, LoaiKH)
                        VALUES (@ten, @sdt, @loai)
                        SELECT SCOPE_IDENTITY()", conn, trans);
                            cmdIns.Parameters.AddWithValue("@ten", tenKH);
                            cmdIns.Parameters.AddWithValue("@sdt", sdt ?? (object)DBNull.Value);
                            cmdIns.Parameters.AddWithValue("@loai", loaiKH);
                            maKH = Convert.ToInt32(cmdIns.ExecuteScalar());
                        }
                    }

                    // update vào hóa đơn
                    SqlCommand cmdUpd = new SqlCommand(@"
                UPDATE HoaDon
                SET 
                    MaKH = @maKH,
                    TrangThaiThanhToan = N'Đã thanh toán',
                    ThoiDiemThanhToan = GETDATE(),
                    HinhThucThanhToan = @ht,
                    KhuyenMai = @km,
                    TongTien = CASE 
                        WHEN @km > 0 THEN TongTien * (1 - @km / 100.0)
                        ELSE TongTien 
                    END
                WHERE MaHoaDon = @hd", conn, trans);

                    cmdUpd.Parameters.AddWithValue("@maKH", maKH ?? (object)DBNull.Value); // 👈 SỬA LỖI TẠI ĐÂY
                    cmdUpd.Parameters.AddWithValue("@ht", hinhThuc);
                    cmdUpd.Parameters.AddWithValue("@km", khuyenMai);
                    cmdUpd.Parameters.AddWithValue("@hd", maHD);
                    cmdUpd.ExecuteNonQuery();

                    // update bàn
                    SqlCommand cmdBan = new SqlCommand(@"
                UPDATE Ban SET TrangThai = N'Trống'
                WHERE MaBan = (SELECT MaBan FROM HoaDon WHERE MaHoaDon = @hd)", conn, trans);
                    cmdBan.Parameters.AddWithValue("@hd", maHD);
                    cmdBan.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show(" Thanh toán thành công!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return;
                }
            }

            LoadDanhSachHoaDon();
            ResetForm();
        }

        // event thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // class phụ 
        private class NhanVienItem
        {
            public int MaNV { get; set; }
            public string TenNV { get; set; }
            public override string ToString() => TenNV;
        }

        private class BanItem
        {
            public int MaBan { get; set; }
            public string TenBan { get; set; }
            public override string ToString() => TenBan;
        }

        private void cboHinhThucThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in!");
                return;
            }

            int maHD = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells[0].Value);
            new FormInHoaDon(maHD).ShowDialog();
        }


        private void txtKhuyenMai_Leave(object sender, EventArgs e)
        {
            LuuKhuyenMai();
        }

        private void txtKhuyenMai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LuuKhuyenMai();
                e.SuppressKeyPress = true;
            }
        }

        private void LuuKhuyenMai()
        {
            if (dgvHoaDon.CurrentRow == null) return;
            int maHD = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells[0].Value);

            decimal km = 0;
            if (decimal.TryParse(txtKhuyenMai.Text, out decimal value) && value >= 0 && value <= 100)
            {
                km = value;
            }
            else
            {
                MessageBox.Show("Khuyến mãi phải từ 0% đến 100%!");
                txtKhuyenMai.Text = "0";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE HoaDon 
                SET KhuyenMai = @km 
                WHERE MaHoaDon = @hd", conn);
                    cmd.Parameters.AddWithValue("@km", km);
                    cmd.Parameters.AddWithValue("@hd", maHD);
                    cmd.ExecuteNonQuery();

                    LoadDanhSachHoaDon();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu khuyến mãi: " + ex.Message);
                }
            }
        }
    }
}