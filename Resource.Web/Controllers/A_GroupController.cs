using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.Model;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Resource.Web.Models;

namespace Resource.Web.Controllers
{

    public class A_GroupController : Controller
    {
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
            var list = from a in dc.Set<T_ResourceGroup>()
                       join b in dc.Set<T_Park>() on a.ParkID equals b.ID into t1
                       from park in t1.DefaultIfEmpty()
                       join c in dc.Set<T_ResourceKind>() on a.ResourceKindID equals c.ID into t2
                       from type in t2.DefaultIfEmpty()
                       select new { a.ID, a.Name, a.ParkID, a.ResourceKindID, a.Enable, ParkName = park.Name, ResourceKindName = type.Name };
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.Enable != null) list = list.Where(a => a.Enable == param.Enable);
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(T_ResourceGroup group)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_User user = RouteData.Values["user"] as T_User;
                group.CreateTime = DateTime.Now;
                group.CreateUser = user.Account;
                group.UpdateTime = DateTime.Now;
                group.UpdateUser = user.Account;
                group.Enable = true;
                dc.Set<T_ResourceGroup>().Add(group);
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
            var group = dc.Set<T_ResourceGroup>().AsNoTracking().Where(a => a.ID == id).FirstOrDefault();
            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                T_User user = RouteData.Values["user"] as T_User;
                DbContext dc = DbContextFactory.Create();
                T_ResourceGroup group = dc.Set<T_ResourceGroup>().Where(a => a.ID == id).FirstOrDefault();
                group.UpdateTime = DateTime.Now;
                group.UpdateUser = user.Account;
                if (TryUpdateModel(group, "", form.AllKeys, new string[] { "Enable"}))
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
                T_ResourceGroup group = dc.Set<T_ResourceGroup>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_ResourceGroup>().Remove(group);
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
                T_User user = RouteData.Values["user"] as T_User;
                DbContext dc = DbContextFactory.Create();
                T_ResourceGroup group = dc.Set<T_ResourceGroup>().Where(a => a.ID == id).FirstOrDefault();
                group.Enable = true;
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
                T_User user = RouteData.Values["user"] as T_User;
                DbContext dc = DbContextFactory.Create();
                T_ResourceGroup group = dc.Set<T_ResourceGroup>().Where(a => a.ID == id).FirstOrDefault();
                group.Enable = false;
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "停用成功！" });
                else return Json(new Result { Flag = 2, Msg = "停用失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "停用异常！", ExMsg = ex.StackTrace });
            }
        }

        

    }
}