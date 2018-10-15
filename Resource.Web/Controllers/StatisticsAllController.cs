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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
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

        public FileResult ImportToExcel(string park, string stime, string etime)
        {
            try
            {
                HSSFWorkbook book = new HSSFWorkbook();
                if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
                {
                    stime = DateTime.Now.ToString("yyyy-MM-dd");
                    etime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                List<SqlParameter> spList = new List<SqlParameter>
                {
                    new SqlParameter("Park",park),
                    new SqlParameter("BeginTime",stime),
                    new SqlParameter("EndTime",etime),
                    new SqlParameter("Account",user.Account)
                };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_StatisticsAll", CommandType.StoredProcedure, spList.ToArray());
                ISheet sheet = book.CreateSheet("资源出租率汇总数据");
                //条件列
                IRow condition = sheet.CreateRow(0);
                condition.CreateCell(0).SetCellValue("园区：");
                condition.CreateCell(1).SetCellValue(string.IsNullOrEmpty(park) ? "全部" : dc.Set<T_Park>().Where(a => a.ID == park).FirstOrDefault().Name);
                condition.CreateCell(2).SetCellValue("开始时间：");
                condition.CreateCell(3).SetCellValue(stime);
                condition.CreateCell(4).SetCellValue("结束时间：");
                condition.CreateCell(5).SetCellValue(etime);
                //第一行：标题行
                IRow title = sheet.CreateRow(1);
                title.CreateCell(0).SetCellValue("资源类型");
                title.CreateCell(1).SetCellValue("总数");
                title.CreateCell(2).SetCellValue("客户租赁");
                title.CreateCell(3).SetCellValue("内部使用");
                title.CreateCell(4).SetCellValue("空置");
                title.CreateCell(5).SetCellValue("客户出租率");
                title.CreateCell(6).SetCellValue("内部使用率");
                title.CreateCell(7).SetCellValue("空置率");
                var rowIndex = 2;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(GetResourceName(ds.Tables[0].Rows[i]["Kind"].ToString()));
                    row.CreateCell(1).SetCellValue(ds.Tables[0].Rows[i]["Total"].ToString());
                    row.CreateCell(2).SetCellValue(ds.Tables[0].Rows[i]["Rent"].ToString());
                    row.CreateCell(3).SetCellValue(ds.Tables[0].Rows[i]["Self"].ToString());
                    row.CreateCell(4).SetCellValue(ds.Tables[0].Rows[i]["Free"].ToString());
                    row.CreateCell(5).SetCellValue(ds.Tables[0].Rows[i]["RentRate"].ToString() + "%");
                    row.CreateCell(6).SetCellValue(ds.Tables[0].Rows[i]["SelfRate"].ToString() + "%");
                    row.CreateCell(7).SetCellValue(ds.Tables[0].Rows[i]["FreeRate"].ToString() + "%");
                    rowIndex++;
                }
                MemoryStream ms = new MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", string.Format("资源出租率汇总报表-{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmsss")));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string GetResourceName(string id)
        {
            if (id == "1") return "房屋";
            else if (id == "2") return "工位";
            else if (id == "3") return "会议室";
            else if (id == "4") return "广告位";
            return "未知";
        }
    }
}