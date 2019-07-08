using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jobs.Models
{
    [Table("HOSO")]
    public partial class HOSO
    {
        [Key]
        public int MaHS { get; set; }

        public int? MaKH { get; set; }

        public int? MaCV { get; set; }

        [StringLength(30)]
        public string TinhTrangXetTuyen { get; set; }

        public DateTime? NgayDang { get; set; }

        [StringLength(30)]

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual CONGVIEC CONGVIEC { get; set; }
    }
}