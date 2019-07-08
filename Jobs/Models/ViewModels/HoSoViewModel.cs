using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs.Models.ViewModels
{
    public class HoSoViewModel
    {
        public int MaHS { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string TinhTrang { get; set; }
        public DateTime? NgayDang { get; set; }
    }
}