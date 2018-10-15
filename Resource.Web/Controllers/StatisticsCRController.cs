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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

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
                var data = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
                var obj = JsonConvert.SerializeObject(new { Flag = 1, result = data });
                return Content(obj);
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(Result.Exception(exmsg: ex.StackTrace)));
            }
        }

        public ActionResult SearchMonth(string park, string beginTime, string endTime)
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
                if (!string.IsNullOrEmpty(park)) result = result.Where(a => a.ParkID == park).ToList();
                var crList = result.GroupBy(a => new { a.ID, a.Name, a.ParkID, a.ParkName })
                    .OrderBy(a => a.Key.ID)
                    .Select(a => new { a.Key.ID, a.Key.Name, a.Key.ParkID, a.Key.ParkName })
                    .ToList();
                var monthList = result.GroupBy(a => a.MonthTime)
                    .OrderBy(a => a.Key)
                    .Select(a => a.Key)
                    .ToList();
                /*
                 * 图表
                 */
                var rentRateList = new List<object>();
                foreach (var item in crList)
                {
                    rentRateList.Add(new
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
                    legend = crList.Select(a => a.Name).ToList(),
                    xAxis = monthList,
                    series = rentRateList
                };
                /*
                 * 表格
                 */
                var rowList = new List<object>();
                foreach (var item in crList)
                {
                    rowList.Add(new
                    {
                        id = item.ID,
                        name = item.Name,
                        parkID = item.ParkID,
                        parkName = item.ParkName,
                        rentRate = result.Where(a => a.ID == item.ID)
                            .OrderBy(a => a.MonthTime)
                            .Select(a => a.RentRate)
                            .ToList()
                    });
                }
                var table = new
                {
                    title = monthList,
                    rows = rowList
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

        public FileResult ImportToExcel(string park, string stime, string etime, int model)
        {
            try
            {
                HSSFWorkbook book = new HSSFWorkbook();
                string fileName = string.Empty;
                if (model == 1)
                {
                    //汇总数据导出
                    fileName = "会议室出租率汇总报表-" + DateTime.Now.ToString("yyyyMMddHHmmsss");
                    if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
                    {
                        stime = DateTime.Now.ToString("yyyy-MM-dd");
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    List<SqlParameter> spList = new List<SqlParameter>
                    {
                        new SqlParameter("Model",1),
                        new SqlParameter("TimeSplit",8),
                        new SqlParameter("BeginTime",stime),
                        new SqlParameter("EndTime",etime),
                        new SqlParameter("Account",user.Account)
                    };
                    DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsCR", CommandType.StoredProcedure, spList.ToArray());
                    //工作簿
                    DataRow[] rows = string.IsNullOrEmpty(park) ? ds.Tables[0].Select() : ds.Tables[0].Select(string.Format("ParkID='{0}'", park));
                    ISheet sheet = book.CreateSheet("出租率汇总数据");
                    //条件列
                    IRow condition = sheet.CreateRow(0);
                    condition.CreateCell(0).SetCellValue("园区：");
                    condition.CreateCell(1).SetCellValue(string.IsNullOrEmpty(park) ? "全部" : rows[0]["ParkName"].ToString());
                    condition.CreateCell(2).SetCellValue("开始时间：");
                    condition.CreateCell(3).SetCellValue(stime);
                    condition.CreateCell(4).SetCellValue("结束时间：");
                    condition.CreateCell(5).SetCellValue(etime);
                    //第一行：标题行
                    IRow title = sheet.CreateRow(1);
                    title.CreateCell(0).SetCellValue("园区");
                    title.CreateCell(1).SetCellValue("会议室名称");
                    title.CreateCell(2).SetCellValue("总可用小时数");
                    title.CreateCell(3).SetCellValue("客户租赁小时数");
                    title.CreateCell(4).SetCellValue("内部使用小时数");
                    title.CreateCell(5).SetCellValue("空置小时数");
                    title.CreateCell(6).SetCellValue("出租率");
                    title.CreateCell(7).SetCellValue("内部使用率");
                    title.CreateCell(8).SetCellValue("空置率");
                    var rowIndex = 2;
                    //内容
                    foreach (var item in rows)
                    {
                        IRow row = sheet.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(item["ParkName"].ToString());
                        row.CreateCell(1).SetCellValue(item["Name"].ToString());
                        row.CreateCell(2).SetCellValue(item["Total"].ToString());
                        row.CreateCell(3).SetCellValue(item["Rent"].ToString());
                        row.CreateCell(4).SetCellValue(item["Self"].ToString());
                        row.CreateCell(5).SetCellValue(item["Free"].ToString());
                        row.CreateCell(6).SetCellValue(item["RentRate"].ToString() + "%");
                        row.CreateCell(7).SetCellValue(item["SelfRate"].ToString() + "%");
                        row.CreateCell(8).SetCellValue(item["FreeRate"].ToString() + "%");
                        rowIndex++;
                    }
                }
                else
                {
                    //趋势图数据导出
                    fileName = "会议室出租率趋势报表-" + DateTime.Now.ToString("yyyyMMddHHmmsss");
                    if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
                    {
                        stime = DateTime.Now.ToString("yyyy-MM") + "-01";
                        etime = DateTime.Now.ToString("yyyy-MM") + "-01";
                    }
                    else
                    {
                        stime = stime + "-01";
                        etime = etime + "-01";
                    }
                    List<SqlParameter> spList = new List<SqlParameter>
                    {
                        new SqlParameter("Model",2),
                        new SqlParameter("TimeSplit",8),
                        new SqlParameter("BeginTime",stime),
                        new SqlParameter("EndTime",etime),
                        new SqlParameter("Account",user.Account)
                    };
                    DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsCR", CommandType.StoredProcedure, spList.ToArray());
                    var result = JsonConvert.DeserializeObject<List<StatisticsMonthView>>(JsonConvert.SerializeObject(ds.Tables[0]));
                    if (!string.IsNullOrEmpty(park)) result = result.Where(a => a.ParkID == park).ToList();
                    //工作簿
                    ISheet sheet = book.CreateSheet("出租率趋势数据");
                    //第一行：标题行
                    var column = 2;
                    IRow monthTitle = sheet.CreateRow(0);
                    monthTitle.CreateCell(0).SetCellValue("园区");
                    monthTitle.CreateCell(1).SetCellValue("会议室");
                    var month = result.GroupBy(a => a.MonthTime).OrderBy(a => a.Key).Select(a => a.Key).ToList();
                    foreach (var item in month)
                    {
                        monthTitle.CreateCell(column).SetCellValue(item);
                        column++;
                    }
                    //内容
                    var row = 1;
                    var monthPark = result.GroupBy(a => new { a.ID, a.Name, a.ParkName }).Select(a => new { a.Key.ID, a.Key.Name, a.Key.ParkName }).ToList();
                    foreach (var item in monthPark)
                    {
                        IRow dataRow = sheet.CreateRow(row);
                        dataRow.CreateCell(0).SetCellValue(item.ParkName);
                        dataRow.CreateCell(1).SetCellValue(item.Name);
                        column = 2;
                        var monthData = result.Where(a => a.ID == item.ID).OrderBy(a => a.MonthTime).ToList();
                        foreach (var data in monthData)
                        {
                            dataRow.CreateCell(column).SetCellValue(data.RentRate + "%");
                            column++;
                        }
                        row++;
                    }
                }
                MemoryStream ms = new MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", fileName + ".xls");
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}