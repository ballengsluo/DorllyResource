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
                return Content(JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace)));
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

                var titleList = result.GroupBy(a => new { a.ID, a.Name, a.ParkName })
                    .OrderBy(a => a.Key.ID)
                    .Select(a => new { a.Key.ID, a.Key.Name, a.Key.ParkName })
                    .ToList();
                var monthList = result.GroupBy(a => a.MonthTime)
                    .OrderBy(a => a.Key)
                    .Select(a => a.Key)
                    .ToList();
                /*
                 * 图表
                 */                
                var seriesData = new List<object>();
                foreach (var item in titleList)
                {
                    seriesData.Add(new
                    {
                        name = item.Name,
                        data = result.Where(a => a.ID == item.ID)
                        .OrderBy(a => a.MonthTime)
                        .Select(a => a.RentRate)
                        .ToList()
                    });
                }
                var graph = new
                {
                    legend = titleList.Select(a => a.Name).ToList(),
                    xAxis = monthList,
                    series = seriesData
                };
                /*
                 * 表格
                 */
                var rowsData = new List<object>();
                foreach (var item in titleList)
                {
                    rowsData.Add(new
                    {
                        name = item.Name,
                        parkName= item.ParkName,
                        data = result.Where(a => a.ID==item.ID)
                            .OrderBy(a => a.MonthTime)
                            .Select(a => a.RentRate)
                            .ToList()
                    });
                }
                var table = new
                {
                    title = monthList,
                    rows = rowsData
                };
                var obj = JsonConvert.SerializeObject(new
                {
                    Flag = 1,
                    graph = graph,
                    table = table
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