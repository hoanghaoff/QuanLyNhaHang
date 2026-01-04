using QuanLyNhaHang.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormQuanLyTaiKhoan : Form
    {
        private readonly string connStr =
            @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
            
        }



        // load form
        private void FormQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            InitSapXep();
            LoadDanhSachTaiKhoan();
            LoadDanhSachQuyen();
            ResetForm();
        }

        private void InitSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
        "Mặc định (ID tăng)",
        "Username A-Z",
        "Username Z-A",
        "Tên NV A-Z",
        "Tên NV Z-A",
        "Ngày tạo mới nhất-cũ nhất",
        "Ngày tạo cũ nhất-mới nhất"
            });
            cboSapXep.SelectedIndex = 0; // chọn mặc định id tăng
        }

        // load danh sách tài khoản vào dgvTaiKhoan
        private void LoadDanhSachTaiKhoan()
        {
            dgvTaiKhoan.Rows.Clear();
            dgvTaiKhoan.Columns.Clear();

            dgvTaiKhoan.Columns.Add("Id", "ID Tài khoản");
            dgvTaiKhoan.Columns.Add("Username", "Username");
            dgvTaiKhoan.Columns.Add("TenNV", "Tên Nhân viên");
            dgvTaiKhoan.Columns.Add("DienThoai", "Điện thoại");
            dgvTaiKhoan.Columns.Add("IsActive", "Đang hoạt động");
            dgvTaiKhoan.Columns.Add("IsDeleted", "Đã bị xóa?");
            dgvTaiKhoan.Columns.Add("CreatedAt", "Tạo vào lúc");

            string orderByClause = "u.Id";
            switch (cboSapXep.SelectedIndex)
            {
                case 1: orderByClause = "u.Username"; break;
                case 2: orderByClause = "u.Username DESC"; break;
                case 3: orderByClause = "nv.TenNV"; break;
                case 4: orderByClause = "nv.TenNV DESC"; break;
                case 5: orderByClause = "u.CreatedAt DESC"; break;
                case 6: orderByClause = "u.CreatedAt"; break;
                default: orderByClause = "u.Id"; break;
            }

            string keyword = txtTimKiem.Text.Trim();
            string whereClause = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                whereClause = @"
            WHERE u.Username LIKE @kw 
               OR nv.TenNV LIKE @kw";
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = $@"
                SELECT 
                        u.Id,
                        u.Username,
                        nv.TenNV,
                        nv.DienThoai,
                        u.IsActive,
                        u.IsDeleted,
                        u.CreatedAt
                FROM Users u
                LEFT JOIN NhanVien nv ON u.Id = nv.UserId
                {whereClause}
                ORDER BY {orderByClause}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int id = rd.GetInt32(0);
                            string username = rd.GetString(1);
                            string tenNV = rd.IsDBNull(2) ? "" : rd.GetString(2);
                            string dienThoai = rd.IsDBNull(3) ? "" : rd.GetString(3);
                            bool isActive = rd.GetBoolean(4);
                            bool isDeleted = rd.GetBoolean(5);
                            string createdAt = rd.GetDateTime(6).ToString("dd/MM/yyyy HH:mm:ss");

                            dgvTaiKhoan.Rows.Add(
                                id,
                                username,
                                tenNV,
                                dienThoai,
                                isActive ? "Có" : "Không",
                                isDeleted ? "Có" : "Không",
                                createdAt
                            );
                        }
                    }
                }
            }
        }

        //load danh sách quyền vào listbox
        private void LoadDanhSachQuyen()
        {
            lsbQuyenHan.Items.Clear();
            lsbQuyenHan.SelectionMode = SelectionMode.MultiSimple; // Cho phép chọn nhiều

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT Id, Name
                    FROM Roles
                    ORDER BY Id", conn);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var item = new RoleItem
                        {
                            Id = rd.GetInt32(0),
                            Name = rd.GetString(1)
                        };
                        lsbQuyenHan.Items.Add(item);
                    }
                }
            }
        }

        // event cell click dgvTaiKhoan
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTaiKhoan.Rows[e.RowIndex];
            int userId = Convert.ToInt32(row.Cells[0].Value);
            bool isDeleted = row.Cells[5].Value?.ToString() == "Có";

            // bind  hiện dữ liệu lên form
            txtIDTaiKhoan.Text = userId.ToString();
            txtUsernameTaiKhoan.Text = row.Cells[1].Value?.ToString() ?? "";
            txtFullNameNhanVien.Text = row.Cells[2].Value?.ToString() ?? "";
            txtDienThoaiNhanVien.Text = row.Cells[3].Value?.ToString() ?? "";
            txtTimeTaoTK.Text = row.Cells[6].Value?.ToString() ?? "";

            bool isActive = row.Cells[4].Value?.ToString() == "Có";
            rbHoatDong_Co.Checked = isActive;
            rbHoatDong_Khong.Checked = !isActive;

            LoadQuyenCuaUser(userId);

            txtMatKhauTaiKhoan.Clear();
            txtXacNhanMatKhauTaiKhoan.Clear();

            // vô hiệu hóa các btn nếu acc đã mark là deleted
            bool enableControls = !isDeleted;
            txtUsernameTaiKhoan.Enabled = enableControls;
            txtFullNameNhanVien.Enabled = enableControls;
            txtDienThoaiNhanVien.Enabled = enableControls;
            rbHoatDong_Co.Enabled = enableControls;
            rbHoatDong_Khong.Enabled = enableControls;
            lsbQuyenHan.Enabled = enableControls;

            btnSua.Enabled = enableControls;
            btnXoa.Enabled = enableControls;
        }

        // load user roles vào listbox
        private void LoadQuyenCuaUser(int userId)
        {
            // clear selection trước nếu có
            for (int i = 0; i < lsbQuyenHan.Items.Count; i++)
                lsbQuyenHan.SetSelected(i, false);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT RoleId
                    FROM UserRoles
                    WHERE UserId = @uid", conn);
                cmd.Parameters.AddWithValue("@uid", userId);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int roleId = rd.GetInt32(0);
                        for (int i = 0; i < lsbQuyenHan.Items.Count; i++)
                        {
                            if ((lsbQuyenHan.Items[i] as RoleItem)?.Id == roleId)
                                lsbQuyenHan.SetSelected(i, true);
                        }
                    }
                }
            }
        }

        //refersh form sau khi thêm sửa xóa
        private void ResetForm()
        {
            txtIDTaiKhoan.Text = "";
            txtUsernameTaiKhoan.Text = "";
            txtMatKhauTaiKhoan.Clear();
            txtXacNhanMatKhauTaiKhoan.Clear();
            txtFullNameNhanVien.Text = "";
            txtDienThoaiNhanVien.Text = "";
            txtTimeTaoTK.Text = "";
            rbHoatDong_Co.Checked = true;
            rbHoatDong_Khong.Checked = false;
            lsbQuyenHan.ClearSelected();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;


            txtUsernameTaiKhoan.Enabled = true;
            txtFullNameNhanVien.Enabled = true;
            txtDienThoaiNhanVien.Enabled = true;
            rbHoatDong_Co.Enabled = true;
            rbHoatDong_Khong.Enabled = true;
            lsbQuyenHan.Enabled = true;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            var formDangKy = new FormDangKy();

            // dk ok thì load lại ds
            formDangKy.Closed += (s, args) =>
            {
                LoadDanhSachTaiKhoan();
                ResetForm();
            };

            formDangKy.ShowDialog();
        }

        // event btnSua_Click
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDTaiKhoan.Text))
            {
                MessageBox.Show("Chưa chọn tài khoản để sửa!");
                return;
            }

            int userId;
            if (!int.TryParse(txtIDTaiKhoan.Text, out userId))
            {
                MessageBox.Show("ID tài khoản không hợp lệ!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsernameTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập Username!");
                return;
            }

            string passwordHash = null;
            if (!string.IsNullOrWhiteSpace(txtMatKhauTaiKhoan.Text) ||
                !string.IsNullOrWhiteSpace(txtXacNhanMatKhauTaiKhoan.Text))
            {
                if (txtMatKhauTaiKhoan.Text != txtXacNhanMatKhauTaiKhoan.Text)
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!");
                    return;
                }
               passwordHash = AuthService.ComputeHash(txtMatKhauTaiKhoan.Text);
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // update user
                    string updatePassClause = "";
                    if (passwordHash != null)
                        updatePassClause = ", PasswordHash = @pass";

                    SqlCommand cmdUser = new SqlCommand($@"
                        UPDATE Users
                        SET 
                            Username = @user,
                            IsActive = @active
                            {updatePassClause}
                            WHERE Id = @id
                        ", conn, trans);

                    cmdUser.Parameters.AddWithValue("@user", txtUsernameTaiKhoan.Text.Trim());
                    cmdUser.Parameters.AddWithValue("@full", txtFullNameNhanVien.Text.Trim());
                    cmdUser.Parameters.AddWithValue("@phone", txtDienThoaiNhanVien.Text.Trim());
                    cmdUser.Parameters.AddWithValue("@active", rbHoatDong_Co.Checked);
                    cmdUser.Parameters.AddWithValue("@id", userId);

                    // update NV
                    SqlCommand cmdUpdateNV = new SqlCommand(@"
                        UPDATE NhanVien 
                        SET TenNV = @ten, DienThoai = @dt
                        WHERE UserId = @uid
                    ", conn, trans);

                    cmdUpdateNV.Parameters.AddWithValue("@ten", txtFullNameNhanVien.Text.Trim());
                    cmdUpdateNV.Parameters.AddWithValue("@dt", txtDienThoaiNhanVien.Text.Trim());
                    cmdUpdateNV.Parameters.AddWithValue("@uid", userId);
                    cmdUpdateNV.ExecuteNonQuery();

                    if (passwordHash != null)
                        cmdUser.Parameters.AddWithValue("@pass", passwordHash);

                    cmdUser.ExecuteNonQuery();

                    // xóa role cũ
                    SqlCommand cmdDel = new SqlCommand(@"
                        DELETE FROM UserRoles WHERE UserId = @uid
                    ", conn, trans);
                    cmdDel.Parameters.AddWithValue("@uid", userId);
                    cmdDel.ExecuteNonQuery();

                    // add role mới
                    foreach (RoleItem item in lsbQuyenHan.SelectedItems)
                    {
                        SqlCommand cmdUserRole = new SqlCommand(@"
                            INSERT INTO UserRoles (UserId, RoleId, AssignedAt)
                            VALUES (@uid, @rid, GETDATE())
                        ", conn, trans);

                        cmdUserRole.Parameters.AddWithValue("@uid", userId);
                        cmdUserRole.Parameters.AddWithValue("@rid", item.Id);
                        cmdUserRole.ExecuteNonQuery();
                    }

                    trans.Commit();
                    MessageBox.Show("Sửa tài khoản thành công!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi khi sửa tài khoản: " + ex.Message);
                    return;
                }
            }

            LoadDanhSachTaiKhoan();
            ResetForm();
        }

        // event btnXoa_Click
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDTaiKhoan.Text))
            {
                MessageBox.Show("Chưa chọn tài khoản để xóa!");
                return;
            }

            int userId;
            if (!int.TryParse(txtIDTaiKhoan.Text, out userId))
            {
                MessageBox.Show("ID tài khoản không hợp lệ!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đánh dấu tài khoản này là đã xóa?",
                "Xác nhận xóa?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE Users 
                SET IsDeleted = 1 
                WHERE Id = @id
            ", conn);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Đã đánh dấu tài khoản là đã xóa!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message);
                    return;
                }
            }

            LoadDanhSachTaiKhoan();
            ResetForm();
        }

        // event btnThoat_Click
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // class hiển thị role listbox lsbQuyenHan
        private class RoleItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSachTaiKhoan();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachTaiKhoan();
        }


        private void lsbQuyenHan_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}