using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model;
using Resource.Web.Models;
using Resource.Model.DB;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
namespace Resource.Web.Controllers
{
    public class StatisticsAllController : BaseController
    {
        // GET: StatisticsAll
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string park, string beginTime, string endTime)
        {
            if (string.IsNullOrEmpty(beginTime) || string.IsNullOrEmpty(endTime))
            {
                beginTime = DateTime.Now.ToString("yyyy-MM-dd");
                endTime = DateTime.Now.ToString("yyyy-MM-dd");
            }
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
    }
}