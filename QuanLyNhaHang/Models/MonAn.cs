namespace QuanLyNhaHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonAn")]
    public partial class MonAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonAn()
        {
            HoaDonChiTiet = new HashSet<HoaDonChiTiet>();
        }

        [Key]
        public int MaMon { get; set; }

        [StringLength(100)]
        public string TenMon { get; set; }

        public double DonGia { get; set; }

        public int MaDanhMuc { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiet { get; set; }
    }
}
