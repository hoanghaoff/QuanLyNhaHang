using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class FormQuanLyNhanVien : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        DataTable dtNV = new DataTable();

        public FormQuanLyNhanVien()
        {
            InitializeComponent();
            Load += FormQuanLyNhanVien_Load;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }

        // load form
        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadChucVu();
            LoadTrangThai();
            LoadSapXep();
            LoadDataRealtime();
        }

        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT Id, Name FROM Roles", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboChucVu.DataSource = dt;
                cboChucVu.DisplayMember = "Name";
                cboChucVu.ValueMember = "Id";
                cboChucVu.SelectedIndex = -1;
            }
        }

        // load trạng thái làm việc
        private void LoadTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new string[]
            {
                "Còn làm",
                "Tạm nghỉ",
                "Thôi việc"
            });
            cboTrangThai.SelectedIndex = 0;
        }

    
        private void LoadSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Tên NV A-Z",
                "Tên NV Z-A",
                "Ngày vào làm tăng",
                "Ngày vào làm giảm"
            });
            cboSapXep.SelectedIndex = 0;
        }

        //load data nv
        private void LoadDataRealtime(string where = "", string order = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql =
                    @"SELECT 
                    nv.MaNV,
                    nv.TenNV,
                    ISNULL(r.Name, N'Chưa phân quyền') AS ChucVu,
                    nv.GioiTinh,
                    nv.NgaySinh,
                    nv.DienThoai,
                    nv.DiaChi,
                    nv.NgayVaoLam,
                    nv.TrangThai
                FROM NhanVien nv
                LEFT JOIN Users u ON nv.UserId = u.Id
                LEFT JOIN UserRoles ur ON u.Id = ur.UserId
                LEFT JOIN Roles r ON ur.RoleId = r.Id";

                if (!string.IsNullOrEmpty(where))
                    sql += " WHERE " + where;

                if (!string.IsNullOrEmpty(order))
                    sql += " ORDER BY " + order;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                dtNV.Clear();
                da.Fill(dtNV);

                dgvNhanVien.Rows.Clear();
                foreach (DataRow r in dtNV.Rows)
                {
                    dgvNhanVien.Rows.Add(
                        r[0], r[1], r[2], r[3], r[4],
                        r[5], r[6], r[7], r[8]
                    );
                }
            }
        }

        // cell click  dgvNhanVien
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var r = dgvNhanVien.Rows[e.RowIndex];
            txtMaNV.Text = r.Cells[0].Value.ToString();
            txtTenNV.Text = r.Cells[1].Value.ToString();
            cboChucVu.Text = r.Cells[2].Value.ToString();

            if (r.Cells[3].Value.ToString() == "Nam") rbNam.Checked = true;
            else rbNu.Checked = true;

            dtpNgaySinh.Value = Convert.ToDateTime(r.Cells[4].Value);
            txtDienThoai.Text = r.Cells[5].Value.ToString();
            txtDiaChi.Text = r.Cells[6].Value.ToString();
            dtpNgayVaoLam.Value = Convert.ToDateTime(r.Cells[7].Value);
            cboTrangThai.Text = r.Cells[8].Value.ToString();
        }

        //check nhập
        private bool KiemTraNhap()
        {
            // họ tên
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên nhân viên!");
                txtTenNV.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtTenNV.Text.Trim(), @"^[\p{L}\s]{3,}$"))
            {
                MessageBox.Show("Họ tên phải ≥ 3 ký tự, không chứa số hoặc ký tự đặc biệt!");
                txtTenNV.Focus();
                return false;
            }

            // chức vụ
            if (cboChucVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Chức vụ!");
                cboChucVu.Focus();
                return false;
            }

            //giới tính
            if (!rbNam.Checked && !rbNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn Giới tính!");
                return false;
            }

            //ngày sinh
            if (dtpNgaySinh.Value.Date >= DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!");
                dtpNgaySinh.Focus();
                return false;
            }

            //số điện thoại
            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại!");
                txtDienThoai.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtDienThoai.Text.Trim(), @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải đúng 10 chữ số!");
                txtDienThoai.Focus();
                return false;
            }

            //Địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ!");
                txtDiaChi.Focus();
                return false;
            }

            // ngày vào làm
            if (dtpNgayVaoLam.Value.Date < dtpNgaySinh.Value.Date)
            {
                MessageBox.Show("Ngày vào làm phải lớn hơn Ngày sinh!");
                dtpNgayVaoLam.Focus();
                return false;
            }

            // trạng thái
            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Trạng thái!");
                cboTrangThai.Focus();
                return false;
            }

            return true;
        }

        // event btnThem_Click
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                @"INSERT INTO NhanVien
                  (UserId, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, NgayVaoLam, TrangThai)
                  VALUES (@cv,@ten,@gt,@ns,@dt,@dc,@nvl,@tt)", conn);

                cmd.Parameters.AddWithValue("@cv", cboChucVu.SelectedValue);
                cmd.Parameters.AddWithValue("@ten", txtTenNV.Text.Trim());
                cmd.Parameters.AddWithValue("@gt", rbNam.Checked ? "Nam" : "Nữ");
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@dt", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@nvl", dtpNgayVaoLam.Value);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm nhân viên thành công!");
            LoadDataRealtime();
        }

        //event btnSua_Click
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "") return;

            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                @"UPDATE NhanVien SET
                    UserId=@cv, TenNV=@ten, GioiTinh=@gt,
                    NgaySinh=@ns, DienThoai=@dt, DiaChi=@dc,
                    NgayVaoLam=@nvl, TrangThai=@tt
                  WHERE MaNV=@id", conn);

                cmd.Parameters.AddWithValue("@cv", cboChucVu.SelectedValue);
                cmd.Parameters.AddWithValue("@ten", txtTenNV.Text.Trim());
                cmd.Parameters.AddWithValue("@gt", rbNam.Checked ? "Nam" : "Nữ");
                cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@dt", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@nvl", dtpNgayVaoLam.Value);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.Parameters.AddWithValue("@id", txtMaNV.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Cập nhật nhân viên thành công!");
            LoadDataRealtime();
        }

        // event btnXoa_Click
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "") return;

            if (MessageBox.Show("Xóa nhân viên này?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM NhanVien WHERE MaNV=@id", conn);
                cmd.Parameters.AddWithValue("@id", txtMaNV.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Đã xóa nhân viên!");
            LoadDataRealtime();
        }

        // event cboSapXep_SelectedIndexChanged
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string order = "TenNV ASC";

            if (cboSapXep.Text.Contains("Z-A"))
                order = "TenNV DESC";
            if (cboSapXep.Text.Contains("tăng"))
                order = "NgayVaoLam ASC";
            if (cboSapXep.Text.Contains("giảm"))
                order = "NgayVaoLam DESC";

            LoadDataRealtime("", order);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            string where =
                $"CAST(nv.MaNV AS NVARCHAR) LIKE N'%{key}%' OR " +
                $"nv.TenNV LIKE N'%{key}%' OR " +
                $"r.Name LIKE N'%{key}%' OR " +
                $"nv.GioiTinh LIKE N'%{key}%' OR " +
                $"CONVERT(NVARCHAR, nv.NgaySinh, 103) LIKE N'%{key}%' OR " +
                $"nv.DienThoai LIKE N'%{key}%' OR " +
                $"nv.DiaChi LIKE N'%{key}%' OR " +
                $"CONVERT(NVARCHAR, nv.NgayVaoLam, 103) LIKE N'%{key}%' OR " +
                $"nv.TrangThai LIKE N'%{key}%'";

            LoadDataRealtime(where);
        }

        // event btnThoat_Click
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
