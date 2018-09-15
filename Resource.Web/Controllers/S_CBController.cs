using Newtonsoft.Json;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class S_CBController : ResourceBusinessController
    {
        // GET: S_CB
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Search(string parkID)
        {
            var park = user.Park.Split(',')[0];
            var list = dc.Set<V_Resource>().Where(a => a.ResourceKindID == 2);
            if (!string.IsNullOrEmpty(parkID)) list = list.Where(a => a.Loc1 == parkID);
            else
            {
                list = list.Where(a => a.Loc1 == park);
            }
            var cbTypeList = list
                .GroupBy(a => new { a.ResourceTypeID, a.ResourceTypeName })
                .OrderBy(a => a.Key.ResourceTypeName)
                .Select(a => new { a.Key.ResourceTypeID, a.Key.ResourceTypeName })
                .ToList();
            List<object> resObjcet = new List<object>();
            foreach (var item in cbTypeList)
            {
                var rmList = list
                    .Where(a => a.ResourceTypeID == item.ResourceTypeID)
                    .GroupBy(a => new { a.Loc5, a.Loc5Name })
                    .Select(a => new { a.Key.Loc5, a.Key.Loc5Name })
                    .ToList();
                List<object> rmObjList = new List<object>();
                foreach (var it in rmList)
                {
                    var cb = list.Where(a => a.Loc5 == it.Loc5)
                        .Select(a => new
                        {
                            a.ID,
                            a.Name,
                            a.RentStatus,
                            a.Loc1,
                            a.RentBeginTime,
                            a.RentEndTime,
                            a.CustShortName,
                            a.CustTel,
                            a.RentArea
                        }).ToList();
                    rmObjList.Add(new { RID = it.Loc5, RName = it.Loc5Name, CB = cb });
                }
                resObjcet.Add(new { TID = item.ResourceTypeID, TName = item.ResourceTypeName, RM = rmObjList });
            }
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd"
            };
            var obj = JsonConvert.SerializeObject(resObjcet,setting).Replace("null", "\"\"");
            return Content(obj);
        }
    }
}