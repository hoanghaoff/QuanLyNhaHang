using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormQuanLyKhachHang : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        DataTable dtKhachHang = new DataTable();

        public FormQuanLyKhachHang()
        {
            InitializeComponent();
            Load += FormQuanLyKhachHang_Load;
        }

        // load form
        private void FormQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadLoaiKH();
            LoadComboSapXep();
            LoadDataRealtime();
        }

        private void LoadLoaiKH()
        {
            cboLoaiKH.Items.Clear();
            cboLoaiKH.Items.AddRange(new string[]
            {
                "Khách VIP", // index 0
                "Khách thường" // index 1
            });
            cboLoaiKH.SelectedIndex = 1;
        }


        private void LoadComboSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Tên KH A-Z",
                "Tên KH Z-A",
                "SĐT A-Z",
                "SĐT Z-A",
                "Loại KH A-Z",
                "Loại KH Z-A"
            });
            cboSapXep.SelectedIndex = 0;
        }


        private void LoadDataRealtime(string where = "", string orderBy = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT MaKH, TenKH, DienThoai, LoaiKH FROM KhachHang";

                if (!string.IsNullOrEmpty(where))
                    sql += " WHERE " + where;

                if (!string.IsNullOrEmpty(orderBy))
                    sql += " ORDER BY " + orderBy;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                dtKhachHang.Clear();
                da.Fill(dtKhachHang);

                dgvKhachHang.Rows.Clear();
                foreach (DataRow r in dtKhachHang.Rows)
                {
                    dgvKhachHang.Rows.Add(
                        r[0], //MaKH
                        r[1], //TenKH
                        r[2], //DienThoai
                        r[3]   //LoaiKH
                    );
                }
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvKhachHang.Rows[e.RowIndex];
            txtMaKH.Text = r.Cells[0].Value.ToString();
            txtTenKH.Text = r.Cells[1].Value.ToString();
            txtDienThoai.Text = r.Cells[2].Value.ToString();
            cboLoaiKH.Text = r.Cells[3].Value.ToString();
        }

        //check nhập
        private bool KiemTraNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }

            if (cboLoaiKH.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Loại khách hàng!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiKH.Focus();
                return false;
            }

            return true;
        }

        // refersh sau khi thêm sửa xóa
        private void LamMoiNhap()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDienThoai.Clear();
            cboLoaiKH.SelectedIndex = 1;
        }

        // event btnThem_Click
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO KhachHang (TenKH, DienThoai, LoaiKH) VALUES (@ten,@dt,@loai)", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenKH.Text.Trim());
                cmd.Parameters.AddWithValue("@dt", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@loai", cboLoaiKH.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Thêm khách hàng thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event btnSua_Click
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE KhachHang
                      SET TenKH=@ten, DienThoai=@dt, LoaiKH=@loai
                      WHERE MaKH=@id", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenKH.Text.Trim());
                cmd.Parameters.AddWithValue("@dt", txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@loai", cboLoaiKH.Text);
                cmd.Parameters.AddWithValue("@id", txtMaKH.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Cập nhật khách hàng thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event btnXoa_Click
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM KhachHang WHERE MaKH=@id", conn);
                cmd.Parameters.AddWithValue("@id", txtMaKH.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Xóa khách hàng thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event txtTimKiem_TextChanged
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            string where =
                $"TenKH LIKE N'%{key}%' OR " +
                $"DienThoai LIKE N'%{key}%' OR " +
                $"LoaiKH LIKE N'%{key}%'";

            LoadDataRealtime(where);
        }

        // event cboSapXep_SelectedIndexChanged
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string order = "TenKH ASC";

            switch (cboSapXep.Text)
            {
                case "Tên KH Z-A":
                    order = "TenKH DESC";
                    break;
                case "SĐT A-Z":
                    order = "DienThoai ASC";
                    break;
                case "SĐT Z-A":
                    order = "DienThoai DESC";
                    break;
                case "Loại KH A-Z":
                    order = "LoaiKH ASC";
                    break;
                case "Loại KH Z-A":
                    order = "LoaiKH DESC";
                    break;
            }

            LoadDataRealtime("", order);
        }

        // event btnThoat_Click
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
