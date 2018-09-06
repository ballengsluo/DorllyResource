using Newtonsoft.Json;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class S_CBController : RSBaseController
    {
        // GET: S_CB
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Search(string parkID)
        {
            var park = user.Park.Split(',')[0];
            var list = dc.Set<V_SCB>().Where(a => true);
            if (!string.IsNullOrEmpty(parkID)) list = list.Where(a => a.ParkID == parkID);
            else
            {
                list = list.Where(a => a.ParkID == park);
            }
            var cbTypeList = list
                .GroupBy(a => new { a.ResourceTypeID, a.ResourceTypeName })
                .OrderBy(a=>a.Key.ResourceTypeName)
                .Select(a => new { a.Key.ResourceTypeID, a.Key.ResourceTypeName })
                .ToList();
            List<object> resObjcet = new List<object>();
            foreach (var item in cbTypeList)
            {
                var rmList = list
                    .Where(a => a.ResourceTypeID == item.ResourceTypeID)
                    .GroupBy(a => new { a.RoomID, a.RoomName })
                    .Select(a => new { a.Key.RoomID, a.Key.RoomName })
                    .ToList();
                List<object> rmObjList = new List<object>();
                foreach (var it in rmList)
                {
                    var cb = list.Where(a => a.RoomID == it.RoomID).ToList();
                    rmObjList.Add(new { RID = it.RoomID, RName = it.RoomName, CB = cb });
                }
                resObjcet.Add(new { TID = item.ResourceTypeID, TName = item.ResourceTypeName, RM = rmObjList });
            }

            var obj = JsonConvert.SerializeObject(resObjcet).Replace("null", "\"\"");
            return Content(obj);
        }
    }
}