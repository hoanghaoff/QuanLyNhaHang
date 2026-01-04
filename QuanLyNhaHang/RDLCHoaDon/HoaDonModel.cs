using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.RDLCHoaDon
{
    public class HoaDonHeader
{
    public string TenQuan { get; set; }
    public string DiaChi { get; set; }
    public string SDT { get; set; }
    public int MaHoaDon { get; set; }
    public string NgayLap { get; set; }
    public string TenBan { get; set; }
    public string LoaiBan { get; set; } // ← Thêm trường này
    public string TenNhanVien { get; set; }
    public string TenKhachHang { get; set; }
    public string LoaiKhachHang { get; set; }
    public decimal KhuyenMai { get; set; } // % khuyến mãi
    public decimal TongTienTruocKM { get; set; } // ← Tổng tiền gốc
    public decimal TongTienSauKM { get; set; } // ← Tổng tiền sau KM
}

    public class HoaDonChiTiet
    {
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
