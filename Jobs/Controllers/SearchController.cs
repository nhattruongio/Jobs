using Jobs.Common;
using Jobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs.Controllers
{
    public class SearchController : Controller
    {
        private readonly JobDatabaseContext _dbContext;

        public SearchController()
        {
            _dbContext = new JobDatabaseContext();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Index(string key)
        {
            if (key != null)
            {
                bool flag = false;
                try
                {
                    _dbContext.Configuration.ProxyCreationEnabled = false;
                    var model = _dbContext.CONGVIECes.Where(x => x.MoTa.Contains(key));
                    flag = (model.Count() != 0) ? true : false;
                    return Json(new { data = model, status = flag }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e)
                {
                    return Json(new { data = e, status = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}