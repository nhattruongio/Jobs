using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public HomeController()
        {
            _dbContext = new JobDatabaseContext();
        }
        [HttpGet]
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            string Tk = Session["Admin"].ToString();
            var model = _dbContext.ADMINs.SingleOrDefault(x => x.TaiKhoan == Tk);

            ViewBag.NguoiXinViec = _dbContext.KHACHHANGs.Where(x => x.LoaiTK == 1).Count();
            ViewBag.NhaTuyenDung = _dbContext.KHACHHANGs.Where(x => x.LoaiTK == 2).Count();
            ViewBag.CongViec = _dbContext.CONGVIECes.Count();
            ViewBag.HoSo = _dbContext.HOSOs.Count();

            return View(model);
        }
    }
}