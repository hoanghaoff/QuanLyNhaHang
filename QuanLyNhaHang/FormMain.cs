using DocumentFormat.OpenXml.Wordprocessing;
using QuanLyNhaHang.Forms;
using QuanLyNhaHang.Models;
using QuanLyNhaHang.Services;
using RestaurantApp;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            mnuHeThong_DangNhap.Click += MnuHeThong_DangNhap_Click;
            mnuHeThong_DangXuat.Click += MnuHeThong_DangXuat_Click;
            mnuHeThong_Thoat.Click += (s, e) => Close();
            
        }

        // load form
        private void FormMain_Load(object sender, EventArgs e)
        {
            using (var f = new FormDangNhap())
            {
                if (f.ShowDialog(this) != DialogResult.OK || !SessionManager.IsSignedIn)
                {
                    Close();
                    return;
                }
            }

            UpdateMenusForCurrentUser();
            CapNhatStatusXinChao();

            MessageBox.Show(
                $"Xin chào {SessionManager.CurrentUser?.FullName}",
                "Đăng nhập thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void timerDongHo_Tick(object sender, EventArgs e)
        {
            CapNhatStatusXinChao();
        }

        // status strip
        private void CapNhatStatusXinChao()
        {
            var user = SessionManager.CurrentUser?.Username ?? "vui lòng đăng nhập";
            string dayngay = DateTime.Now.ToString("dd");
            string monthngay = DateTime.Now.ToString("MM");
            string yearngay = DateTime.Now.ToString("yyyy");
            string gio = DateTime.Now.ToString("HH:mm:ss");
            toolStripStatusLabel_Xinchao.Text = $"Xin chào, {user}! Hôm nay là ngày {dayngay}/{monthngay}/{yearngay} - Bây giờ là {gio}";
        }

        // login-logout
        private void MnuHeThong_DangNhap_Click(object sender, EventArgs e)
        {
            var f = new Forms.FormDangNhap
            {
                IsMdiChildMode = true,
            };

            f.FormClosed += (s, args) =>
            {
                if (SessionManager.IsSignedIn)
                {
                    UpdateMenusForCurrentUser();
                    CapNhatStatusXinChao();

                    MessageBox.Show(
                        $"Xin chào {SessionManager.CurrentUser?.FullName}",
                        "Đăng nhập thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            };

            OpenMdiForm(f);
        }

        private void MnuHeThong_DangXuat_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsSignedIn) return;

            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // close tất cả các form mdi con
            foreach (var child in this.MdiChildren)
            {
                try
                {
                    child.Close();
                }
                catch
                {
                    
                }
            }

            SessionManager.SignOut();

            //cập nhật lại menu và status sau khi đăng xuất
            UpdateMenusForCurrentUser();
            CapNhatStatusXinChao();
        }

        //load phân quyền theo role của user
        private void UpdateMenusForCurrentUser()
        {
            bool signedIn = SessionManager.IsSignedIn;

            mnuHeThong_DangNhap.Visible = !signedIn;
            mnuHeThong_DangXuat.Visible = signedIn;

            mnuQuanLy.Visible = false;
            mnuBanHang.Visible = false;
            mnuTinhTrangBan.Visible = false;
            mnuHoaDon.Visible = false;
            mnuThongKe.Visible = false;
            mnuQuanLyPhanQuyen.Visible = false;
            mnuQuanLyTaiKhoan.Visible = false;

            if (!signedIn) return;

            if (SessionManager.IsInRole("Chủ"))
            {
                mnuQuanLy.Visible = true;
                mnuBanHang.Visible = true;
                mnuTinhTrangBan.Visible = true;
                mnuHoaDon.Visible = true;
                mnuThongKe.Visible = true;
                mnuQuanLyPhanQuyen.Visible = true;
                mnuQuanLyTaiKhoan.Visible = true;
                return;
            }

            if (SessionManager.IsInRole("Quản lý"))
            {
                mnuQuanLy.Visible = true;
                mnuBanHang.Visible = true;
                mnuTinhTrangBan.Visible = true;
                mnuHoaDon.Visible = true;
                mnuThongKe.Visible = true;
                return;
            }

            if (SessionManager.IsInRole("Nhân Viên"))
            {
                mnuBanHang.Visible = true;
                mnuTinhTrangBan.Visible = true;
                mnuHoaDon.Visible = true;
            }
        }

        // open mdi form
        private void OpenMdiForm(Form frm)
        {
            foreach (Form child in this.MdiChildren.ToArray())
            {
                if (!object.ReferenceEquals(child, frm))
                    child.Close();
            }

            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            if (frm.IsDisposed) return; // extra safety
            frm.Show();
        }

        private void mnuQuanLyBan_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyBan());

        private void mnuQuanLyDanhMucMonAn_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyDanhMucMonAn());

        private void mnuQuanLyMonAn_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyMonAn());

        private void mnuQuanLyKhachHang_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyKhachHang());

        private void mnuQuanLyNhanVien_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyNhanVien());

        private void mnuBanHang_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormBanHang());

        private void mnuTinhTrangBan_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormTinhTrangBan());

        private void mnuQuanLyPhanQuyen_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyPhanQuyen());
        private void mnuQuanLyTaiKhoan_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormQuanLyTaiKhoan());
        private void mnuHoaDon_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormHoaDon());
        private void mnuThongKe_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormThongKe());
        private void mnuTroGiup_GioiThieuVePhanMem_Click(object sender, EventArgs e)
            => OpenMdiForm(new FormTroGiup_GioiThieuVePhanMem());

        private void mnuHeThong_DangKy_Click(object sender, EventArgs e)
        {
            // mở form BeforeDangKy login chủ trc rồi mới cho đki acc
            using (var f = new Forms.FormBeforeDangKy())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    OpenMdiForm(new Forms.FormDangKy());
                }
            }
        }

        


    }
}
