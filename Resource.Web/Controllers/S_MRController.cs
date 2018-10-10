using Resource.Web.Models;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;


namespace Resource.Web.Controllers
{
    public class S_MRController : BaseController
    {
        // GET: S_MR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            try
            {
                if (param.Stime == null) return Json(Result.Fail(msg: "请选择时间！"), JsonRequestBehavior.AllowGet);
                DateTime begin = Convert.ToDateTime(Convert.ToDateTime(param.Stime).ToString("yyyy-MM-dd") + " 09:00:00");
                DateTime end = Convert.ToDateTime(Convert.ToDateTime(param.Stime).ToString("yyyy-MM-dd") + " 21:00:00");
                DateTime selectTime1 = begin.Date;
                DateTime selectTime2 = begin.Date.AddDays(1);
                string tableStr = string.Empty;
                string detailStr = string.Empty;
                var crList = dc.Set<V_Resource>().Where(a => a.ResourceKindID == 3 && ParkList.Contains(a.Loc1));
                //会议室数据
                if (!string.IsNullOrEmpty(param.Park)) crList = crList.Where(a => a.Loc1 == param.Park);
                var crTable = crList.Select(a => new { a.ID, a.Name }).ToList();
                if (crTable.Count() <= 0)
                {
                    tableStr += "<tr><td colspan='25' style='text-align:center;'>暂无数据</td></tr>";
                }
                foreach (var item in crTable)//遍历会议室
                {
                    var tempTime = begin;

                    tableStr += string.Format("<tr><td>{0}</td>", item.Name);

                    var statusList = dc.Set<V_ResourceStatus>()
                    .Where(a => a.ResourceID == item.ID && selectTime1 < a.RentBeginTime && a.RentEndTime < selectTime2)
                    .OrderBy(a => a.RentBeginTime)
                    .ToList();
                    if (statusList.Count() > 0)
                    {
                        //存在数据
                        foreach (var status in statusList)
                        {
                            if (status.RentBeginTime <= tempTime)//小于正常开始时间
                            {
                                if (status.RentEndTime <= end)
                                {
                                    tableStr += TimeSplit(tempTime, status.RentEndTime, status);
                                    tempTime = Convert.ToDateTime(status.RentEndTime);
                                }
                                else//大于正常结束时间
                                {
                                    tableStr += TimeSplit(tempTime, end, status);
                                    tempTime = end;
                                }
                            }
                            else
                            {

                                tableStr += TimeSplit(tempTime, Convert.ToDateTime(status.RentBeginTime), null);//空隙
                                if (end < status.RentEndTime)//超越正常时间
                                {
                                    tableStr += TimeSplit(tempTime, end, status);
                                    tempTime = end;
                                }
                                else
                                {
                                    tableStr += TimeSplit(Convert.ToDateTime(status.RentBeginTime), Convert.ToDateTime(status.RentEndTime), status);
                                    tempTime = Convert.ToDateTime(status.RentEndTime);
                                }
                            }
                            
                            //状态详情组装
                            detailStr += string.Format(@"<div class='detail' data-id='{0}'>
                                                                <p><span>资源编号：</span><span>{1}</span></p>
                                                                <p><span>公司(个人)：</span><span>{2}</span></p>
                                                                <p><span>联系电话：</span><span>{3}</span></p>
                                                                <p><span>开始时间：</span>{4}<span></span></p>
                                                                <p><span>结束时间：</span>{5}<span></span></p>
                                                                </div>",
                                                                       status.ID,
                                                                       status.ResourceID,
                                                                       status.CustLongName,
                                                                       status.CustTel,
                                                                       status.RentBeginTime,
                                                                       status.RentEndTime);
                        }
                        if (tempTime < end)//时间刻度数据组装：后面空格
                        {
                            tableStr += TimeSplit(tempTime, end, null);
                        }
                    }
                    else
                    {
                        //没有数据
                        tableStr += TimeSplit(begin, end, null);
                    }
                }

                return Json(new { Flag = 1, table = tableStr, detail = detailStr }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }
        public string TimeSplit(DateTime begin, DateTime end, V_ResourceStatus status)
        {
            var str = string.Empty;
            TimeSpan tempts = end - begin;
            var tdNum = (int)tempts.TotalMinutes / 30;
            if (status != null)
            {
                for (int n = 0; n < tdNum; n++)
                {
                    str += string.Format("<td data-pid='{0}' data-status='{1}' id='{2}'></td>", status.ID, status.Status, Guid.NewGuid().ToString());
                }
            }
            else
            {
                for (int n = 0; n < tdNum; n++)
                {
                    str += "<td></td>";
                }
            }
            return str;
        }
        public ActionResult SearchBak(SearchParam param)
        {
            string tableStr = string.Empty;
            string detail = string.Empty;
            string park = "01";
            DateTime date = Convert.ToDateTime("2017-11-28");
            butlerservice.AppService service = new butlerservice.AppService();
            service.Url = "http://wx.dorlly.com/api/AppService.asmx";
            DataSet ds = service.GetConferenceRoomReserveList_Resource(park, date.ToString("yyyy-MM-dd"));
            List<SMRView> mrList = JsonConvert.DeserializeObject<List<SMRView>>(JsonConvert.SerializeObject(ds.Tables[0]));
            List<SMRView> orderList = JsonConvert.DeserializeObject<List<SMRView>>(JsonConvert.SerializeObject(ds.Tables[1]));
            foreach (var item in mrList)
            {
                tableStr += string.Format("<tr><td>{0}</td>", item.CRName);
                var temp = orderList.Where(a => a.CRNo == item.CRNo).OrderBy(a => a.CRBegReservedDate).ToList();
                DateTime begin = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
                DateTime end = new DateTime(date.Year, date.Month, date.Day, 21, 0, 0);
                for (int i = 0; i < temp.Count(); i++)
                {
                    detail += string.Format(@"<div class='detail' data-pid='{0}'>
                                            <p><span>资源编号：</span><span>{1}</span></p>
                                            <p><span>公司(个人)：</span><span>{2}</span></p>
                                            <p><span>联系电话：</span><span>{3}</span></p>
                                            <p><span>开始时间：</span>{4}<span></span></p>
                                            <p><span>结束时间：</span>{5}<span></span></p>
                                            </div>",
                                                   temp[i].RowPointer,
                                                   temp[i].CRNo,
                                                   temp[i].ApplyUserName,
                                                   temp[i].ApplyTel,
                                                   temp[i].CRBegReservedDate,
                                                   temp[i].CREndReservedDate);
                    int tdNum = 0;
                    //空格处理
                    if (temp[i].CRBegReservedDate > begin)
                    {
                        TimeSpan tempts = temp[i].CRBegReservedDate - begin;
                        tdNum = (int)tempts.TotalMinutes / 30;
                        for (int n = 0; n < tdNum; n++)
                        {
                            tableStr += "<td></td>";
                        }
                    }
                    //数据处理
                    //tableStr += string.Format("<tbody data-id='{0}'>", temp[i].RowPointer);
                    TimeSpan ts = temp[i].CREndReservedDate - temp[i].CRBegReservedDate;
                    tdNum = (int)ts.TotalMinutes / 30;
                    for (int n = 0; n < tdNum; n++)
                    {
                        tableStr += string.Format("<td data-status='{0}' data-id='{1}' id='{2}'></td>", temp[i].Status, temp[i].RowPointer, Guid.NewGuid().ToString());
                    }
                    //tableStr += "</tbody>";
                    begin = temp[i].CREndReservedDate;
                }
                //空格处理
                if (end > begin)
                {
                    TimeSpan tempts = end - begin;
                    int num = (int)tempts.TotalMinutes / 30;
                    for (int i = 0; i < num; i++)
                    {
                        tableStr += "<td></td>";
                    }
                }
                tableStr += "</tr>";
            }
            return Json(new { table = tableStr, detail = detail }, JsonRequestBehavior.AllowGet);
        }
    }
}