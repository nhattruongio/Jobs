using Jobs.Common;
using Jobs.Models;
using Jobs.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Controllers
{
    public class ProfileController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public ProfileController()
        {
            _dbContext = new JobDatabaseContext();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult Index()
        {
            if (Session != null || Session["NguoiDung"] == null)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];

                var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);
                var loaitk = _dbContext.LOAITKs.SingleOrDefault(x => x.ID == result.LoaiTK);

                ViewBag.LoaiTK = loaitk.LoaiTK;
                return View(result);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AuthorizeAccount]
        public ActionResult Index(KHACHHANG nd)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);
            string sex = String.Format("{0}", Request.Form["sex"]);

            if (result != null)
            {
                if (string.IsNullOrEmpty(nd.TenKH))
                {
                    ViewBag.fullname = "<div class=\"alert alert-danger\" role=\"alert\">Nhập tên đầy đủ</div>";
                }
                else if (string.IsNullOrEmpty(nd.DienThoaiKH))
                {
                    ViewBag.tel = "<div class=\"alert alert-danger\" role=\"alert\">Nhập số điện thoại</div>";
                }
                else if (string.IsNullOrEmpty(nd.DiaChi))
                {
                    ViewBag.address = "<div class=\"alert alert-danger\" role=\"alert\">Nhập địa chỉ</div>";
                }
                else
                {
                    result.TenKH = nd.TenKH;
                    result.DienThoaiKH = nd.DienThoaiKH;
                    result.DiaChi = nd.DiaChi;
                    result.GioiTinh = sex;

                    _dbContext.SaveChanges();
                    return View();
                }
            }
            else
            {
                ViewBag.incorrect = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản không tồn tại</div>";
            }

            return View();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult ListJob()
        {
            if (Session != null || Session["NguoiDung"] == null)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                //var model = _dbContext.HOSOs.Where(x => x.MaKH == session.UserID).ToList();

                var model = (from a in _dbContext.HOSOs
                             join b in _dbContext.CONGVIECes
                             on a.MaCV equals b.MaCV
                             where a.MaKH == session.UserID
                             select new CongViecViewModel
                             {
                                 MaCV = b.MaCV,
                                 MaKH = a.MaKH,
                                 MoTa = b.MoTa,
                                 NgayDang = a.NgayDang,
                                 TinhTrangXetTuyen = a.TinhTrangXetTuyen,
                                 Anh = b.Anh
                             }).OrderByDescending(x => x.NgayDang).ToList();
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult NguoiUngTuyen()
        {
            if (Session != null || Session["NguoiDung"] == null)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);

                var model = _dbContext.CONGVIECes.Where(x => x.MaKH == result.MaKH).ToList();
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewProfile(int? id)
        {
            var data = _dbContext.KHACHHANGs.SingleOrDefault(x => x.MaKH == id);
            return View(data);
        }
    }
}