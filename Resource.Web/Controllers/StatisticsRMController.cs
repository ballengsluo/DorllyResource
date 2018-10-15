using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Resource.Model.DB;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class StatisticsRMController : BaseController
    {
        // GET: StatisticsRM
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
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsRM", CommandType.StoredProcedure, spList.ToArray());
                var parkData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
                var buildData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[1]));
                var obj = JsonConvert.SerializeObject(new { Flag = 1, Part1 = parkData, Part2 = buildData });
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
                new SqlParameter("BeginTime",beginTime),
                new SqlParameter("EndTime",endTime),
                new SqlParameter("Account",user.Account)
            };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsRM", CommandType.StoredProcedure, spList.ToArray());
                var result = JsonConvert.DeserializeObject<List<StatisticsMonthView>>(JsonConvert.SerializeObject(ds.Tables[0]));
                /*
                  * 园区数据组装:为图表服务
                  */
                var titleList = result.GroupBy(a => new { a.ParkID, a.ParkName })
                    .OrderBy(a => a.Key.ParkID)
                    .Select(a => new { a.Key.ParkID, a.Key.ParkName })
                    .ToList();
                var monthList = result.GroupBy(a => a.MonthTime)
                    .OrderBy(a => a.Key)
                    .Select(a => a.Key)
                    .ToList();
                var seriesData = new List<object>();
                foreach (var item in titleList)
                {
                    seriesData.Add(new
                    {
                        name = item.ParkName,
                        data = result.Where(a => a.ParkID == item.ParkID)
                        .OrderBy(a => a.MonthTime)
                        .Select(a => a.RentRate)
                        .ToList()
                    });
                }
                var graph = new
                {
                    legend = titleList.Select(a => a.ParkName).ToList(),
                    xAxis = monthList,
                    series = seriesData
                };
                /*
                 * 月份数据组装:为表格服务
                 */
                var rowsData = new List<object>();
                foreach (var item in monthList)
                {
                    rowsData.Add(new
                    {
                        name = item,
                        data = result.Where(a => a.MonthTime == item)
                            .OrderBy(a => a.ID)
                            .Select(a => a.RentRate)
                            .ToList()
                    });
                }
                var table = new
                {
                    title = titleList.Select(a => a.ParkName).ToList(),
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

        public FileResult ImportToExcel(string park, string stime, string etime, int model)
        {
            try
            {
                HSSFWorkbook book = new HSSFWorkbook();
                string fileName = string.Empty;
                if (model == 1)
                {
                    //汇总数据导出
                    fileName = "房屋出租率汇总报表-"+DateTime.Now.ToString("yyyyMMddHHmmsss");
                    if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
                    {
                        stime = DateTime.Now.ToString("yyyy-MM-dd");
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    List<SqlParameter> spList = new List<SqlParameter>
                    {
                        new SqlParameter("Model",1),
                        new SqlParameter("BeginTime",stime),
                        new SqlParameter("EndTime",etime),
                        new SqlParameter("Account",user.Account)
                    };
                    DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsRM", CommandType.StoredProcedure, spList.ToArray());
                    //工作簿
                    DataRow[] parkRows = string.IsNullOrEmpty(park) ? ds.Tables[0].Select() : ds.Tables[0].Select(string.Format("ParkID='{0}'", park));
                    ISheet parkSheet = book.CreateSheet("房屋出租率汇总数据");
                    //条件列
                    IRow condition = parkSheet.CreateRow(0);
                    condition.CreateCell(0).SetCellValue("园区：");
                    condition.CreateCell(1).SetCellValue(string.IsNullOrEmpty(park) ? "全部" : parkRows[0]["ParkName"].ToString());
                    condition.CreateCell(2).SetCellValue("开始时间：");
                    condition.CreateCell(3).SetCellValue(stime);
                    condition.CreateCell(4).SetCellValue("结束时间：");
                    condition.CreateCell(5).SetCellValue(etime);
                    //第一行：标题行
                    IRow parkTitle = parkSheet.CreateRow(1);
                    parkTitle.CreateCell(0).SetCellValue("园区");
                    parkTitle.CreateCell(1).SetCellValue("房间总数");
                    parkTitle.CreateCell(2).SetCellValue("面积总数");
                    parkTitle.CreateCell(3).SetCellValue("出租面积");
                    parkTitle.CreateCell(4).SetCellValue("内部使用面积");
                    parkTitle.CreateCell(5).SetCellValue("空置面积");
                    parkTitle.CreateCell(6).SetCellValue("出租率");
                    parkTitle.CreateCell(7).SetCellValue("内部使用率");
                    parkTitle.CreateCell(8).SetCellValue("空置率");
                    var rowIndex = 2;
                    //内容
                    foreach (var item in parkRows)
                    {
                        IRow row = parkSheet.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(item["ParkName"].ToString());
                        row.CreateCell(1).SetCellValue(item["TotalNum"].ToString());
                        row.CreateCell(2).SetCellValue(item["Total"].ToString());
                        row.CreateCell(3).SetCellValue(item["Rent"].ToString());
                        row.CreateCell(4).SetCellValue(item["Self"].ToString());
                        row.CreateCell(5).SetCellValue(item["Free"].ToString());
                        row.CreateCell(6).SetCellValue(item["RentRate"].ToString() + "%");
                        row.CreateCell(7).SetCellValue(item["SelfRate"].ToString() + "%");
                        row.CreateCell(8).SetCellValue(item["FreeRate"].ToString() + "%");
                        rowIndex++;
                    }
                    //工作簿
                    DataRow[] buildRows = string.IsNullOrEmpty(park) ? ds.Tables[1].Select() : ds.Tables[1].Select(string.Format("ParkID='{0}'", park));
                    ISheet buildSheet = book.CreateSheet("楼栋出租率汇总数据");
                    //条件列
                    condition = buildSheet.CreateRow(0);
                    condition.CreateCell(0).SetCellValue("园区：");
                    condition.CreateCell(1).SetCellValue(string.IsNullOrEmpty(park) ? "全部" : parkRows[0]["ParkName"].ToString());
                    condition.CreateCell(2).SetCellValue("开始时间：");
                    condition.CreateCell(3).SetCellValue(stime);
                    condition.CreateCell(4).SetCellValue("结束时间：");
                    condition.CreateCell(5).SetCellValue(etime);
                    //第一行：标题行
                    IRow buildTitle = buildSheet.CreateRow(1);
                    buildTitle.CreateCell(0).SetCellValue("园区");
                    buildTitle.CreateCell(1).SetCellValue("楼栋");
                    buildTitle.CreateCell(2).SetCellValue("房间总数");
                    buildTitle.CreateCell(3).SetCellValue("面积总数");
                    buildTitle.CreateCell(4).SetCellValue("出租面积");
                    buildTitle.CreateCell(5).SetCellValue("内部使用面积");
                    buildTitle.CreateCell(6).SetCellValue("空置面积");
                    buildTitle.CreateCell(7).SetCellValue("出租率");
                    buildTitle.CreateCell(8).SetCellValue("内部使用率");
                    buildTitle.CreateCell(9).SetCellValue("空置率");
                    rowIndex = 2;
                    //内容
                    foreach (var item in buildRows)
                    {
                        IRow row = buildSheet.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(item["ParkName"].ToString());
                        row.CreateCell(1).SetCellValue(item["Name"].ToString());
                        row.CreateCell(2).SetCellValue(item["TotalNum"].ToString());
                        row.CreateCell(3).SetCellValue(item["Total"].ToString());
                        row.CreateCell(4).SetCellValue(item["Rent"].ToString());
                        row.CreateCell(5).SetCellValue(item["Self"].ToString());
                        row.CreateCell(6).SetCellValue(item["Free"].ToString());
                        row.CreateCell(7).SetCellValue(item["RentRate"].ToString() + "%");
                        row.CreateCell(8).SetCellValue(item["SelfRate"].ToString() + "%");
                        row.CreateCell(9).SetCellValue(item["FreeRate"].ToString() + "%");
                        rowIndex++;
                    }

                }
                else
                {
                    //趋势图数据导出
                    fileName = "房屋出租率趋势报表-" + DateTime.Now.ToString("yyyyMMddHHmmsss");
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
                        new SqlParameter("BeginTime",stime),
                        new SqlParameter("EndTime",etime),
                        new SqlParameter("Account",user.Account)
                    };
                    DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsRM", CommandType.StoredProcedure, spList.ToArray());
                    var result = JsonConvert.DeserializeObject<List<StatisticsMonthView>>(JsonConvert.SerializeObject(ds.Tables[0]));
                    //if (!string.IsNullOrEmpty(park)) result = result.Where(a => a.ParkID == park).ToList();
                    //工作簿
                    ISheet sheet = book.CreateSheet("出租率趋势数据");
                    //第一行：标题行
                    var column = 1;
                    IRow title = sheet.CreateRow(0);
                    title.CreateCell(0).SetCellValue("园区");
                    var month = result.GroupBy(a => a.MonthTime).OrderBy(a => a.Key).Select(a=>a.Key).ToList();
                    foreach (var item in month)
                    {
                        title.CreateCell(column).SetCellValue(item);
                        column++;
                    }
                    //内容
                    var rowIndex = 1;
                    var rows = result.GroupBy(a => new { a.ParkID, a.ParkName }).Select(a => new { a.Key.ParkID, a.Key.ParkName }).ToList();
                    foreach (var item in rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        dataRow.CreateCell(0).SetCellValue(item.ParkName);
                        column = 1;
                        var monthData = result.Where(a => a.ParkID == item.ParkID).OrderBy(a => a.MonthTime).ToList();
                        foreach (var data in monthData)
                        {
                            dataRow.CreateCell(column).SetCellValue(data.RentRate + "%");
                            column++;
                        }
                        rowIndex++;
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