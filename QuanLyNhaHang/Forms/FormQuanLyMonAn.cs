using System;
using System.IO;
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
    public partial class FormQuanLyMonAn : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        DataTable dtMonAn = new DataTable();

        //đường dẫn image mặc định để load vào thẻ món khi k có image
        string duongDanMacDinh = @"D:\1 IMAGE DOAN\no-image.png";

        public FormQuanLyMonAn()
        {
            InitializeComponent();

            
            var col = new DataGridViewTextBoxColumn { Name = "HinhAnh", HeaderText = "HinhAnh", Visible = false };
            if (!dgvMonAn.Columns.Contains("HinhAnh"))
                dgvMonAn.Columns.Add(col);
        }


        private void FormQuanLyMonAn_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
            LoadComboSapXep();
            LoadDataRealtime();
            HienAnhMacDinh();
        }


        private void LoadDanhMuc()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboDanhMuc.DataSource = dt;
                cboDanhMuc.DisplayMember = "TenDanhMuc";
                cboDanhMuc.ValueMember = "MaDanhMuc";
                cboDanhMuc.SelectedIndex = -1;
            }
        }


        private void LoadComboSapXep()
        {
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new string[]
            {
                "Tên món A-Z",
                "Tên món Z-A",
                "Giá tăng dần",
                "Giá giảm dần"
            });
            cboSapXep.SelectedIndex = 0;
        }


        private void LoadDataRealtime(string where = "", string order = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql =
                @"SELECT m.MaMon, m.TenMon, m.DonGia, d.TenDanhMuc, m.HinhAnh
                  FROM MonAn m
                  JOIN DanhMuc d ON m.MaDanhMuc = d.MaDanhMuc";

                if (!string.IsNullOrEmpty(where))
                    sql += " WHERE " + where;

                if (!string.IsNullOrEmpty(order))
                    sql += " ORDER BY " + order;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                dtMonAn.Clear();
                da.Fill(dtMonAn);

                dgvMonAn.Rows.Clear();
                foreach (DataRow r in dtMonAn.Rows)
                {
                  
                    var index = dgvMonAn.Rows.Add(r[0], r[1], r[2], r[3]);
                    dgvMonAn.Rows[index].Tag = r[4]?.ToString();
                }
            }
        }

        // event cell click  dgvMonAn
        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var r = dgvMonAn.Rows[e.RowIndex];
            txtMaMon.Text = r.Cells[0].Value?.ToString() ?? "";
            txtTenMon.Text = r.Cells[1].Value?.ToString() ?? "";
            txtGiaTien.Text = r.Cells[2].Value?.ToString() ?? "";
            cboDanhMuc.Text = r.Cells[3].Value?.ToString() ?? "";

            // doc path image
            string path = r.Tag as string;
            if (string.IsNullOrEmpty(path) && r.Cells.Count > 4)
                path = r.Cells[4].Value?.ToString();

            LoadAnh(path); // load anh tu path vao picturebox
        }

        private void LoadAnh(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picMonAn.Image = Image.FromStream(fs);
                    }
                    txtHinhAnh.Text = path;
                }
                else
                {
                    HienAnhMacDinh();
                }
            }
            catch
            {
                HienAnhMacDinh();
            }
        }

        private void HienAnhMacDinh()
        {
            if (File.Exists(duongDanMacDinh))
            {
                using (var fs = new FileStream(duongDanMacDinh, FileMode.Open, FileAccess.Read))
                {
                    picMonAn.Image = Image.FromStream(fs);
                }
                txtHinhAnh.Text = duongDanMacDinh;
            }
            else
            {
                picMonAn.Image = null;
                txtHinhAnh.Text = "";
            }
        }

        // btn chọn ảnh
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.png;*.jpeg;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picMonAn.Image = Image.FromFile(ofd.FileName);
                txtHinhAnh.Text = ofd.FileName;
            }
        }

        //check dữ liệu nhập
        private bool KiemTraNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Chưa nhập tên món!");
                return false;
            }

            if (!decimal.TryParse(txtGiaTien.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá tiền không hợp lệ!");
                return false;
            }

            if (cboDanhMuc.SelectedIndex < 0)
            {
                MessageBox.Show("Chưa chọn danh mục!");
                return false;
            }

            return true;
        }

        //btnThem click
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                @"INSERT INTO MonAn (TenMon, DonGia, MaDanhMuc, HinhAnh)
                  VALUES (@ten,@gia,@dm,@img)", conn);

                cmd.Parameters.AddWithValue("@ten", txtTenMon.Text.Trim());
                cmd.Parameters.AddWithValue("@gia", txtGiaTien.Text);
                cmd.Parameters.AddWithValue("@dm", cboDanhMuc.SelectedValue);
                cmd.Parameters.AddWithValue("@img",
                    string.IsNullOrEmpty(txtHinhAnh.Text) ? duongDanMacDinh : txtHinhAnh.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm món thành công!");
            LoadDataRealtime();
        }

        // btn Sua_Click
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text)) return;
            if (!KiemTraNhap()) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                @"UPDATE MonAn
                  SET TenMon=@ten, DonGia=@gia, MaDanhMuc=@dm, HinhAnh=@img
                  WHERE MaMon=@id", conn);

                cmd.Parameters.AddWithValue("@ten", txtTenMon.Text.Trim());
                cmd.Parameters.AddWithValue("@gia", txtGiaTien.Text);
                cmd.Parameters.AddWithValue("@dm", cboDanhMuc.SelectedValue);
                cmd.Parameters.AddWithValue("@img",
                    string.IsNullOrEmpty(txtHinhAnh.Text) ? duongDanMacDinh : txtHinhAnh.Text);
                cmd.Parameters.AddWithValue("@id", txtMaMon.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật thành công!");
            LoadDataRealtime();
        }

        //btnXoa_Click
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text)) return;

            if (MessageBox.Show("Xóa món này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM MonAn WHERE MaMon=@id", conn);
                cmd.Parameters.AddWithValue("@id", txtMaMon.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Đã xóa!");
            LoadDataRealtime();
        }

        //txtTimKiem_TextChanged
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();
            LoadDataRealtime($"m.TenMon LIKE N'%{key}%'", "");
        }

        // cboSapXep_SelectedIndexChanged
        private void cboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string order = "m.TenMon ASC";
            if (cboSapXep.Text.Contains("Z-A")) order = "m.TenMon DESC";
            if (cboSapXep.Text.Contains("tăng")) order = "m.DonGia ASC";
            if (cboSapXep.Text.Contains("giảm")) order = "m.DonGia DESC";
            LoadDataRealtime("", order);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
