using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Controllers
{
    public class AuthController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public AuthController()
        {
            _dbContext = new JobDatabaseContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DangNhap()
        {
            var session = Session["NguoiDung"];
            if (Session != null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DangNhap(KHACHHANG nd)
        {
            var userSession = new UserLogin();
            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == nd.TaiKhoanKH);
            if (string.IsNullOrEmpty(nd.TaiKhoanKH))
            {
                ViewBag.username = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập tài khoản!</div>";
            }
            else if (string.IsNullOrEmpty(nd.MatKhauKH))
            {
                ViewBag.password = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập mật khẩu!</div>";
            }
            else if (result == null)
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản không tồn tại!</div>";
            }
            else if (result.MatKhauKH != StringHash.crypto(nd.MatKhauKH))
            {
                ViewBag.confirmPassword = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu không đúng!</div>";
            }
            else
            {
                userSession.UserName = result.TaiKhoanKH;
                userSession.UserID = result.MaKH;
                userSession.Ten = result.TenKH;
                userSession.LoaiTK = result.LoaiTK;

                Session["NguoiDung"] = result.TaiKhoanKH;
                Session["LoaiTK"] = result.LoaiTK;

                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DangKy()
        {
            var session = Session["NguoiDung"];
            if (Session != null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DangKy(KHACHHANG nd)
        {
            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == nd.TaiKhoanKH);
            string input_re_password = String.Format("{0}", Request.Form["re_password"]);
            int role_id = int.Parse(String.Format("{0}", Request.Form["role"]));
            string sex = String.Format("{0}", Request.Form["sex"]);

            if (string.IsNullOrEmpty(nd.TenKH))
            {
                ViewBag.fullname = "<div class=\"alert alert-danger\" role=\"alert\">Họ tên không được để trống</div>";
            }
            else if (string.IsNullOrEmpty(nd.TaiKhoanKH))
            {
                ViewBag.username = "<div class=\"alert alert-danger\" role=\"alert\">Nhập tên tài khoản</div>";
            }
            else if (string.IsNullOrEmpty(nd.Email))
            {
                ViewBag.email = "<div class=\"alert alert-danger\" role=\"alert\">Nhập email</div>";
            }
            else if (string.IsNullOrEmpty(nd.DienThoaiKH))
            {
                ViewBag.tel = "<div class=\"alert alert-danger\" role=\"alert\">Nhập số điện thoại</div>";
            }
            else if (string.IsNullOrEmpty(nd.MatKhauKH))
            {
                ViewBag.password = "<div class=\"alert alert-danger\" role=\"alert\">Nhập mật khẩu</div>";
            }
            else if (nd.MatKhauKH != input_re_password)
            {
                ViewBag.confirmPassword = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu không khớp</div>";
            }
            else if (string.IsNullOrEmpty(nd.DiaChi))
            {
                ViewBag.address = "<div class=\"alert alert-danger\" role=\"alert\">Nhập địa chỉ</div>";
            }
            else if (result == null)
            {
                try
                {
                    KHACHHANG n = new KHACHHANG
                    {
                        TenKH = nd.TenKH,
                        Email = nd.Email,
                        DienThoaiKH = nd.DienThoaiKH,
                        TaiKhoanKH = nd.TaiKhoanKH,
                        MatKhauKH = StringHash.crypto(nd.MatKhauKH),
                        GioiTinh = sex,
                        DiaChi = nd.DiaChi,
                        LoaiTK = role_id
                    };

                    _dbContext.KHACHHANGs.Add(n);
                    _dbContext.SaveChanges();
                    return RedirectToAction("DangNhap", "Auth", new { area = "" });
                }
                catch (Exception e)
                {
                    ViewBag.errorsignup1 = e.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản đã có người sử dụng!</div>";
            }

            return View();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult DangXuat()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult ChangePassword()
        {
            var session = Session["NguoiDung"];
            if (Session != null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AuthorizeAccount]
        public ActionResult ChangePassword(KHACHHANG nd)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var result = _dbContext.KHACHHANGs.SingleOrDefault(x => x.TaiKhoanKH == session.UserName);

            if(result != null)
            {
                string input_new_password = String.Format("{0}", Request.Form["new_password"]);
                string input_re_new_password = String.Format("{0}", Request.Form["re_new_password"]);

                if (string.IsNullOrEmpty(nd.MatKhauKH))
                {
                    ViewBag.oldPassword = "<div class=\"alert alert-danger\" role=\"alert\">Nhập mật khẩu cũ</div>";
                }
                else if (string.IsNullOrEmpty(input_new_password))
                {
                    ViewBag.newPassword = "<div class=\"alert alert-danger\" role=\"alert\">Nhập mật khẩu mới</div>";
                }
                else if (string.IsNullOrEmpty(input_re_new_password))
                {
                    ViewBag.reNewPassword = "<div class=\"alert alert-danger\" role=\"alert\">Nhập lại mật khẩu mới</div>";
                }
                else if (input_new_password != input_re_new_password)
                {
                    ViewBag.incorrectNewPassword = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu không khớp</div>";
                }
                else if (result.MatKhauKH != StringHash.crypto(nd.MatKhauKH))
                {
                    ViewBag.incorrectOldPassword = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu cũ không chính xác</div>";
                }
                else
                {
                    result.MatKhauKH = StringHash.crypto(input_new_password);
                    _dbContext.SaveChanges();

                    return RedirectToAction("DangXuat", "Auth", new { area = "" });
                }
            }
            else
            {
                ViewBag.incorrect = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản không tồn tại</div>";
            }

            return View();
        }
    }
}