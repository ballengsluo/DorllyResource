using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace Resource.Web.Controllers
{
    public class S_MRController : Controller
    {
        // GET: S_MR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(SearchParam param)
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
                        tableStr += string.Format("<td data-status='{0}' data-id='{1}' id='{2}'></td>", temp[i].Status,temp[i].RowPointer,Guid.NewGuid().ToString());
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