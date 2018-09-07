using Resource.Model;
using System;
using System.Linq;
using System.Web.Mvc;
using Resource.Web.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
namespace Resource.Web.Controllers
{
    public class R_CBController : ResourceController
    {
        public ActionResult Index()
        {
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }
        public ActionResult Create()
        {
            ViewBag.price = new T_ResourcePrice();
            return View(new V_RS_Info());
        }
        public ActionResult Edit(string id)
        {
            var obj = dc.Set<V_RS_Info>().Where(a => a.ID == id).FirstOrDefault();
            ViewBag.price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == id).FirstOrDefault() ?? new T_ResourcePrice();
            ViewBag.img = dc.Set<T_ResourceImg>().Where(a => a.ResourceID == id).ToList();
            return View(obj);
        }
        public JsonResult Search(SearchParam param)
        {
            var list = dc.Set<V_RS_Info>().Where(a => a.ResourceKindID == 2);
            if (!string.IsNullOrEmpty(param.Floor)) list = list.Where(a => a.Loc4 == param.Floor);
            else if (!string.IsNullOrEmpty(param.Build)) list = list.Where(a => a.Loc3 == param.Build);
            else if (!string.IsNullOrEmpty(param.Stage)) list = list.Where(a => a.Loc2 == param.Stage);
            else if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            if (!string.IsNullOrEmpty(param.Room)) list = list.Where(a => a.Loc5.Contains(param.Room));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var obj = list.Select(a => new
            {
                a.ID,
                a.Name,
                a.Loc5Name,
                a.LocText,
                a.ResourceTypeName,
                a.GroupName,
                a.Enable,
                a.Price
            }).ToList();
            return Json(new { count = count, data = obj }, JsonRequestBehavior.AllowGet);
        }
    }
}
