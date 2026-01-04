namespace QuanLyNhaHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            HoaDonChiTiet = new HashSet<HoaDonChiTiet>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        public DateTime NgayLap { get; set; }

        public int MaNV { get; set; }

        public int? MaKH { get; set; }

        public int MaBan { get; set; }

        public double? TongTien { get; set; }

        [Required]
        [StringLength(20)]
        public string TrangThaiThanhToan { get; set; }

        public DateTime? ThoiDiemThanhToan { get; set; }

        [StringLength(50)]
        public string HinhThucThanhToan { get; set; }

        public virtual Ban Ban { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiet { get; set; }
    }
}
