using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Areas.Admin.Controllers
{
    public class NguoiXinViecController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public NguoiXinViecController()
        {
            _dbContext = new JobDatabaseContext();
        }

        [HttpGet]
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var model = _dbContext.KHACHHANGs.Where(x => x.LoaiTK == 1).ToList();
            return View(model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Delete(int id)
        {
            var check = _dbContext.KHACHHANGs.Where(x => x.MaKH == id).FirstOrDefault();

            if(check != null)
            {
                try
                {
                    _dbContext.KHACHHANGs.Remove(check);
                    _dbContext.SaveChanges();
                    return Json(new { data = "Xoá thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e)
                {
                    return Json(new { data = "Xoá không thành công, vui lòng xoá hồ sơ trước" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "Xoá thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}