using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Areas.Admin.Controllers
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var session = Session["Admin"];
            if (Session != null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(ADMIN admin)
        {
            var result = _dbContext.ADMINs.SingleOrDefault(x => x.TaiKhoan == admin.TaiKhoan);
            if (string.IsNullOrEmpty(admin.TaiKhoan))
            {
                ViewBag.username = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập tài khoản!</div>";
            }
            else if (string.IsNullOrEmpty(admin.Matkhau))
            {
                ViewBag.password = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập mật khẩu!</div>";
            }
            else if (result == null)
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản không tồn tại!</div>";
            }
            else if (result.Matkhau != StringHash.crypto(admin.Matkhau))
            {
                ViewBag.confirmPassword = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu không đúng!</div>";
            }
            else
            {
                Session["Admin"] = result.TaiKhoan;
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View();
        }
    }
}