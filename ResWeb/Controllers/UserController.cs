using Newtonsoft.Json;
using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{

    public class UserController : Controller
    {
        private IUserService us = Container.Resolve<IUserService>();
        private IRoleService rs = Container.Resolve<IRoleService>();

        public void GetUserList()
        {
            var userList = from u in us.GetModels(u => true)
                           join r in rs.GetModels(r => true) on u.RoleID equals r.ID
                           select new
                           {
                               u.UserName,
                               u.Password,
                               u.Phone,
                               u.Addr,
                               u.CreateDate,
                               u.Email,
                               u.Status
                           };

            List<dynamic> dyList = new List<dynamic>();
            foreach (var item in userList.ToList())
            {
                dynamic dyUser = new ExpandoObject();

                dyUser.password = item.Password;
                dyUser.createDate = item.CreateDate;
                dyUser.addr = item.Addr;
                dyUser.email = item.Email;
                dyUser.status = item.Status;
                dyUser.phone = item.Phone;
                dyList.Add(dyUser);
            }
            ViewBag.userList = dyList;
            //var uList = us.GetModels(u => true);
            //var rList = rs.GetModels(r => true);
            //var gList = uList.Join(rList, u => u.RoleID, r => r.ID, (u, r) => new { u.ID }).Select(o => o).DefaultIfEmpty().ToList();
            //var gList = from u in uList
            //            join r in rList on u.RoleID equals r.ID into user_role
            //            from ur in user_role.DefaultIfEmpty()
            //            select new
            //            {
            //                ID = u.ID,
            //                loginName = u.LoginName ?? string.Empty,
            //                userName = u.UserName ?? string.Empty,
            //                phone = u.Phone ?? string.Empty,
            //                addr = u.Addr ?? string.Empty,
            //                email = u.Email ?? string.Empty,
            //                roleID = u.RoleID == null ? 0 : u.RoleID,
            //                roleName = ur.Name ?? "",
            //                createDate = u.CreateDate ?? DateTime.MinValue,
            //                status = u.Status ?? false
            //            };
            //var listJosonObeject = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(gList)); 
        }
        // GET: User
        public ActionResult Index()
        {
            var userList = us.GetModels(u => true).ToList();
            return View(userList);
        }

        // GET: User/Details/5
        public ActionResult Details()
        {
            var userList = us.GetModels(u => true).ToList();
            return PartialView("_UserTable", userList);
            //return View();
        }
        public ActionResult Select(string userName, string createDate)
        {
             var userList = us.GetModels(u => true);
            if (!string.IsNullOrEmpty(userName))
                userList = userList.Where(u => u.UserName.Contains(userName));
            if (!string.IsNullOrEmpty(createDate))
            {
                var time = createDate.Trim().Split('~');
                DateTime begin = Convert.ToDateTime(time[0]);
                DateTime end = Convert.ToDateTime(time[1]);
                userList = userList.Where(u => u.CreateDate >= begin && u.CreateDate <= end);
            }
            return PartialView("_UserTable", userList.ToList());
        }

        public ActionResult CloseOrOpenUser(string id, string type)
        {
            int userId = Convert.ToInt32(id);
            var user = us.GetModels(u => u.ID == userId).FirstOrDefault();
            if (type == "open")
                user.Status = true;
            else
                user.Status = false;
            us.Update(user);
            var userList = us.GetModels(u => true).ToList();
            return PartialView("_UserTable", userList);

        }

        public ContentResult ResetPwd(int id, string pwd)
        {
            try
            {
                T_User user = us.GetModels(u => u.ID == id).FirstOrDefault();
                user.Password = pwd;
                if (us.Update(user))
                    return Content("1:更改密码成功！");
                else
                    return Content("5:更改密码失败！");
            }
            catch (Exception)
            {
                return Content("2:后台程序错误，请联系产品提供者！");
            }
        }

        // GET: User/Create
        public ActionResult Create()
        {
            List<T_Role> rList = rs.GetModels(r => true).ToList();
            ViewData["rList"] = new SelectList(rList, "RoleID", "RoleName");
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ContentResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                T_User user = new T_User();
                user.UserName = collection["userName"];
                user.Password = collection["pwd"];
                user.Phone = collection["phone"];
                user.Email = collection["email"];
                user.Addr = collection["addr"];
                user.RoleID = Convert.ToInt32(collection["role"]);
                user.CreateDate = DateTime.Now;
                user.Status = true;
                if (us.Add(user))
                    return Content("1:注册成功");
                else
                    return Content("5:注册失败");
            }
            catch (Exception ex)
            {
                return Content("2:后台程序错误，请联系产品提供者");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            T_User user = us.GetModels(u => u.ID == id).FirstOrDefault();
            List<T_Role> rList = rs.GetModels(r => true).ToList();
            ViewData["rList"] = new SelectList(rList, "RoleID", "RoleName");
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ContentResult Edit(FormCollection collection)
        {
            try
            {
                int userId = Convert.ToInt32(collection["userID"]);
                T_User user = us.GetModels(u => u.ID == userId).FirstOrDefault();
                user.UserName = collection["userName"];
                user.Password = collection["pwd"];
                user.Phone = collection["phone"];
                user.Email = collection["email"];
                user.Addr = collection["addr"];
                user.RoleID = Convert.ToInt32(collection["role"]);
                if (us.Update(user))
                    return Content("1:用户更新成功！");
                else
                    return Content("5:用户更新失败！");
            }
            catch
            {
                return Content("2:后台程序错误，请联系产品提供者！");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(id);
            var user = us.GetModels(u => u.ID == userId).FirstOrDefault();
            us.Delete(user);
            var userList = us.GetModels(u => true).ToList();
            return PartialView("_UserTable", userList);
        }

        public ActionResult Create1()
        {
            List<T_Role> rList = rs.GetModels(r => true).ToList();
            ViewData["rList"] = new SelectList(rList, "RoleID", "RoleName");
            return View();
        }

    }
}
