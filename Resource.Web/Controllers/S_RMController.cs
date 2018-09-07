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
        public ContentResult Search(string park)
        {
            var parkDefalut = user.Park.Split(',')[0];
            var list = dc.Set<V_RS_Info>().Where(a => a.ResourceKindID == 1);
            if (!string.IsNullOrEmpty(park)) list = list.Where(a => a.Loc1 == park);
            else
            {
                list = list.Where(a => a.Loc1 == parkDefalut);
            }
            var buildList = list
                .GroupBy(a => new { a.Loc3, a.Loc3Name })
                .Select(a => new { a.Key.Loc3, a.Key.Loc3Name })
                .ToList();
            List<object> result = new List<object>();
            foreach (var item in buildList)
            {
                var floorList = list
                    .Where(a => a.Loc3 == item.Loc3)
                    .GroupBy(a => new { a.Loc4, a.Loc4Name })
                    .Select(a => new { a.Key.Loc4, a.Key.Loc4Name })
                    .ToList();
                List<object> floorObjList = new List<object>();
                foreach (var it in floorList)
                {
                    var rm = list.Where(a => a.Loc4 == it.Loc4)
                        .Select(a => new
                        {
                            a.ID,
                            a.Name,
                            a.Status,
                            a.RentBeginTime,
                            a.RentEndTime,
                            a.CustShortName,
                            a.CustTel,
                            a.RentArea
                        }).ToList();
                    floorObjList.Add(new { FID = it.Loc4, FName = it.Loc4Name, RM = rm });
                }
                result.Add(new { BID = item.Loc3, BName = item.Loc3Name, Floor = floorObjList });
            }
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd"
            };
            var obj = JsonConvert.SerializeObject(result,setting).Replace("null", "\"\"");
            return Content(obj);
        }
    }
}