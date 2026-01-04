using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantApp
{
    public partial class FormTinhTrangBan : Form
    {
        string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        public FormTinhTrangBan()
        {
            InitializeComponent();
            this.Load += FormTinhTrangBan_Load;
        }

        // load form
        private void FormTinhTrangBan_Load(object sender, EventArgs e)
        {
            LoadDanhSachBan();
        }


        private void LoadDanhSachBan()
        {
            flpBan.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT MaBan, TenBan, TrangThai FROM Ban", conn);

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int maBan = (int)rd["MaBan"];
                    string tenBan = rd["TenBan"].ToString();
                    string trangThai = rd["TrangThai"].ToString();

                    Panel pnlBan = TaoBanTuTemplate(maBan, tenBan, trangThai);
                    flpBan.Controls.Add(pnlBan);
                }
            }
        }

        //clone thẻ món
        private Panel TaoBanTuTemplate(int maBan, string tenBan, string trangThai)
        {
            Panel pnl = new Panel();
            pnl.Size = pnlBan.Size;
            pnl.Margin = pnlBan.Margin;
            pnl.BackColor = GetColorByTrangThai(trangThai);

            foreach (Control c in pnlBan.Controls)
            {
                Control clone = (Control)Activator.CreateInstance(c.GetType());
                clone.Size = c.Size;
                clone.Location = c.Location;
                clone.Font = c.Font;

                if (clone is Label lbl)
                {
                    lbl.Text = tenBan;
                }

                if (clone is ComboBox cbo)
                {
                    cbo.Items.AddRange(new string[]
                    {
                        "Trống",
                        "Có khách",
                        "Đã đặt",
                        "Bảo trì"
                    });

                    cbo.SelectedItem = trangThai;
                    cbo.Tag = maBan;
                    cbo.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
                }

                pnl.Controls.Add(clone);
            }

            return pnl;
        }

        //update status bàn khu thay đổi cboTrangThai
        private void CboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo == null || cbo.Tag == null) return;

            int maBan = (int)cbo.Tag;
            string trangThaiMoi = cbo.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Ban SET TrangThai=@tt WHERE MaBan=@id", conn);
                cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                cmd.Parameters.AddWithValue("@id", maBan);
                cmd.ExecuteNonQuery();
            }

            Panel pnl = cbo.Parent as Panel;
            if (pnl != null)
            {
                pnl.BackColor = GetColorByTrangThai(trangThaiMoi);
            }
        }

        // set màu bàn theo trạng thái bàn
        private Color GetColorByTrangThai(string tt)
        {
            switch (tt)
            {
                case "Trống": return Color.LightGreen;
                case "Có khách": return Color.IndianRed;
                case "Đã đặt": return Color.Gold;
                case "Bảo trì": return Color.Gray;
                default: return Color.White;
            }
        }
    }
}
