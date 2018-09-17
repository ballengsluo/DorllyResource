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
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult GetResourceData(string park)
        {
            SQLHelper sq = SQLFactory.Create();
            DataSet ds = sq.GetDataSet("Pro_MainResourceReport", CommandType.StoredProcedure,
                new List<SqlParameter> { new SqlParameter("Park", park) }.ToArray());
            //ViewBag.AreaCount = ds.Tables[0].Compute("SUM(Area)", "true");
            //ViewBag.NumberCount = ds.Tables[0].Compute("SUM(Count)", "true");
            ViewBag.rm = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
            ViewBag.cb = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[1]));
            ViewBag.mr = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[2]));
            ViewBag.ad = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[3]));
           
            return PartialView("_Resource");
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