using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class S_ADController : BaseController
    {
        // GET: S_AD
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            var list = dc.Set<V_SAD>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Cust)) list = list.Where(a => a.CustName.Contains(param.Cust));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupName.Contains(param.Group));
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}