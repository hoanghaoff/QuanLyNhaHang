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
    public partial class FormQuanLyBan : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        DataTable dtBan = new DataTable();

        public FormQuanLyBan()
        {
            InitializeComponent();
            Load += FormQuanLyBan_Load;
        }

        // event FormQuanLyBan_Load
        private void FormQuanLyBan_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDataRealtime();
        }

        // load cboLoaiBan, cboTrangThai, cboSapXep
        private void LoadComboBox()
        {
            cboLoaiBan.Items.Clear();
            cboLoaiBan.Items.AddRange(new string[]
            {
                "Bàn thường",
                "Bàn VIP",
                "Bàn ngoài trời"
            });

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new string[]
            {
                "Trống",
                "Có khách",
                "Đã đặt",
                "Bảo trì"
            });

            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Loại bàn A-Z",
                "Loại bàn Z-A",
                "Trạng thái A-Z",
                "Trạng thái Z-A"
            });

            cboLoaiBan.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            cboSapXep.SelectedIndex = 0;
        }

        //  load data realtime
        private void LoadDataRealtime(string where = "", string orderBy = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT MaBan, TenBan, LoaiBan, TrangThai FROM Ban";

                if (!string.IsNullOrEmpty(where))
                    sql += " WHERE " + where;

                if (!string.IsNullOrEmpty(orderBy))
                    sql += " ORDER BY " + orderBy;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                dtBan.Clear();
                da.Fill(dtBan);

                dgvBan.Rows.Clear();
                foreach (DataRow r in dtBan.Rows)
                {
                    dgvBan.Rows.Add(
                        r[0], // id bàn
                        r[1], // tên bàn
                        r[2], // loại bàn
                        r[3]  // trạng thái
                    );
                }
            }
        }

        // cell click vào dgvBan
        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvBan.Rows[e.RowIndex];
            txtMaBan.Text = r.Cells[0].Value.ToString();
            txtTenBan.Text = r.Cells[1].Value.ToString();
            cboLoaiBan.Text = r.Cells[2].Value.ToString();
            cboTrangThai.Text = r.Cells[3].Value.ToString();
        }

        // check nhập
        private bool KiemTraNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTenBan.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên bàn!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBan.Focus();
                return false;
            }

            if (cboLoaiBan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Loại bàn!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiBan.Focus();
                return false;
            }

            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Trạng thái!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThai.Focus();
                return false;
            }

            return true;
        }

        // Refersh sau khi nhập OK
        private void LamMoiNhap()
        {
            txtMaBan.Clear();
            txtTenBan.Clear();
            cboLoaiBan.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        //event btn them nv
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Ban (TenBan, LoaiBan, TrangThai) VALUES (@ten,@loai,@tt)", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenBan.Text.Trim());
                cmd.Parameters.AddWithValue("@loai", cboLoaiBan.Text);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm bàn thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event btn sua nv
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaBan.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bàn cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Ban SET TenBan=@ten, LoaiBan=@loai, TrangThai=@tt WHERE MaBan=@id", conn);
                cmd.Parameters.AddWithValue("@ten", txtTenBan.Text.Trim());
                cmd.Parameters.AddWithValue("@loai", cboLoaiBan.Text);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.Parameters.AddWithValue("@id", txtMaBan.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật bàn thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event btn xoa nv
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaBan.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bàn này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Ban WHERE MaBan=@id", conn);
                cmd.Parameters.AddWithValue("@id", txtMaBan.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa bàn thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadDataRealtime();
            LamMoiNhap();
        }

        // event txtTimKiem_TextChanged (khi text thay đổi)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            string where =
                $"TenBan LIKE N'%{key}%' OR " +
                $"LoaiBan LIKE N'%{key}%' OR " +
                $"TrangThai LIKE N'%{key}%'";

            LoadDataRealtime(where);
        }

        // event cboSapXep_SelectedIndexChanged
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string order = "LoaiBan ASC";

            switch (cboSapXep.Text)
            {
                case "Loại bàn Z-A":
                    order = "LoaiBan DESC";
                    break;
                case "Trạng thái A-Z":
                    order = "TrangThai ASC";
                    break;
                case "Trạng thái Z-A":
                    order = "TrangThai DESC";
                    break;
            }

            LoadDataRealtime("", order);
        }

        //event btnThoat_Click
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
