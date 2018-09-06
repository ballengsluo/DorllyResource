
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class RoleController : RSBaseController
    {

        // GET: Role
        public ActionResult Index()
        {
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_Role role)
        {
            try
            {

                role.Enable = true;
                dc.Set<T_Role>().Add(role);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                else return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }

        }
        public ActionResult Edit(int id)
        {

            T_Role role = dc.Set<T_Role>().AsNoTracking().Where(a => a.ID == id).FirstOrDefault();
            return View(role);
        }
        [HttpPost]
        public JsonResult Edit(int id, FormCollection form)
        {

            try
            {

                T_Role role = dc.Set<T_Role>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(role, "", form.AllKeys, new string[] { "Enable" }))
                {
                    if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                }
                return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }
        }
        [HttpPost]
        public JsonResult Del(int id)
        {
            try
            {

                T_Role role = dc.Set<T_Role>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Role>().Remove(role);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "删除成功！" });
                else return Json(new Result { Flag = 2, Msg = "删除失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "删除异常！", ExMsg = ex.StackTrace });
            }
        }
        public JsonResult Open(int id)
        {
            try
            {

                T_Role role = dc.Set<T_Role>().Where(a => a.ID == id).FirstOrDefault();
                role.Enable = true;
                dc.Set<T_Role>().AddOrUpdate(role);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "启用成功！" });
                else return Json(new Result { Flag = 2, Msg = "启用失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "启用异常！", ExMsg = ex.StackTrace });
            }
        }

        public JsonResult Close(int id)
        {
            try
            {

                T_Role role = dc.Set<T_Role>().Where(a => a.ID == id).FirstOrDefault();
                role.Enable = false;
                dc.Set<T_Role>().AddOrUpdate(role);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "停用成功！" });
                else return Json(new Result { Flag = 2, Msg = "停用失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "停用异常！", ExMsg = ex.StackTrace });
            }
        }
        public ActionResult Func(int id)
        {

            var role = dc.Set<T_Role>().Where(a => a.ID == id).FirstOrDefault();
            ViewBag.role = role;
            ViewBag.order = 1;
            var list = dc.Set<T_Menu>().ToList();
            return View(list);
        }
        [HttpPost]
        public JsonResult Func(int id, List<T_RoleMenu> rmList, List<T_RoleFunc> rmfList)
        {
            try
            {

                if (id == 0) return Json(ResponseResult.GetResult(ResultEnum.Fail));
                //
                var menuList = dc.Set<T_RoleMenu>().Where(a => a.RoleID == id);
                foreach (var item in menuList)
                {
                    dc.Set<T_RoleMenu>().Remove(item);
                }
                if (rmList != null)
                {
                    foreach (var item in rmList)
                    {
                        dc.Set<T_RoleMenu>().Add(item);
                    }
                }

                //
                var funcList = dc.Set<T_RoleFunc>().Where(a => a.RoleID == id);
                foreach (var item in funcList)
                {
                    dc.Set<T_RoleFunc>().Remove(item);
                }
                if (rmfList != null)
                {
                    foreach (var item in rmfList)
                    {
                        dc.Set<T_RoleFunc>().Add(item);
                    }
                }
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                else return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }
        }
        public JsonResult Search(SearchParam param)
        {

            var list = dc.Set<T_Role>().AsNoTracking().Where(a => true).Select(a => new { a.ID, a.Name, a.Description, a.Enable });
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }

    }
}