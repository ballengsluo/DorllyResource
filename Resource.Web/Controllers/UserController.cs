
using Newtonsoft.Json;
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
    public class UserController : BaseController
    {

        // GET: user
        public ActionResult Index()
        {
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }


        public ActionResult Create()
        {
            var parklist = user.Park.Split(',');
            var obj = user.Account == "admin" ?
                dc.Set<T_Park>().Select(a => new { a.ID, a.Name }).ToList() :
                dc.Set<T_Park>().Where(a => parklist.Contains(a.ID)).Select(a => new { a.ID, a.Name }).ToList();
            ViewBag.park = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
            return View(user);
        }
        [HttpPost]
        public JsonResult Create(T_User user)
        {
            try
            {

                if (string.IsNullOrEmpty(user.PWD)) user.PWD = Encrypt.EncryptDES("888888", 1);
                else user.PWD = Encrypt.EncryptDES(user.PWD, 1);
                user.Enable = true;
                user.CreateDate = DateTime.Now;
                dc.Set<T_User>().Add(user);

                if (!string.IsNullOrEmpty(Request.Form["InitRole"]) && Request.Form["InitRole"] == "1")
                {

                    T_UserRole ur = new T_UserRole();
                    ur.RoleID = 1;
                    ur.UserID = user.Account;
                    dc.Set<T_UserRole>().Add(ur);
                }

                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }

        }
        public ActionResult Edit(string id)
        {

            //ViewBag.park = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dc.Set<T_Park>().Select(a => new { a.ID, a.Name }).ToList()));
            var parklist = user.Park.Split(',');
            var obj = user.Account == "admin" ?
                dc.Set<T_Park>().Select(a => new { a.ID, a.Name }).ToList() :
                dc.Set<T_Park>().Where(a => parklist.Contains(a.ID)).Select(a => new { a.ID, a.Name }).ToList();
            ViewBag.park = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
            var userObj = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
            return View(userObj);
        }
        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {

            try
            {

                T_User user = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                if (TryUpdateModel(user, "", form.AllKeys, new string[] { "Enable", "PWD", "CreateDate" }))
                {
                    if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                    else return Json(ResponseResult.GetResult(ResultEnum.Fail));
                }
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        public ActionResult Reset(string id)
        {

            T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
            return View(user);
        }
        public ActionResult SingleReset()
        {

            return View(user);
        }
        [HttpPost]
        public ActionResult SingleReset(string id, FormCollection form)
        {

            try
            {
                T_User user = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                user.PWD = Encrypt.EncryptDES(form["PWD"], 1);
                var list = dc.Set<T_LoginInfo>().Where(a => a.UserID == user.Account).ToList();
                foreach (var item in list)
                {
                    dc.Set<T_LoginInfo>().Remove(item);
                }
                return Service(dc);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        [HttpPost]
        public JsonResult Reset(string id, FormCollection form)
        {

            try
            {
                T_User user = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                user.PWD = Encrypt.EncryptDES(form["PWD"], 1);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        public ActionResult Role(string id)
        {

            ViewBag.Role = dc.Set<T_Role>().ToList();
            T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public JsonResult Role(string id, List<T_UserRole> urList)
        {

            try
            {

                var _urList = dc.Set<T_UserRole>().Where(a => a.UserID.Equals(id));
                foreach (var item in _urList)
                {
                    dc.Set<T_UserRole>().Remove(item);
                }
                foreach (var item in urList)
                {
                    dc.Set<T_UserRole>().Add(item);
                }
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        [HttpPost]
        public JsonResult Del(string id)
        {
            try
            {

                T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                dc.Set<T_User>().Remove(user);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        public JsonResult Open(string id)
        {
            try
            {

                T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                user.Enable = true;
                dc.Set<T_User>().AddOrUpdate(user);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        public JsonResult Close(string id)
        {
            try
            {

                T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                user.Enable = false;
                dc.Set<T_User>().AddOrUpdate(user);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        public ContentResult Search(SearchParam param)
        {

            var list = dc.Set<T_User>().Where(a => true);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.Account.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.UserName.Contains(param.Name));
            if (param.BeginTime != null) list = list.Where(a => a.CreateDate >= param.BeginTime);
            if (param.EndTime != null) list = list.Where(a => a.CreateDate <= param.EndTime);
            if (param.Enable != null) list = list.Where(a => a.Enable == param.Enable);
            int count = list.Count();
            list = list.OrderBy(a => a.Account).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:sss"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = list.ToList() }, setting);
            return Content(obj);
        }

    }
}