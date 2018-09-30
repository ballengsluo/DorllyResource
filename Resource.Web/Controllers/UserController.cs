
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
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ContentResult Search(SearchParam param)
        {
            var list = dc.Set<T_User>()
                .AsNoTracking()
                .Where(a => true)
                .Select(a => new { a.Account, a.UserName, a.CreateDate, a.Addr, a.Enable, a.Email, a.Phone });
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.Account.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.UserName.Contains(param.Name));
            if (param.Stime != null) list = list.Where(a => a.CreateDate >= param.Stime);
            if (param.Etime != null) list = list.Where(a => a.CreateDate <= param.Etime);
            if (param.Enable != null) list = list.Where(a => a.Enable == param.Enable);
            int count = list.Count();
            list = list.OrderBy(a => a.Account)
                .Skip((param.PageIndex - 1) * param.PageSize)
                .Take(param.PageSize);
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = list.ToList() }, setting);
            return Content(obj);
        }
        public ActionResult Create()
        {
            SetPark(null);
            return View();
        }
        [HttpPost]
        public JsonResult Create(FormCollection form)
        {
            try
            {
                //数据转换对象
                T_User newUser = new T_User();
                if (!TryUpdateModel(newUser, "", form.AllKeys))
                    return Json(Result.Fail());
                //密码加密
                if (string.IsNullOrEmpty(newUser.PWD)) newUser.PWD = Encrypt.EncryptDES("888888", 1);
                else newUser.PWD = Encrypt.EncryptDES(newUser.PWD, 1);
                //其余数据补充
                newUser.Enable = true;
                newUser.CreateTime = DateTime.Now;
                newUser.CreateUser = user.Account;
                newUser.UpdateTime = DateTime.Now;
                newUser.UpdateUser = user.Account;
                dc.Set<T_User>().Add(newUser);
                //绑定数据权限
                foreach (var item in form["Park"].Split(','))
                {
                    T_UserData ud = new T_UserData();
                    ud.UserID = newUser.Account;
                    ud.DataID = item;
                    dc.Set<T_UserData>().Add(ud);
                }
                //绑定角色权限
                T_UserRole role = new T_UserRole();
                role.UserID = newUser.Account;
                if (!string.IsNullOrEmpty(form["Role"]) && form["Role"] == "1")
                    role.RoleID = 1;
                else
                    role.RoleID = 2;
                dc.Set<T_UserRole>().Add(role);
                dc.SaveChanges();
                return Json(Result.Success());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Edit(string id)
        {
            var user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
            SetPark(user);
            return View(user);
        }
        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                T_User updateUser = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                if (updateUser == null) return Json(Result.Fail(msg: "系统无此用户，更新失败！"));
                if (!TryUpdateModel(updateUser, "", form.AllKeys, new string[] { "Enable", "PWD", "CreateTime", "CreateUser" }))
                    return Json(Result.Fail());
                updateUser.UpdateTime = DateTime.Now;
                updateUser.UpdateUser = user.Account;
                //绑定数据权限
                dc.Database.ExecuteSqlCommand(string.Format("delete from t_userdata where userid='{0}'", updateUser.Account));
                foreach (var item in form["Park"].Split(','))
                {
                    T_UserData ud = new T_UserData();
                    ud.UserID = updateUser.Account;
                    ud.DataID = item;
                    dc.Set<T_UserData>().Add(ud);
                }
                dc.SaveChanges();
                return Json(Result.Success());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
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
                T_User updateUser = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                updateUser.PWD = Encrypt.EncryptDES(form["PWD"], 1);
                updateUser.UpdateTime = DateTime.Now;
                updateUser.UpdateUser = user.Account;
                var list = dc.Set<T_LoginInfo>().Where(a => a.UserID == updateUser.Account).ToList();
                foreach (var item in list)
                {
                    dc.Set<T_LoginInfo>().Remove(item);
                }
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }

        [HttpPost]
        public JsonResult Reset(string id, FormCollection form)
        {
            try
            {
                T_User updateUser = dc.Set<T_User>().Where(a => a.Account.Equals(id)).FirstOrDefault();
                updateUser.PWD = Encrypt.EncryptDES(form["PWD"], 1);
                updateUser.UpdateTime = DateTime.Now;
                updateUser.UpdateUser = user.Account;
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
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
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public JsonResult Del(string id)
        {
            try
            {
                T_User user = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                var ur = user.T_UserRole.ToList();
                foreach (var item in ur)
                {
                    dc.Set<T_UserRole>().Remove(item);
                }
                var li = user.T_LoginInfo.ToList();
                foreach (var item in li)
                {
                    dc.Set<T_LoginInfo>().Remove(item);
                }
                dc.Set<T_User>().Remove(user);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public JsonResult Open(string id)
        {
            try
            {
                T_User updateUser = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                updateUser.Enable = true;
                updateUser.UpdateTime = DateTime.Now;
                updateUser.UpdateUser = user.Account;
                dc.Set<T_User>().AddOrUpdate(updateUser);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public JsonResult Close(string id)
        {
            try
            {
                T_User updateUser = dc.Set<T_User>().Where(a => a.Account == id).FirstOrDefault();
                updateUser.Enable = false;
                updateUser.UpdateTime = DateTime.Now;
                updateUser.UpdateUser = user.Account;
                dc.Set<T_User>().AddOrUpdate(updateUser);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }

        public void SetPark(T_User cust)
        {
            List<T_UserData> custPark = null;
            if (cust != null)
                custPark = cust.T_UserData.ToList();
            if (custPark != null && custPark.Count() > 0)
            {
                var mastPark = user.T_UserData
                .Select(a => new
                {
                    a.DataID,
                    Enable = custPark.Where(b => b.DataID == a.DataID).Count() > 0
                })
                .Join(dc.Set<T_Park>(), a => a.DataID, b => b.ID, (a, b) => new
                {
                    ID = a.DataID,
                    Name = b.Name,
                    Enable = a.Enable
                }).ToList();
                ViewBag.Park = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(mastPark));
            }
            else
            {
                var mastPark = user.T_UserData
                 .Select(a => new
                 {
                     a.DataID,
                     Enable = false
                 })
                 .Join(dc.Set<T_Park>(), a => a.DataID, b => b.ID, (a, b) => new
                 {
                     ID = a.DataID,
                     Name = b.Name,
                     Enable = a.Enable
                 }).ToList();
                ViewBag.Park = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(mastPark));
            }

        }
    }
}