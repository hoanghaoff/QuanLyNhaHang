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

namespace QuanLyNhaHang
{
    public partial class FormQuanLyDanhMucMonAn : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        DataTable dtDanhMuc = new DataTable();

        public FormQuanLyDanhMucMonAn()
        {
            InitializeComponent();
            Load += FormQuanLyDanhMucMonAn_Load;
        }

        // event lúc load form
        private void FormQuanLyDanhMucMonAn_Load(object sender, EventArgs e)
        {
            LoadComboSapXep();
            LoadDataRealtime();
        }

        // event load cboSapXep
        private void LoadComboSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Tên danh mục A-Z",
                "Tên danh mục Z-A"
            });
            cboSapXep.SelectedIndex = 0;
        }

        // load data realtime
        private void LoadDataRealtime(string where = "", string orderBy = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc";

                if (!string.IsNullOrEmpty(where))
                    sql += " WHERE " + where;

                if (!string.IsNullOrEmpty(orderBy))
                    sql += " ORDER BY " + orderBy;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                dtDanhMuc.Clear();
                da.Fill(dtDanhMuc);

                dgvDanhMuc.Rows.Clear();
                foreach (DataRow r in dtDanhMuc.Rows)
                {
                    dgvDanhMuc.Rows.Add(
                        r[0], //mã danh mục món
                        r[1]  //tên danh mục món
                    );
                }
            }
        }

        // cell click dgvDanhMuc
        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvDanhMuc.Rows[e.RowIndex];
            txtMaDanhMuc.Text = r.Cells[0].Value.ToString();
            txtTenDanhMuc.Text = r.Cells[1].Value.ToString();
        }

        // check nhập
        private bool KiemTraNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên danh mục!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return false;
            }
            return true;
        }

        // refersh sau khi nhập thêm OK
        private void LamMoiNhap()
        {
            txtMaDanhMuc.Clear();
            txtTenDanhMuc.Clear();
        }

        //event thêm danh mục
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO DanhMuc (TenDanhMuc) VALUES (@ten)", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenDanhMuc.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(" Thêm danh mục thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        //event btn sửa danh mục
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaDanhMuc.Text == "")
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE DanhMuc SET TenDanhMuc=@ten WHERE MaDanhMuc=@id", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenDanhMuc.Text.Trim());
                cmd.Parameters.AddWithValue("@id", txtMaDanhMuc.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật danh mục thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event btn xóa danh mục
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaDanhMuc.Text == "")
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM DanhMuc WHERE MaDanhMuc=@id", conn);
                cmd.Parameters.AddWithValue("@id", txtMaDanhMuc.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("🗑️ Xóa danh mục thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event txtTimKiem text changed realtime
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();
            LoadDataRealtime($"TenDanhMuc LIKE N'%{key}%'");
        }

        // event cboSapXep index changed
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string order = cboSapXep.Text == "Tên danh mục Z-A"
                ? "TenDanhMuc DESC"
                : "TenDanhMuc ASC";

            LoadDataRealtime("", order);
        }

        // event btnthoat
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
