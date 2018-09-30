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
    public class StatisticsList_CRController : Controller
    {
        //
        // GET: /StatisticsList_CR/
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Search(SearchParam param)
        {
            string PackNo = "01";
            if (param.Park != null) PackNo = param.Park;

            butlerservice.AppService service1 = new butlerservice.AppService();
            service1.Url = "http://wx.dorlly.com/api/AppService.asmx";
            DataTable dt = service1.GetStatisticsList_Resourse(PackNo, "", "", "5218E3ED752A49D4");

            return Content(JsonConvert.SerializeObject(new { data = dt }));
        }
        
        public ContentResult Search2(SearchParam param)
        {
            string PackNo = "01";
            if (param.Park != null) PackNo = param.Park;

            butlerservice.AppService service1 = new butlerservice.AppService();
            service1.Url = "http://wx.dorlly.com/api/AppService.asmx";
            DataTable dt = service1.GetStatisticsList_Resourse_Charts(PackNo, "", "", "5218E3ED752A49D4");

            return Content(JsonConvert.SerializeObject(new { data = dt }));
        }

        public ContentResult Search3(SearchParam param)
        {
            string BeginTime = DateTime.Now.ToString("yyyy-MM-dd");
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            string PackNo = "01";
            if (param.Stime != null) BeginTime = Convert.ToDateTime(param.Stime).ToString("yyyy-MM-dd");
            if (param.Etime != null) EndTime = Convert.ToDateTime(param.Etime).ToString("yyyy-MM-dd");
            if (param.Park != null) PackNo = param.Park;

            butlerservice.AppService service1 = new butlerservice.AppService();
            service1.Url = "http://wx.dorlly.com/api/AppService.asmx";
            DataTable dt = service1.GetStatisticsList_Resourse_List(PackNo, BeginTime, EndTime, "5218E3ED752A49D4");
            
            return Content(JsonConvert.SerializeObject(new { data = dt }));
        }
	}
}