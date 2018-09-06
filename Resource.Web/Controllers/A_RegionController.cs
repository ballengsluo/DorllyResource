using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class A_RegionController : Controller
    {
        // GET: Region
        public ActionResult Index()
        {
            T_User user = RouteData.Values["user"] as T_User;
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }
        public JsonResult Search(SearchParam param)
        {
            DbContext dc = DbContextFactory.Create();
            var list = from a in dc.Set<T_Region>()
                       join b in dc.Set<T_City>() on a.CityID equals b.ID into t1
                       from city in t1.DefaultIfEmpty()
                       select new { a.ID, a.CityID, a.Name, a.Enable, CityName = city.Name };
            if (!string.IsNullOrEmpty(param.City)) list = list.Where(a => a.CityID == param.City);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(T_Region region)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                dc.Set<T_Region>().Add(region);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                else return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }
        }

        public ActionResult Edit(string id)
        {
            DbContext dc = DbContextFactory.Create();
            var obj = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(region, "", form.AllKeys, new string[] { "Enable" }))
                {
                    if (dc.SaveChanges() > 0) Json(new Result { Flag = 1, Msg = "保存成功！" });
                }
                return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }

        }

        [HttpPost]
        public JsonResult Del(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Region>().Remove(region);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "删除成功！" });
                else return Json(new Result { Flag = 2, Msg = "删除失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "删除异常！", ExMsg = ex.StackTrace });
            }
        }
        [HttpPost]
        public JsonResult Open(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = true;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "启用成功！" });
                else return Json(new Result { Flag = 2, Msg = "启用失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "启用异常！", ExMsg = ex.StackTrace });
            }
        }
        [HttpPost]
        public JsonResult Close(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = false;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "停用成功！" });
                else return Json(new Result { Flag = 2, Msg = "停用失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "停用异常！", ExMsg = ex.StackTrace });
            }
        }
        

        public JsonResult GetList(string pid)
        {
            DbContext dc = DbContextFactory.Create();
            var list = dc.Set<T_Region>().Where(a => true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.CityID == pid);
            return Json(list.Select(a => new { a.ID, a.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}
