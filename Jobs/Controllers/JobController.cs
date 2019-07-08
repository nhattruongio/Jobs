using Jobs.Common;
using Jobs.Models;
using Jobs.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Jobs.Controllers
{
    public class JobController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public JobController()
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

                if (result.LoaiTK == 2)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AuthorizeAccount]
        public ActionResult Index(CONGVIEC cv, HttpPostedFileBase Anh)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);

            if (result != null)
            {
                int maKH = result.MaKH;

                string status = String.Format("{0}", Request.Form["tinhtrang"]);
                if (string.IsNullOrEmpty(cv.SoLuong))
                {
                    ViewBag.soluong = "<div class=\"alert alert-danger\" role=\"alert\">Nhập số lượng tuyển</div>";
                }
                else if (string.IsNullOrEmpty(cv.YeuCau))
                {
                    ViewBag.soluong = "<div class=\"alert alert-danger\" role=\"alert\">Nhập yêu cầu</div>";
                }
                else if (string.IsNullOrEmpty(cv.DiaChi))
                {
                    ViewBag.address = "<div class=\"alert alert-danger\" role=\"alert\">Nhập địa chỉ</div>";
                }
                else if (string.IsNullOrEmpty(cv.Luong))
                {
                    ViewBag.salary = "<div class=\"alert alert-danger\" role=\"alert\">Nhập lương</div>";
                }
                else if (string.IsNullOrEmpty(cv.MoTa))
                {
                    ViewBag.description = "<div class=\"alert alert-danger\" role=\"alert\">Nhập mô tả</div>";
                }
                else if (string.IsNullOrEmpty(cv.ChucVu))
                {
                    ViewBag.position = "<div class=\"alert alert-danger\" role=\"alert\">Nhập chức vụ</div>";
                }
                else
                {
                    if (Anh != null && Anh.ContentLength > 0)
                    {
                        var fileName = StringRandom.getRandomString(5) + Path.GetFileName(Anh.FileName);
                        var image_path = "uploads/";
                        var path = Server.MapPath("~/uploads/");
                        Anh.SaveAs(path + fileName);

                        DateTime now = DateTime.Now;
                        CONGVIEC congviec = new CONGVIEC
                        {
                            SoLuong = cv.SoLuong,
                            YeuCau = cv.YeuCau,
                            DiaChi = cv.DiaChi,
                            Luong = cv.Luong,
                            MaKH = maKH,
                            MoTa = cv.MoTa,
                            ChucVu = cv.ChucVu,
                            DaNhan = 0,
                            Anh = image_path + fileName,
                            NgayCapNhat = now,
                            TinhTrang = status
                        };

                        _dbContext.CONGVIECes.Add(congviec);
                        _dbContext.SaveChanges();

                        return RedirectToAction("Index", "Job", new { area = "" });
                    }
                    else
                    {
                        ViewBag.image = "<div class=\"alert alert-danger\" role=\"alert\">Chọn ảnh đại diện</div>";
                    }
                }
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ListJob()
        {
            var listJob = _dbContext.CONGVIECes.OrderByDescending(x => x.MaCV).ToList();
            return View(listJob);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult View(int? id)
        {
            var data_job = _dbContext.CONGVIECes.SingleOrDefault(x => x.MaCV == id);
            var data_user = _dbContext.KHACHHANGs.SingleOrDefault(x => x.MaKH == data_job.MaKH);

            ViewData["username"] = data_user.TaiKhoanKH;
            return View(data_job);
        }

        [HttpPost]
        [AuthorizeAccount]
        public JsonResult UngTuyen(int? id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int user_id = session.UserID;
            var check = _dbContext.HOSOs.SingleOrDefault(x => x.MaKH == user_id && x.MaCV == id);

            if (check == null)
            {
                DateTime now = DateTime.Now;
                HOSO hs = new HOSO
                {
                    MaKH = user_id,
                    MaCV = id,
                    TinhTrangXetTuyen = "Chưa xét duyệt",
                    NgayDang = now
                };

                var data_user = (from a in _dbContext.KHACHHANGs
                                 where a.MaKH == user_id
                                 select new NguoiDungViewModel
                                 {
                                     Email = a.Email
                                 }).FirstOrDefault();

                string Mail = data_user.Email;

                string body = "<h1><font color = \"#00b894\">Ứng tuyển thành công</font></h1><br /><p><i><font color=\"#dfe6e9\">Chúng tôi đã nhận được đơn ứng tuyển từ bạn. Nếu bạn đổi ý, hãy bỏ qua email này";

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = true;
                WebMail.UserName = "phutruongckk33@gmail.com";
                WebMail.Password = "tumotdentam";
                WebMail.From = "phutruongckk33@gmail.com";
                WebMail.Send(to: Mail, subject: "Ứng tuyển thành công", body: body, isBodyHtml: true);

                _dbContext.Configuration.ProxyCreationEnabled = false;
                _dbContext.HOSOs.Add(hs);
                _dbContext.SaveChanges();

                return Json(new { data = "Ứng tuyển thành công", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = "Bạn đã ứng tuyển", status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult XemNguoiUngTuyen(int? id)
        {
            if (Session != null || Session["NguoiDung"] == null)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);

                if (result.LoaiTK == 2)
                {
                    var model = (from a in _dbContext.HOSOs
                                 join b in _dbContext.KHACHHANGs
                                 on a.MaKH equals b.MaKH
                                 where a.MaCV == id
                                 select new NguoiUngTuyenViewModel
                                 {
                                     TenKH = b.TenKH,
                                     TaiKhoanKH = b.TaiKhoanKH,
                                     DienThoaiKH = b.DienThoaiKH,
                                     Email = b.Email,
                                     DiaChi = b.DiaChi,
                                     GioiTinh = b.GioiTinh,
                                     TinhTrangXetTuyen = a.TinhTrangXetTuyen,
                                     NgayDang = a.NgayDang,
                                     MaKH = a.MaKH,
                                     MaCV = a.MaCV
                                 }).ToList();

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AuthorizeAccount]
        public JsonResult XetDuyet(int? makh, int? macv)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);

            if (result.LoaiTK == 2)
            {
                var check = _dbContext.HOSOs.SingleOrDefault(x => x.MaCV == macv && x.MaKH == makh && x.TinhTrangXetTuyen == "Chưa xét duyệt");

                if (check != null)
                {
                    check.TinhTrangXetTuyen = "Đã xét duyệt";
                    _dbContext.SaveChanges();

                    return Json(new { data = "Duyệt thành công", status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "Duyệt không thành công", status = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Duyệt không thành công", status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult UngTuyenThanhCong(int? id)
        {
            return View();
        }
    }
}