using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs.Models.ViewModels
{
    public class CongViecViewModel
    {
        public int? MaCV { get; set; }
        public int? MaKH { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayDang { get; set; }
        public string YeuCau { get; set; }
        public string Anh { get; set; }
        public string TinhTrangXetTuyen { get; set; }
    }
}