using Microsoft.Reporting.WinForms;
using QuanLyNhaHang.RDLCHoaDon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhaHang.Forms
{
    public partial class FormInHoaDon : Form
    {
        private readonly string connStr = @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";
        private int _maHoaDon;

        public FormInHoaDon(int maHoaDon)
        {
            InitializeComponent();
            _maHoaDon = maHoaDon;
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                var header = LayHeader(_maHoaDon);
                var chiTiet = LayChiTiet(_maHoaDon);


                rpvHoaDon.LocalReport.ReportEmbeddedResource = "QuanLyNhaHang.RDLCHoaDon.HoaDon.rdlc";

                rpvHoaDon.LocalReport.DataSources.Clear();
                rpvHoaDon.LocalReport.DataSources.Add(new ReportDataSource("dsHeader", new[] { header }));
                rpvHoaDon.LocalReport.DataSources.Add(new ReportDataSource("dsChiTiet", chiTiet));

                rpvHoaDon.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hóa đơn: " + ex.Message);
            }
        }

        private HoaDonHeader LayHeader(int maHD)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
            SELECT 
                h.MaHoaDon,
                h.NgayLap,
                b.TenBan,
                b.LoaiBan,
                nv.TenNV,
                ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                ISNULL(kh.LoaiKH, N'Khách thường') AS LoaiKH,
                ISNULL(h.KhuyenMai, 0) AS KhuyenMai,
                ISNULL(h.HinhThucThanhToan, N'Tiền mặt') AS HinhThuc
            FROM HoaDon h
            INNER JOIN Ban b ON h.MaBan = b.MaBan
            INNER JOIN NhanVien nv ON h.MaNV = nv.MaNV
            LEFT JOIN KhachHang kh ON h.MaKH = kh.MaKH
            WHERE h.MaHoaDon = @hd", conn);
                cmd.Parameters.AddWithValue("@hd", maHD);

                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return new HoaDonHeader
                        {
                            TenQuan = "NHÀ HÀNG HHPC",
                            DiaChi = "Địa chỉ: Khu Công nghệ cao, Quận 9, TP.HCM",
                            SDT = "Hotline: 0989 491 137",
                            MaHoaDon = rd.GetInt32(0),
                            NgayLap = rd.GetDateTime(1).ToString("dd/MM/yyyy HH:mm"),
                            TenBan = rd.GetString(2),
                            LoaiBan = rd.GetString(3),
                            TenNhanVien = rd.GetString(4),
                            TenKhachHang = rd.GetString(5),
                            LoaiKhachHang = rd.GetString(6),
                            KhuyenMai = Convert.ToDecimal(rd.GetValue(7))
                        };
                    }
                }
            }
            throw new Exception("Không tìm thấy hóa đơn");
        }

        private List<HoaDonChiTiet> LayChiTiet(int maHD)
        {
            var list = new List<HoaDonChiTiet>();
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT m.TenMon, c.SoLuong, c.DonGia, c.SoLuong * c.DonGia
                    FROM HoaDonChiTiet c
                    JOIN MonAn m ON c.MaMon = m.MaMon
                    WHERE c.MaHoaDon = @hd", conn);
                cmd.Parameters.AddWithValue("@hd", maHD);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new HoaDonChiTiet
                        {
                            TenMon = rd.GetString(0),
                            SoLuong = rd.GetInt32(1),
                            DonGia = Convert.ToDecimal(rd.GetValue(2)),
                            ThanhTien = Convert.ToDecimal(rd.GetValue(3))
                        });
                    }
                }
            }
            return list;
        }
    }
}