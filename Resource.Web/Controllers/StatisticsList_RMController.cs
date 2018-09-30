using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model;
using Resource.Web.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Resource.Web.Controllers
{
    public class StatisticsList_RMController : Controller
    {
        //
        // GET: /StatisticsList_RM/
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Search(SearchParam param)
        {
            string BeginTime = DateTime.Now.ToString("yyyy-MM-dd");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            string PackNo = "01";
            if (param.Stime != null) BeginTime = Convert.ToDateTime(param.Stime).ToString("yyyy-MM-dd");
            if (param.Etime != null) EndTime = Convert.ToDateTime(param.Etime).ToString("yyyy-MM-dd");
            if (param.Park != null) PackNo = param.Park;

            DataSet ds = null;
            DataTable dt = null;
            DataTable dt1 = null;
            if (PackNo == "01")
            {
                doservice.Service service = new doservice.Service();
                service.Url = "http://120.76.154.6/Order/api/Service.asmx";
                ds = service.GetStatisticsList_Room(BeginTime, EndTime, "6E5F0C851FD4");

                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }

            return Content(JsonConvert.SerializeObject(new { data = dt, data1 = dt1 }));
        }

        public ContentResult Search1(SearchParam param)
        {
            string BeginTime = DateTime.Now.ToString("yyyy-MM-dd");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            string PackNo = "01";
            if (param.Park != null) PackNo = param.Park;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dt1 = null;
            if (PackNo == "01")
            {
                doservice.Service service = new doservice.Service();
                service.Url = "http://120.76.154.6/Order/api/Service.asmx";
                ds = service.GetStatisticsList_Room(BeginTime, EndTime, "6E5F0C851FD4");

                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }

            return Content(JsonConvert.SerializeObject(new { data = dt, data1 = dt1 }));
        }

        public ContentResult Search2(SearchParam param)
        {
            string BeginTime = DateTime.Now.ToString("yyyy-MM-dd");
            string EndTime = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            string PackNo = "01";
             if (param.Park != null) PackNo = param.Park;

            DataTable dt = null;
            if (PackNo == "01")
            {
                doservice.Service service = new doservice.Service();
                service.Url = "http://120.76.154.6/Order/api/Service.asmx";
                dt = service.GetStatisticsList_Room_Charts(BeginTime, EndTime, "6E5F0C851FD4");
            }

            return Content(JsonConvert.SerializeObject(new { data = dt }));
        }

        public ContentResult Search3(SearchParam param)
        {
            string BeginTime = DateTime.Now.Year.ToString() + "-01";
            string EndTime = DateTime.Now.Year.ToString() + "-12";
            string PackNo = "01";
             if (param.Park != null) PackNo = param.Park;

            DataTable dt = null;
            if (PackNo == "01")
            {
                doservice.Service service = new doservice.Service();
                service.Url = "http://120.76.154.6/Order/api/Service.asmx";
                dt = service.GetOccupancyRate(BeginTime, EndTime, "6E5F0C851FD4");
            }

            return Content(JsonConvert.SerializeObject(new { data = dt }));
        }

	}
}