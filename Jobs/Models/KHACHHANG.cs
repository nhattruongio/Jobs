using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Models
{
    [Table("KHACHHANG")]
    public class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            HOSOs = new HashSet<HOSO>();
            CONGVIECes = new HashSet<CONGVIEC>();
        }

        [Key]
        public int MaKH { get; set; }

        [StringLength(30)]
        public string TenKH { get; set; }

        [StringLength(30)]
        public string TaiKhoanKH { get; set; }

        [StringLength(300)]
        public string MatKhauKH { get; set; }

        [StringLength(10)]
        public string DienThoaiKH { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(90)]
        public string DiaChi { get; set; }

        [StringLength(6)]
        public string GioiTinh { get; set; }

        public int LoaiTK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOSO> HOSOs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONGVIEC> CONGVIECes { get; set; }
    }
}