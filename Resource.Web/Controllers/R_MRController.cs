using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class R_MRController : ResourceController
    {
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.price = new T_ResourcePrice();
            return View(new V_Resource());
        }
        public ActionResult Edit(string id)
        {
            var obj = dc.Set<V_Resource>().Where(a => a.ID == id).FirstOrDefault();
            ViewBag.price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == id).FirstOrDefault() ?? new T_ResourcePrice();
            ViewBag.img = dc.Set<T_ResourceImg>().Where(a => a.ResourceID == id).ToList();
            return View(obj);
        }
        public JsonResult Search(SearchParam param)
        {
            var list = dc.Set<V_Resource>().Where(a => a.ResourceKindID == 3 && ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var obj = list.Select(a => new
            {
                a.ID,
                a.Name,
                a.Loc1Name,
                a.ResourceTypeName,
                a.GroupName,
                a.Enable,
                a.RangeNum
            }).ToList();
            return Json(new { count = count, data = obj }, JsonRequestBehavior.AllowGet);
        }
    }
}
