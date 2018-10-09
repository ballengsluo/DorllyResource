using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Web.Models;
using Resource.Model;
using Newtonsoft.Json;
namespace Resource.Web.Controllers
{
    public class S_RMController : ResourceBusinessController
    {
        // GET: S_RM
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Search(SearchParam param)
        {
            string park = string.Empty;
            var list = dc.Set<V_Resource>().Where(a => a.ResourceKindID == 1);
            if (!string.IsNullOrEmpty(param.Park)) park = param.Park;
            else park = ParkList.FirstOrDefault();
            list = list.Where(a => a.Loc1 == park);
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Cust)) list = list.Where(a => a.CustLongName.Contains(param.Cust));
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            var bulidList = dc.Set<V_Building>().Where(a => a.ParkID == park).Select(a => new { a.ID, a.Name }).ToList();
            var floorList = dc.Set<V_Floor>().Where(a => a.ParkID == park).Select(a => new { a.ID, a.Name, BID = a.BuildingID }).ToList();
            var rmList = list.Select(a => new
            {
                a.ID,
                a.Name,
                a.Status,
                a.RentBeginTime,
                a.RentEndTime,
                a.CustShortName,
                a.CustTel,
                a.RentArea,
                a.Loc4
            }).ToList();
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd"
            };
            var obj = JsonConvert.SerializeObject(new
            {
                park = park,
                blist = bulidList,
                flist = floorList,
                rmlist = rmList
            }, setting).Replace("null", "\"\"");
            return Content(obj);
        }
    }
}