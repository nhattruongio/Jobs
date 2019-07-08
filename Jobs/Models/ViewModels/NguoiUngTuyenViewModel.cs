using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs.Models.ViewModels
{
    public class NguoiUngTuyenViewModel
    {
        public int? MaKH { get; set; }
        public int? MaCV { get; set; }
        public string TenKH { get; set; }
        public string TaiKhoanKH { get; set; }
        public string DienThoaiKH { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public string TinhTrangXetTuyen { get; set; }
        public DateTime? NgayDang { get; set; }
    }
}