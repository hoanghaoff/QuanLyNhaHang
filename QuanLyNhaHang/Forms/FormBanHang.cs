using QuanLyNhaHang.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormBanHang : Form
    {
        private readonly string connStr =
            @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        private int maHoaDonDangMo = 0;

        public FormBanHang()
        {
            InitializeComponent();

            Load += FormBanHang_Load;
            txtTimKiem.TextChanged += (s, e) => LoadDanhSachMon();
            cboSapXep.SelectedIndexChanged += (s, e) => LoadDanhSachMon();
            cboMaBan.SelectedIndexChanged += cboMaBan_SelectedIndexChanged;
            btnXoaMon.Click += btnXoaMon_Click;
            dgvHoaDon.CellClick += (s, e) => btnXoaMon.Enabled = true;

            cboLocDanhMuc.SelectedIndexChanged += (s, e) => LoadDanhSachMon();
        }

        // lay manv tu user đang đăng nhập
        private int GetCurrentMaNV()
        {
            if (SessionManager.CurrentUser == null)
                return 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT nv.MaNV
                    FROM NhanVien nv
                    INNER JOIN Users u ON nv.UserId = u.Id
                    WHERE u.Id = @uid AND nv.TrangThai = N'Còn làm'
                ", conn);

                cmd.Parameters.AddWithValue("@uid", SessionManager.CurrentUser.Id);

                object rs = cmd.ExecuteScalar();
                return rs == null ? 0 : Convert.ToInt32(rs);
            }
        }

        private string GetCurrentTenNV()
        {
            if (SessionManager.CurrentUser == null)
                return "";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT TenNV FROM NhanVien WHERE UserId = @uid
                ", conn);

                cmd.Parameters.AddWithValue("@uid", SessionManager.CurrentUser.Id);

                object rs = cmd.ExecuteScalar();
                if (rs != null)
                    return rs.ToString();
            }

            return SessionManager.CurrentUser.FullName ?? SessionManager.CurrentUser.Username;
        }


        private void FormBanHang_Load(object sender, EventArgs e)
        {
            maHoaDonDangMo = 0;

            lblTenCuaNV.Text = GetCurrentTenNV();
            lblTenCuaNV.AutoSize = true;

            LoadDanhMuc();
            InitSapXep();
            LoadBan();
            LoadDanhSachMon();
            CapNhatTongTien();

            btnXoaMon.Enabled = false;
        }

        private void LoadDanhMuc()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT MaDanhMuc, TenDanhMuc
            FROM DanhMuc", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                // thêm "tất cả"
                DataRow rowAll = dt.NewRow();
                rowAll["MaDanhMuc"] = 0;
                rowAll["TenDanhMuc"] = "Tất cả";
                dt.Rows.InsertAt(rowAll, 0);

                cboLocDanhMuc.DisplayMember = "TenDanhMuc";
                cboLocDanhMuc.ValueMember = "MaDanhMuc";
                cboLocDanhMuc.DataSource = dt;
                cboLocDanhMuc.SelectedIndex = 0; // chọn tất cả mặc định
            }
        }
        // sap xep. cboSapXep
        private void InitSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Tên món A-Z",
                "Tên món Z-A",
                "Giá tăng dần",
                "Giá giảm dần"
            });
            cboSapXep.SelectedIndex = 0;
        }

        // load bàn
        private void LoadBan()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT MaBan,
                           TenBan + N' (' + ISNULL(TrangThai,N'Trống') + N')' AS TenBanHienThi
                    FROM Ban", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboMaBan.DisplayMember = "TenBanHienThi";
                cboMaBan.ValueMember = "MaBan";
                cboMaBan.DataSource = dt;
                cboMaBan.SelectedIndex = -1;
            }
        }

        // load món
        private void LoadDanhSachMon()
        {
            flpMonAn.Controls.Clear();

            string orderByClause;
            switch (cboSapXep.SelectedIndex)
            {
                case 1:
                    orderByClause = "TenMon DESC";
                    break;
                case 2:
                    orderByClause = "DonGia ASC";
                    break;
                case 3:
                    orderByClause = "DonGia DESC";
                    break;
                default:
                    orderByClause = "TenMon";
                    break;
            }

            int maDanhMuc = Convert.ToInt32(cboLocDanhMuc.SelectedValue);
            string whereClause = "";
            if (maDanhMuc != 0) // Không phải "Tất cả"
            {
                whereClause = " AND m.MaDanhMuc = @dm";
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $@"
            SELECT m.MaMon, m.TenMon, m.DonGia, m.HinhAnh
            FROM MonAn m
            WHERE m.TenMon LIKE @kw {whereClause}
            ORDER BY {orderByClause}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + txtTimKiem.Text.Trim() + "%");
                    if (maDanhMuc != 0)
                        cmd.Parameters.AddWithValue("@dm", maDanhMuc);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int maMon = rd.GetInt32(0);
                            string tenMon = rd.GetString(1);
                            decimal gia = rd.IsDBNull(2) ? 0m : Convert.ToDecimal(rd.GetValue(2));
                            object hinhAnhObj = rd.GetValue(3);
                            string hinhAnh = hinhAnhObj == DBNull.Value ? null : hinhAnhObj.ToString();

                            flpMonAn.Controls.Add(
                                TaoTheMon(maMon, tenMon, gia, hinhAnh));
                        }
                    }
                }
            }
        }

        // clone thẻ món
        private Panel TaoTheMon(int maMon, string tenMon, decimal gia, string hinhAnh)
        {
            Panel pnl = new Panel
            {
                Size = pnlMon.Size,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(6),
                Tag = maMon
            };

            foreach (Control c in pnlMon.Controls)
            {
                Control clone = (Control)Activator.CreateInstance(c.GetType());
                clone.Name = c.Name;
                clone.Size = c.Size;
                clone.Location = c.Location;
                clone.Font = c.Font;
                clone.Visible = true;

                if (clone.Name == "lblTenMon")
                {
                    clone.Text = tenMon;
                    clone.AutoSize = false;
                    clone.Width = pnl.Width - 1;
                    clone.Height = 40;
                }

                if (clone.Name == "lblGiaTien")
                {
                    clone.Text = gia.ToString("N0") + " VND";
                    clone.AutoSize = true;
                    clone.ForeColor = Color.Black;
                }

                if (clone.Name == "lblSL")
                {
                    clone.Text = "SL:";
                    clone.AutoSize = true;
                    clone.Visible = true;
                }
                if (clone is NumericUpDown nud)
                {
                    nud.Minimum = 1;
                    nud.Value = 1;
                }

                if (clone is PictureBox pb)
                {
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    if (!string.IsNullOrEmpty(hinhAnh) && File.Exists(hinhAnh))
                        pb.Image = Image.FromFile(hinhAnh);
                }

                if (clone is Button btn && btn.Name == "btnDat")
                {
                    btn.Text = "ĐẶT";
                    btn.Click += (s, e) =>
                    {
                        if (cboMaBan.SelectedIndex < 0)
                        {
                            MessageBox.Show("Vui lòng chọn bàn!");
                            return;
                        }

                        int sl = Convert.ToInt32(
                            pnl.Controls.OfType<NumericUpDown>().First().Value);

                        ThemMonVaoHoaDon(maMon, tenMon, gia, sl);
                    };
                }

                pnl.Controls.Add(clone);
            }

            return pnl;
        }

        // them mon vao hoa don
        private void ThemMonVaoHoaDon(int maMon, string ten, decimal gia, int sl)
        {
            foreach (DataGridViewRow r in dgvHoaDon.Rows)
            {
                if ((int)r.Tag == maMon)
                {
                    int slMoi = Convert.ToInt32(r.Cells[1].Value) + sl;
                    r.Cells[1].Value = slMoi;
                    r.Cells[3].Value = slMoi * gia;
                    CapNhatTongTien();
                    return;
                }
            }

            int i = dgvHoaDon.Rows.Add();
            dgvHoaDon.Rows[i].Cells[0].Value = ten;
            dgvHoaDon.Rows[i].Cells[1].Value = sl;
            dgvHoaDon.Rows[i].Cells[2].Value = gia;
            dgvHoaDon.Rows[i].Cells[3].Value = sl * gia;
            dgvHoaDon.Rows[i].Tag = maMon;

            CapNhatTongTien();
        }

        // event btnXoaMon
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
                dgvHoaDon.Rows.Remove(dgvHoaDon.CurrentRow);

            CapNhatTongTien();
            btnXoaMon.Enabled = false;
        }

        // update tong tien lblTongTien
        private void CapNhatTongTien()
        {
            decimal tong = dgvHoaDon.Rows
                .Cast<DataGridViewRow>()
                .Sum(r => Convert.ToDecimal(r.Cells[3].Value));

            lblTongTien.Text = tong.ToString("N0") + " VND";
        }

        // event btnLuuVaoHoaDon
        private void btnLuuVaoHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count == 0 || cboMaBan.SelectedIndex < 0)
            {
                MessageBox.Show("Chưa chọn bàn hoặc chưa có món!");
                return;
            }

            int maNV = GetCurrentMaNV();
            if (maNV == 0)
            {
                MessageBox.Show("Tài khoản chưa gán nhân viên!");
                return;
            }

            decimal tongTien = dgvHoaDon.Rows
                .Cast<DataGridViewRow>()
                .Sum(r => Convert.ToDecimal(r.Cells[3].Value));

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                int maBan = Convert.ToInt32(cboMaBan.SelectedValue);

                if (maHoaDonDangMo == 0)
                {
                    // tao hoa don moi neu maHD = 0
                    SqlCommand cmdHD = new SqlCommand(@"
                INSERT INTO HoaDon
                    (NgayLap, MaNV, MaBan, TongTien, TrangThaiThanhToan)
                OUTPUT INSERTED.MaHoaDon
                VALUES
                    (GETDATE(), @manv, @ban, @tong, N'Chưa thanh toán')
            ", conn);
                    cmdHD.Parameters.AddWithValue("@manv", maNV);
                    cmdHD.Parameters.AddWithValue("@ban", maBan);
                    cmdHD.Parameters.AddWithValue("@tong", tongTien);
                    maHoaDonDangMo = Convert.ToInt32(cmdHD.ExecuteScalar());

                    // cap nhat trang thai ban
                    SqlCommand cmdBan = new SqlCommand(
                        "UPDATE Ban SET TrangThai = N'Có khách' WHERE MaBan = @ban", conn);
                    cmdBan.Parameters.AddWithValue("@ban", maBan);
                    cmdBan.ExecuteNonQuery();
                }
                else
                {
                    // cap nhat hoa don neu maHD != 0
                    SqlCommand cmdUpdate = new SqlCommand(@"
                UPDATE HoaDon
                SET TongTien = @tong, MaNV = @manv
                WHERE MaHoaDon = @hd
            ", conn);
                    cmdUpdate.Parameters.AddWithValue("@tong", tongTien);
                    cmdUpdate.Parameters.AddWithValue("@manv", maNV);
                    cmdUpdate.Parameters.AddWithValue("@hd", maHoaDonDangMo);
                    cmdUpdate.ExecuteNonQuery();

                    // Xóa chi tiết cũ
                    SqlCommand cmdDel = new SqlCommand(
                        "DELETE FROM HoaDonChiTiet WHERE MaHoaDon = @hd", conn);
                    cmdDel.Parameters.AddWithValue("@hd", maHoaDonDangMo);
                    cmdDel.ExecuteNonQuery();
                }

                // Thêm chi tiết mới
                foreach (DataGridViewRow r in dgvHoaDon.Rows)
                {
                    SqlCommand cmdCT = new SqlCommand(@"
                INSERT INTO HoaDonChiTiet
                    (MaHoaDon, MaMon, SoLuong, DonGia)
                VALUES
                    (@hd, @mon, @sl, @gia)
            ", conn);
                    cmdCT.Parameters.AddWithValue("@hd", maHoaDonDangMo);
                    cmdCT.Parameters.AddWithValue("@mon", r.Tag);
                    cmdCT.Parameters.AddWithValue("@sl", r.Cells[1].Value);
                    cmdCT.Parameters.AddWithValue("@gia", r.Cells[2].Value);
                    cmdCT.ExecuteNonQuery();
                }
            }

            MessageBox.Show(" Lưu hóa đơn thành công!");
            
            CapNhatTongTien();
            LoadBan();
        }

        private void cboMaBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvHoaDon.Rows.Clear();
            CapNhatTongTien();
            maHoaDonDangMo = 0; // <-- THÊM DÒNG NÀY

            if (cboMaBan.SelectedIndex < 0) return;

            int maBan = Convert.ToInt32(cboMaBan.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmdHD = new SqlCommand(@"
            SELECT MaHoaDon
            FROM HoaDon
            WHERE MaBan = @ban AND TrangThaiThanhToan = N'Chưa thanh toán'
        ", conn);
                cmdHD.Parameters.AddWithValue("@ban", maBan);
                object hdResult = cmdHD.ExecuteScalar();

                if (hdResult == null) return;

                maHoaDonDangMo = Convert.ToInt32(hdResult);

                SqlCommand cmdCT = new SqlCommand(@"
            SELECT m.MaMon, m.TenMon, c.SoLuong, c.DonGia
            FROM HoaDonChiTiet c
            JOIN MonAn m ON m.MaMon = c.MaMon
            WHERE c.MaHoaDon = @hd
        ", conn);
                cmdCT.Parameters.AddWithValue("@hd", maHoaDonDangMo);

                using (SqlDataReader rd = cmdCT.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ThemMonVaoHoaDon(
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.IsDBNull(3) ? 0m : Convert.ToDecimal(rd.GetValue(3)),
                            rd.GetInt32(2)
                        );
                    }
                }
            }
        }
    }
}
