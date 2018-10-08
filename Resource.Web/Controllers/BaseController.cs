using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class BaseController : Controller
    {
        public DbContext dc
        {
            get
            {
                return DbContextFactory.Create();
            }
        }
        public T_User user
        {
            get
            {

                T_User user = RouteData.Values["user"] as T_User;
                ViewBag.user = user;
                return user;

            }
        }
        public List<string> ParkList
        {
            get
            {
                if (user.RoleID == 1)
                    return dc.Set<T_Park>().Select(a => a.ID).ToList();
                else
                    return user.T_UserData.Select(a => a.DataID).ToList();
            }
        }
        public string MenuPath
        {
            get
            {
                return string.Format("/{0}/{1}", ControllerContext.RouteData.GetRequiredString("Controller"), ControllerContext.RouteData.GetRequiredString("Action")).ToLower();
            }
        }


    }
}
