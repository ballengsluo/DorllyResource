using Newtonsoft.Json;
using Resource.Model.DB;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class StatisticsWPController : BaseController
    {
        // GET: StatisticsWP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchDay(string beginTime, string endTime)
        {
            try
            {
                if (string.IsNullOrEmpty(beginTime) || string.IsNullOrEmpty(endTime))
                {
                    beginTime = DateTime.Now.ToString("yyyy-MM-dd");
                    endTime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                List<SqlParameter> spList = new List<SqlParameter>
                 {
                     new SqlParameter("Model",1),
                     new SqlParameter("BeginTime",beginTime),
                     new SqlParameter("EndTime",endTime),
                     new SqlParameter("Account",user.Account)
                };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsWP", CommandType.StoredProcedure, spList.ToArray());
                var parkData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
                var obj = JsonConvert.SerializeObject(new { Flag = 1, Part1 = parkData });
                return Content(obj);
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(Result.Exception(exmsg:ex.StackTrace)));
            }
            
        }
    }
}