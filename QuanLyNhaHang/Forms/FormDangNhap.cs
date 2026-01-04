using QuanLyNhaHang.Models;
using QuanLyNhaHang.Services;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormDangNhap : Form
    {
        public bool IsMdiChildMode { get; set; } = false;
        public FormDangNhap()
        {
            InitializeComponent();

            txtPasswordLogin.UseSystemPasswordChar = true;
        }

        //event btnDangNhapLogin_Click
        private void BtnDangNhapLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsernameLogin.Text.Trim();
            string password = txtPasswordLogin.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Password!",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var ctx = new RestaurentContextDB())
                {
                    string hash = AuthService.ComputeHash(password);

                    var user = ctx.Users.FirstOrDefault(u =>
                            u.Username == username &&
                            u.PasswordHash == hash &&
                            u.IsActive == true &&
                            u.IsDeleted == false);

                    if (user == null)
                    {
                        MessageBox.Show("Sai username hoặc password! \nHoặc có thể tài khoản đã bị xóa!",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // luu session
                    SessionManager.SignIn(user);


                    if (!IsMdiChildMode)
                    {
                        MessageBox.Show(
                            $"Đăng nhập thành công!\nXin chào {user.Username}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //event btnThoatLogin_Click
        private void BtnThoatLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //link label DangKyTaiKhoan_LinkClicked
        private void Linklabel_DangKyTaiKhoan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.MdiParent != null)
            {
                var f = new FormBeforeDangKy
                {
                    IsMdiChildMode = true
                };

                f.FormClosed += (s, args) =>
                {
                    var fb = s as FormBeforeDangKy;
                    if (fb != null && fb.Authorized)
                        OpenOrShowMdiChild(new FormDangKy { MdiParent = this.MdiParent });
                };

                OpenOrShowMdiChild(f);
            }
            else
            {
                using (var f = new FormBeforeDangKy())
                {
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        using (var dk = new FormDangKy())
                        {
                            dk.ShowDialog(this);
                        }
                    }
                }
            }
        }



        private void OpenOrShowMdiChild(Form frm)
        {
            var parent = frm.MdiParent as FormMain;
            if (parent != null)
            {
                foreach (Form child in parent.MdiChildren.ToArray())
                {
                    if (object.ReferenceEquals(child, frm)) continue;
                    child.Close();
                }

                frm.MdiParent = parent;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                if (frm.IsDisposed) return;
                frm.Show();
            }
            else
            {
                if (frm.IsDisposed) return;
                frm.Show();
            }
        }
    }
}
