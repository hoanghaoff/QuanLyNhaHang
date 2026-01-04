using QuanLyNhaHang.Models;
using QuanLyNhaHang.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormBeforeDangKy : Form
    {

        public bool IsMdiChildMode { get; set; } = false;

        public bool Authorized { get; private set; } = false;         // set thành true nếu đăng nhập là role chủ

        public FormBeforeDangKy()
        {
            InitializeComponent();

            btnBeforeDangKyThoat.DialogResult = DialogResult.Cancel;
            btnBeforeDangKyThoat.Click += (s, e) =>
            {
                Debug.WriteLine("btnBeforeDangKyThoat clicked -> Close()");
                Close();
            };

            Debug.WriteLine("FormBeforeDangKy ctor");
            this.FormClosing += (s, e) => Debug.WriteLine("FormBeforeDangKy FormClosing");
        }

        private void BtnBeforeDangKyLogin_Click(object sender, EventArgs e)
        {
            string username = txtBeforeDangKyUsernameLogin.Text.Trim();
            string password = txtBeforeDangKyPasswordLogin.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!");
                return;
            }

            using (var ctx = new RestaurentContextDB())
            {
                string hash = AuthService.ComputeHash(password);

                var user = ctx.Users.FirstOrDefault(u =>
                    u.Username == username &&
                    u.PasswordHash == hash &&
                    u.IsActive &&
                    !u.IsDeleted);

                if (user == null)
                {
                    MessageBox.Show("Sai thông tin đăng nhập!");
                    return;
                }

                bool isChu = ctx.UserRoles
                    .Any(ur => ur.UserId == user.Id &&
                               ur.Roles.Name == "Chủ");

                if (!isChu)
                {
                    MessageBox.Show(
                        "Chỉ Chủ nhà hàng mới được phép đăng ký tài khoản!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                
                Authorized = true;

                if (!IsMdiChildMode)
                    this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}
