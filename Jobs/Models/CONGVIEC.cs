using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jobs.Models
{
    [Table("CONGVIEC")]
    public partial class CONGVIEC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONGVIEC()
        {
            HOSOs = new HashSet<HOSO>();
        }

        [Key]
        public int MaCV { get; set; }

        [StringLength(10)]
        public string SoLuong { get; set; }

        [StringLength(30)]
        public string YeuCau { get; set; }

        [StringLength(300)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string Luong { get; set; }

        public int? MaKH { get; set; }

        [StringLength(300)]
        public string MoTa { get; set; }

        public int? DaNhan { get; set; }

        [StringLength(20)]
        public string ChucVu { get; set; }

        public string Anh { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        [StringLength(60)]
        public string TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOSO> HOSOs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}