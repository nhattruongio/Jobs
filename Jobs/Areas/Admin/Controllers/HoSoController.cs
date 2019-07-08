using Jobs.Common;
using Jobs.Models;
using Jobs.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Areas.Admin.Controllers
{
    public class HoSoController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public HoSoController()
        {
            _dbContext = new JobDatabaseContext();
        }
        [HttpGet]
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var model = (from a in _dbContext.HOSOs
                         join b in _dbContext.KHACHHANGs
                         on a.MaKH equals b.MaKH
                         select new HoSoViewModel
                         {
                             MaKH = b.MaKH,
                             MaHS = a.MaHS,
                             TenKH = b.TenKH,
                             NgayDang = a.NgayDang,
                             TinhTrang = a.TinhTrangXetTuyen
                         }).ToList();
            return View(model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Delete(int id)
        {
            var check = _dbContext.HOSOs.Where(x => x.MaHS == id).FirstOrDefault();

            if (check != null)
            {
                try
                {
                    _dbContext.HOSOs.Remove(check);
                    _dbContext.SaveChanges();
                    return Json(new { data = "Xoá thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { data = "Xoá không thành công" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Xoá thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}