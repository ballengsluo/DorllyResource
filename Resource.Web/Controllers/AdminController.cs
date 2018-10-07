using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Resource.Web.Models;
using Resource.Model;
using Resource.Model.DB;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Resource.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            var menuList = new List<T_Menu>();
            List<T_RoleMenu> rmList = new List<T_RoleMenu>();
            if (user.RoleID == 1)
            {
                menuList = dc.Set<T_Menu>().ToList();
            }
            else
            {
                menuList = user.T_Role.T_RoleMenu.Select(a => a.T_Menu).ToList();
            }
            //foreach (var role in user.T_UserRole)
            //{
            //    foreach (var rm in role.T_Role.T_RoleMenu)
            //    {
            //        foreach (var item in rmList)
            //        {
            //            if (rm.MenuID == item.MenuID) continue;
            //        }
            //        rmList.Add(rm);
            //    }
            //}
            return View(menuList);
        }
        public ActionResult Main()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult GetResourceData(string park)
        {
            var beginTime = DateTime.Now.ToString("yyyy-MM-dd");
            var endTime = DateTime.Now.ToString("yyyy-MM-dd");
            List<SqlParameter> spList = new List<SqlParameter>
            {
                new SqlParameter("Park",park),
                new SqlParameter("BeginTime",beginTime),
                new SqlParameter("EndTime",endTime),
                new SqlParameter("Account",user.Account)
            };
            DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsAll", CommandType.StoredProcedure, spList.ToArray());
            var obj = JsonConvert.SerializeObject(ds.Tables[0]);
            return Content(obj);
        }
        public ActionResult GetOrderData(string stime, string etime)
        {
            var spList = new List<SqlParameter> { 
                new SqlParameter("BeginTime", stime), 
                new SqlParameter("EndTime", etime) 
            };
            DataSet ds = SQLFactory.Create().GetDataSet("Pro_OrderStatistics", CommandType.StoredProcedure, spList.ToArray());
            var obj = JsonConvert.SerializeObject(ds.Tables[0]);
            return Content(obj);
        }
        public JsonResult GetTransactionData()
        {
            var list = dc.Set<V_Public>().Where(a => a.Status == 1).Select(a => new
            {
                Title = "[资源发布待审核]",
                Msg = "[" + a.LocText + a.ResourceID + "]发布待审核",
                Url = "/public/index/?rid=" + a.ResourceID
            }).Take(20).ToList();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}