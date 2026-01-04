using QuanLyNhaHang.Models;
using QuanLyNhaHang.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();

            Load += FormDangKy_Load;
            btnDangKy.Click += BtnDangKy_Click;
            btnThoatDangKy.Click += (s, e) => Close();
        }

        // ================= LOAD DATA =================
        private void FormDangKy_Load(object sender, EventArgs e)
        {
            using (var ctx = new RestaurentContextDB())
            {
                // load roles vào listbox
                lsbQuyenHan.DataSource = ctx.Roles.ToList();
                lsbQuyenHan.DisplayMember = "Name";
                lsbQuyenHan.ValueMember = "Id";
                lsbQuyenHan.SelectionMode = SelectionMode.MultiSimple;

                // load nhân viên vào combobox (có phân biệt đã có tài khoản hay chưa)
                var nhanVienList = ctx.NhanVien
                    .Select(nv => new
                    {
                        nv.MaNV,
                        nv.TenNV,
                        DaCoTK = nv.UserId != null
                    })
                    .ToList()
                    .Select(x => new
                    {
                        MaNV = x.MaNV,
                        HienThi = x.TenNV + (x.DaCoTK ? " (Đã có tài khoản)" : "")
                    })
                    .ToList();

                cboThuocVeNhanVien.DataSource = nhanVienList;
                cboThuocVeNhanVien.DisplayMember = "HienThi";
                cboThuocVeNhanVien.ValueMember = "MaNV";
                cboThuocVeNhanVien.SelectedIndex = -1;
            }
        }

        // event btnDangKy_Click
        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtUsernameDangKy.Text.Trim();
            string password = txtPasswordDangKy.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!");
                return;
            }

            if (lsbQuyenHan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một quyền hạn!");
                return;
            }

            // Lấy MaNV đã chọn
            if (cboThuocVeNhanVien.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!");
                return;
            }

            object selectedValue = cboThuocVeNhanVien.SelectedValue;
            if (selectedValue == null)
            {
                MessageBox.Show("Giá trị nhân viên không hợp lệ!");
                return;
            }

            int maNV = Convert.ToInt32(selectedValue);

            using (var ctx = new RestaurentContextDB())
            {
                // check username nếu đã tồn tại thì nhập lại
                if (ctx.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Username đã tồn tại!");
                    return;
                }

                // tìm nv theo maNV
                var nv = ctx.NhanVien.FirstOrDefault(x => x.MaNV == maNV);
                if (nv == null)
                {
                    MessageBox.Show("Nhân viên không tồn tại!");
                    return;
                }

                bool daCoTaiKhoan = nv.UserId != null;

                // nếu NV đã có tài khoản thì hỏi có muốn ghi đè không
                if (daCoTaiKhoan)
                {
                    var confirm = MessageBox.Show(
                        "Tài khoản cũ của nhân viên này sẽ ngừng hoạt động \nvà vào trạng thái Xóa.\nBạn có chắc chắn muốn tạo tài khoản mới cho nhân viên này?",
                        "Xác nhận ghi đè tài khoản",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm != DialogResult.Yes)
                        return;

                    // vô hiệu acc cũ
                    var userCu = ctx.Users.FirstOrDefault(u => u.Id == nv.UserId);
                    if (userCu != null)
                    {
                        userCu.IsActive = false;
                        userCu.IsDeleted = true;
                    }
                }

                // tạo acc mới
                var userMoi = new Users
                {
                    Username = username,
                    PasswordHash = AuthService.ComputeHash(password),
                    FullName = nv.TenNV,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now
                };

                ctx.Users.Add(userMoi);
                ctx.SaveChanges();

                // gán role cho acc mới
                foreach (Roles role in lsbQuyenHan.SelectedItems)
                {
                    ctx.UserRoles.Add(new UserRoles
                    {
                        UserId = userMoi.Id,
                        RoleId = role.Id,
                        AssignedAt = DateTime.Now
                    });
                }

                // gán acc mới cho nv
                nv.UserId = userMoi.Id;

                ctx.SaveChanges();
            }

            MessageBox.Show("Đăng ký tài khoản thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }
    }
}