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
    public partial class FormQuanLyPhanQuyen : Form
    {
        private readonly string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        public FormQuanLyPhanQuyen()
        {
            InitializeComponent();
        }

        private void FormQuanLyPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadDanhSachQuyen();
            ResetForm();
        }

        private void LoadDanhSachQuyen()
        {
            dgvPhanQuyen.CellClick -= dgvPhanQuyen_CellClick;

            dgvPhanQuyen.Rows.Clear();
            dgvPhanQuyen.Columns.Clear();

            dgvPhanQuyen.Columns.Add("MaQuyen", "Mã quyền");
            dgvPhanQuyen.Columns.Add("TenQuyen", "Tên quyền");
            dgvPhanQuyen.Columns.Add("MoTa", "Mô tả quyền");
            dgvPhanQuyen.Columns.Add("CreatedAt", "Được tạo lúc");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT Id, Name, Description, CreatedAt
            FROM Roles
            ORDER BY Id", conn);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int id = rd.GetInt32(0);
                        string name = rd.GetString(1);
                        string desc = rd.IsDBNull(2) ? "" : rd.GetString(2);
                        string createdAt = rd.GetDateTime(3).ToString("dd/MM/yyyy HH:mm:ss");

                        dgvPhanQuyen.Rows.Add(id, name, desc, createdAt);
                    }
                }
            }

            dgvPhanQuyen.ClearSelection();
            dgvPhanQuyen.CellClick += dgvPhanQuyen_CellClick;
        }

        private void dgvPhanQuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPhanQuyen.Rows[e.RowIndex];
            txtRoleID.Text = row.Cells[0].Value?.ToString() ?? "";
            txtRoleName.Text = row.Cells[1].Value?.ToString() ?? "";
            txtRoleMoTa.Text = row.Cells[2].Value?.ToString() ?? "";
            txtRoleCreateAt.Text = row.Cells[3].Value?.ToString() ?? "";

            int maQuyen;
            bool isChu = int.TryParse(txtRoleID.Text, out maQuyen) && maQuyen == 1;

            // chỉ bật btn sửa xóa acc đó khi không phải quyền chủ
            btnSua.Enabled = !isChu;
            btnXoa.Enabled = !isChu;
        }

        private void ResetForm()
        {
            txtRoleID.Text = "";
            txtRoleName.Text = "";
            txtRoleMoTa.Text = "";
            txtRoleCreateAt.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            dgvPhanQuyen.ClearSelection();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên quyền!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Roles (Name, Description, CreatedAt)
                    VALUES (@name, @desc, GETDATE())
                ", conn);

                cmd.Parameters.AddWithValue("@name", txtRoleName.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtRoleMoTa.Text.Trim());

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm quyền thành công!");
            LoadDanhSachQuyen();
            ResetForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoleID.Text))
            {
                MessageBox.Show("Chưa chọn quyền để sửa!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên quyền!");
                return;
            }

            int maQuyen;
            if (!int.TryParse(txtRoleID.Text, out maQuyen))
            {
                MessageBox.Show("Mã quyền không hợp lệ!");
                return;
            }

            if (maQuyen == 1)
            {
                MessageBox.Show("Không thể sửa vai trò 'Chủ' vì đây là quyền hệ thống!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE Roles
                    SET Name = @name, Description = @desc
                    WHERE Id = @id
                ", conn);

                cmd.Parameters.AddWithValue("@name", txtRoleName.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtRoleMoTa.Text.Trim());
                cmd.Parameters.AddWithValue("@id", maQuyen);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Không tìm thấy quyền để sửa!");
                    return;
                }
            }

            MessageBox.Show("Sửa quyền thành công!");
            LoadDanhSachQuyen();
            ResetForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoleID.Text))
            {
                MessageBox.Show("Chưa chọn quyền để xóa!");
                return;
            }

            int maQuyen;
            if (!int.TryParse(txtRoleID.Text, out maQuyen))
            {
                MessageBox.Show("Mã quyền không hợp lệ!");
                return;
            }

            if (maQuyen == 1)
            {
                MessageBox.Show("Không thể xóa vai trò 'Chủ' vì đây là quyền hệ thống!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa quyền này? Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM Roles WHERE Id = @id
                ", conn);
                cmd.Parameters.AddWithValue("@id", maQuyen);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Không tìm thấy quyền để xóa!");
                    return;
                }
            }

            MessageBox.Show("Xóa quyền thành công!");
            LoadDanhSachQuyen();
            ResetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
