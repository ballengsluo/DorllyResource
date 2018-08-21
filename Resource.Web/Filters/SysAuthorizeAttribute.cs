using Resource.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Web.Models;

namespace Resource.Web.Filters
{
    public class SysAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var action = filterContext.RouteData.Values["action"].ToString().ToLower();
            if (controller == "login" && (action == "index" || action == "login")) { return; }
            if (controller == "home") { return; }
            if (filterContext.HttpContext.Request.Cookies["resource"] == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            var tooken = Convert.ToString(filterContext.HttpContext.Request.Cookies["resource"].Value);
            if (string.IsNullOrEmpty(tooken))
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            DbContext dc = DbContextFactory.Create();
            tooken = Encrypt.DecryptDES(tooken, 2);
            var loginObj = dc.Set<T_LoginInfo>().Where(a => a.ID == tooken).FirstOrDefault();
            if (loginObj == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            var failTime = loginObj.FailTime;
            if (DateTime.UtcNow > failTime)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            filterContext.RouteData.Values["user"] = loginObj.T_User;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
            }
            else
            {
                filterContext.HttpContext.Response.Write("<script>window.top.location.href='/Login/Index';</script>");
                filterContext.HttpContext.Response.End();
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
            }

            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}