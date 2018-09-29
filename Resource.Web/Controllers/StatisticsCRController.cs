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
    public class StatisticsCRController : BaseController
    {
        // GET: StatisticsCR
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
                List<SqlParameter> spList = new List<SqlParameter>{
                    new SqlParameter("Model",1),
                    new SqlParameter("TimeSplit",8),
                    new SqlParameter("BeginTime",beginTime),
                    new SqlParameter("EndTime",endTime),
                    new SqlParameter("Account",user.Account)
                };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsCR", CommandType.StoredProcedure, spList.ToArray());
                var parkData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
                var obj = JsonConvert.SerializeObject(new { Flag = 1, Part1 = parkData });
                return Content(obj);
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(Result.Exception(exmsg:ex.StackTrace)));
            }
        }

        public ActionResult SearchMonth(string beginTime, string endTime)
        {
            try
            {
                if (string.IsNullOrEmpty(beginTime) || string.IsNullOrEmpty(endTime))
                {
                    beginTime = DateTime.Now.ToString("yyyy-MM") + "-01";
                    endTime = DateTime.Now.ToString("yyyy-MM") + "-01";
                }
                else
                {
                    beginTime = beginTime + "-01";
                    endTime = endTime + "-01";
                }
                List<SqlParameter> spList = new List<SqlParameter>
            {
                new SqlParameter("Model",2),
                 new SqlParameter("TimeSplit",8),
                new SqlParameter("BeginTime",beginTime),
                new SqlParameter("EndTime",endTime),
                new SqlParameter("Account",user.Account)
            };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsCR", CommandType.StoredProcedure, spList.ToArray());
                var result = JsonConvert.DeserializeObject<List<StatisticsMonthView>>(JsonConvert.SerializeObject(ds.Tables[0]));
                /*
                 * 园区数据组装:为图表服务
                 */
                var parkList = result.GroupBy(a => new { ParkID = a.ParkID, a.ParkName })
                    .OrderBy(a => a.Key.ParkID)
                    .Select(a => new { a.Key.ParkID, a.Key.ParkName })
                    .ToList();
                List<object> parkDataList = new List<object>();
                foreach (var item in parkList)
                {
                    parkDataList.Add(new
                    {
                        parkName = item.ParkName,
                        parkData = result.Where(a => a.ParkID == item.ParkID)
                        .OrderBy(a => a.MonthTime)
                        .Select(a => a.RentRate)
                        .ToList()
                    });
                }
                /*
                 * 月份数据组装:为表格服务
                 */
                List<string> monthList = result.GroupBy(a => a.MonthTime)
                    .OrderBy(a => a.Key)
                    .Select(a => a.Key)
                    .ToList();
                List<object> monthDataList = new List<object>();
                foreach (var item in monthList)
                {
                    monthDataList.Add(new
                    {
                        monthName = item,
                        monthData = result.Where(a => a.MonthTime == item)
                            .OrderBy(a => a.ParkID)
                            .Select(a => a.RentRate)
                            .ToList()
                    });
                }
                var obj = JsonConvert.SerializeObject(new
                {
                    Flag = 1,
                    park = parkList.Select(a => a.ParkName).ToList(),
                    parkData = parkDataList,
                    month = monthList,
                    monthData = monthDataList
                });
                return Content(obj);
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace)));
            }
        }
    }
}