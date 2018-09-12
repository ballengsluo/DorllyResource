using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model;
using Resource.Web.Models;
using System.Data.Entity;
namespace Resource.Web.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginView lv)
        {
            if (!string.IsNullOrEmpty(lv.Name) && !string.IsNullOrEmpty(lv.Pwd))
            {
                
                var loginObj = dc.Set<T_LoginInfo>();
                var pwd = Encrypt.EncryptDES(lv.Pwd, 1);
                var user = dc.Set<T_User>().Where(a => a.Account == lv.Name && a.PWD == pwd && a.Enable == true).FirstOrDefault();
                if (user != null)
                {
                    var cookieName = "resource";
                    if (Response.Cookies.AllKeys.Contains(cookieName))
                    {
                        var cookieVal = Encrypt.DecryptDES(Response.Cookies[cookieName].Value, 2);
                        var cookieObj = loginObj.Where(a => a.ID == cookieVal).FirstOrDefault();
                        loginObj.Remove(cookieObj);
                        Response.Cookies.Remove(cookieName);
                    }
                    var token = Guid.NewGuid().ToString();
                    var newLoginObj = new T_LoginInfo { ID = token, UserID = user.Account, FailTime = DateTime.UtcNow.AddDays(1) };
                    loginObj.Add(newLoginObj);
                    dc.SaveChanges();
                    var hc = new HttpCookie(cookieName, Encrypt.EncryptDES(token, 2)) { HttpOnly = true };
                    Response.Cookies.Add(hc);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return Content("<script>alert('用户名或密码错误！');window.location='/login/index';</script>");
        }
        public ActionResult LogoOut()
        {
            
            var cookieName = "resource";
            if (Response.Cookies.AllKeys.Contains(cookieName))
            {
                Response.Cookies.Remove(cookieName);
            }
            T_User user = RouteData.Values["user"] as T_User;
            var loginList = user.T_LoginInfo.ToList();
            foreach (var item in loginList)
            {
                dc.Set<T_LoginInfo>().Remove(item);
            }
            dc.SaveChanges();
            return View("Index");
        }
    }
}