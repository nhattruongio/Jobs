using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Areas.Admin.Controllers
{
    public class CongViecController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public CongViecController()
        {
            _dbContext = new JobDatabaseContext();
        }

        [HttpGet]
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var model = _dbContext.CONGVIECes.ToList();
            return View(model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Delete(int id)
        {
            var check = _dbContext.CONGVIECes.Where(x => x.MaCV == id).FirstOrDefault();

            if (check != null)
            {
                try
                {
                    _dbContext.CONGVIECes.Remove(check);
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