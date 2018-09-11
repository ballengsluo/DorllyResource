using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model.DB;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
namespace Resource.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            List<T_RoleMenu> rmList = new List<T_RoleMenu>();
            foreach (var role in user.T_UserRole)
            {
                foreach (var rm in role.T_Role.T_RoleMenu)
                {
                    foreach (var item in rmList)
                    {
                        if (rm.MenuID == item.MenuID) continue;
                    }
                    rmList.Add(rm);
                }
            }
            return View(rmList);
        }
        public ActionResult Main()
        {
            //var fd = user.T_UserRole.Where(a=>a.T_Role.T_RoleFunc.Where(b=>b.me))
            //var roleList = dc.Set<T_UserRole>().Where(a=>a.UserID==user.Account).ToList();
            //ViewBag.Identity=dc.Set<T_RoleFunc>().Where(a=>a.)
            return View();
        }
        public ActionResult GetBusinessData(string park)
        {
            SQLHelper sq = SQLFactory.Create();
            DataSet ds = sq.GetDataSet("Pro_MainResourceCount",
                CommandType.StoredProcedure,
                new List<SqlParameter> { new SqlParameter("Park", park) }.ToArray());
            ViewBag.AreaCount = ds.Tables[0].Compute("SUM(Area)", "true");
            ViewBag.NumberCount = ds.Tables[0].Compute("SUM(Count)", "true");
            ViewBag.rm = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
            ViewBag.cb = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[1]));
            ViewBag.mr = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[2]));
            ViewBag.ad = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[3]));
            return PartialView("_BusinessData");
        }
        public ActionResult GetOrderData(string stime, string etime)
        {
            List<SqlParameter> spList = new List<SqlParameter> { 
                new SqlParameter("BeginTime", stime), 
                new SqlParameter("EndTime", etime) 
            };
            SQLHelper sq = SQLFactory.Create();
            DataSet ds = sq.GetDataSet("Pro_OrderStatistics", CommandType.StoredProcedure, spList.ToArray());
            var obj = JsonConvert.SerializeObject(ds.Tables[0]);
            return Content(obj);
            //ViewBag.OrderData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
            //return PartialView("_OrderData");
        }
    }
}