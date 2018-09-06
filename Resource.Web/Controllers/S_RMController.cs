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
    public class S_RMController : RSBaseController
    {
        // GET: S_RM
        public ActionResult Index()
        {

            return View();
        }
        public ContentResult Search(string park)
        {
            var parkDefalut = user.Park.Split(',')[0];
            //var parkList = dc.Set<T_Park>().Where(a => park.Contains(a.ID)).Select(a => new { a.ID, a.Name }).ToList();
            //var stageObj = dc.Set<T_Stage>().Where(a => a.ParkID == parkList[0].ID).FirstOrDefault();
            var list = dc.Set<V_SRM>().Where(a => true);
            if (!string.IsNullOrEmpty(park)) list = list.Where(a => a.ParkID == park);
            else
            {
                list = list.Where(a => a.ParkID == parkDefalut);
            }
            var buildList = list
                .GroupBy(a => new { a.BuildingID, a.BuildingName })
                .Select(a => new { a.Key.BuildingID, a.Key.BuildingName })
                .ToList();
            List<object> resObjcet = new List<object>();
            foreach (var item in buildList)
            {
                var floorList = list
                    .Where(a => a.BuildingID == item.BuildingID)
                    .GroupBy(a => new { a.FloorID, a.FloorName })
                    .Select(a => new { a.Key.FloorID, a.Key.FloorName })
                    .ToList();
                List<object> floorObjList = new List<object>();
                foreach (var it in floorList)
                {
                    var rm = list.Where(a => a.FloorID == it.FloorID).ToList();
                    floorObjList.Add(new { FID = it.FloorID, FName = it.FloorName, RM = rm });
                }
                resObjcet.Add(new { BID = item.BuildingID, BName = item.BuildingName, Floor = floorObjList });
            }

            var obj = JsonConvert.SerializeObject(resObjcet).Replace("null", "\"\"");
            return Content(obj);
        }

    }
}