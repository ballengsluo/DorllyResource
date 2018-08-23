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
    public class R_RMController : ResourceController
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
            return View(new V_RM());
        }
        public ActionResult Edit(string id)
        {
            
            var obj = dc.Set<V_RM>().Where(a => a.ID==id).FirstOrDefault();
            ViewBag.price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == id).FirstOrDefault() ?? new T_ResourcePrice();
            ViewBag.img = dc.Set<T_ResourceImg>().Where(a => a.ResourceID == id).ToList();
            return View(obj);
        }
        public JsonResult Search(SearchParam param)
        {
            
            var list = dc.Set<V_RM>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Floor)) list = list.Where(a => a.ParentID == param.Floor);
            else if (!string.IsNullOrEmpty(param.Build)) list = list.Where(a => a.BuildingID == param.Build);
            else if (!string.IsNullOrEmpty(param.Stage)) list = list.Where(a => a.StageID == param.Stage);
            else if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(string pid)
        {            
            var list = dc.Set<T_Resource>().Where(a => a.ParentID == pid && a.ResourceKindID == 1);
            return Json(list.Select(a => new { a.ID }), JsonRequestBehavior.AllowGet);
        }
    }
}
