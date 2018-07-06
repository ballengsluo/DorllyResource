using Newtonsoft.Json;
using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class ParkController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        private IParkService _parkService = Container.Resolve<IParkService>();
        // GET: Park
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDropData(string parentCode, string selectCode)
        {
            var list = _parkService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _parkService.GetModels(a => a.RegionCode == parentCode);
            if (!string.IsNullOrEmpty(selectCode)) ViewData["dataList"] = new SelectList(list.ToList(), "ParkCode", "ParkShortName", selectCode);
            else ViewData["dataList"] = new SelectList(list.ToList(), "ParkCode", "ParkShortName");

            return PartialView("_ParkDrop");
        }
        //public ActionResult Details(string cityCode, string regionCode)
        //{
        //    var tempList = from a in _parkService.GetModels(a => true)
        //                   join b in _regionService.GetModels(a => true) on a.RegionCode equals b.Code into t1
        //                   from c in t1.DefaultIfEmpty()
        //                   join d in _cityService.GetModels(a => true) on c.CityCode equals d.Code into t2
        //                   from e in t2.DefaultIfEmpty()
        //                   select new { a.ID, a.Code, a.Name, a.Addr, a.GisX, a.GisY, a.RegionCode, RegionName = c.Name, CityCode = e.Code, CityName = e.Name };
        //    if (!string.IsNullOrEmpty(regionCode)) tempList = tempList.Where(a => a.RegionCode == regionCode);
        //    else if (!string.IsNullOrEmpty(cityCode)) tempList = tempList.Where(a => a.CityCode == cityCode);
        //    return PartialView("_ParkTable", JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tempList.ToList())));
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_Park park)
        {
            try
            {
                if (park.RegionCode == null || park.RegionCode == "") return Json(Result.get(4));
                if (_parkService.Add(park)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }
        //public ActionResult Edit(int id)
        //{
        //    //T_Park park = _parkService.GetModels(a => a.ID == id).FirstOrDefault();
        //    //return View(park);
        //    var tempList = from a in _parkService.GetModels(a => a.ID==id)
        //                   join b in _regionService.GetModels(a => true) on a.RegionCode equals b.Code into t1
        //                   from c in t1.DefaultIfEmpty()
        //                   join d in _cityService.GetModels(a => true) on c.CityCode equals d.Code into t2
        //                   from e in t2.DefaultIfEmpty()
        //                   select new { a.ID, a.Code, a.Name, a.Addr, a.GisX, a.GisY, a.RegionCode, RegionName = c.Name, CityCode = e.Code, CityName = e.Name };
        //    return View(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tempList.FirstOrDefault())));
        //}
        [HttpPost]
        public JsonResult Edit(T_Park park)
        {
            try
            {
                if (park.RegionCode == null || park.RegionCode == "") return Json(Result.get(4));
                if (_parkService.Update(park)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                T_Park park = _parkService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_parkService.Delete(park)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }
        #region old

        //// GET: Park/Details/5
        //public ActionResult Details()
        //{
        //    var linqList = from park in _parkService.GetModels(p => true)
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //                   select new { city, region, park };
        //    ViewBag.parkList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_ParkTable");
        //}

        //public ActionResult GetDropData(string parentCode)
        //{
        //    var list = _parkService.GetModels(a => true);
        //    if (!string.IsNullOrEmpty(parentCode)) list = _parkService.GetModels(a => a.RegionCode == parentCode);
        //    ViewData["dataList"] = new SelectList(list.ToList(), "Code", "Name");
        //    return PartialView("_ParkDrop");
        //}

        //// POST: Park/Create
        //[HttpPost]
        //public ContentResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        T_Park park = new T_Park();
        //        park.Code = collection["edit-code"];
        //        park.Name = collection["edit-name"];
        //        park.RegionCode = collection["edit-regionName"];
        //        park.Addr = collection["edit-addr"];
        //        park.GisX = collection["edit-x"];
        //        park.GisY = collection["edit-y"];
        //        if (_parkService.Add(park))
        //            return Content("1:添加成功！");
        //        else
        //            return Content("5:添加失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Park/Edit/5
        //public JsonResult Edit(int id)
        //{
        //    var obj = from park in _parkService.GetModels(p => p.ID == id)
        //              join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //              join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //              select new { city, region, park };
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //// POST: Park/Edit/5
        //[HttpPost]
        //public ContentResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        T_Park park = _parkService.GetModels(p => p.ID == id).FirstOrDefault();
        //        park.Code = collection["edit-code"];
        //        park.Name = collection["edit-name"];
        //        park.RegionCode = collection["edit-regionName"];
        //        park.Addr = collection["edit-addr"];
        //        park.GisX = collection["edit-x"];
        //        park.GisY = collection["edit-y"];
        //        if (_parkService.Update(park))
        //            return Content("1:更新成功！");
        //        else
        //            return Content("5:更新失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Park/Delete/5
        //public ContentResult Delete(int id)
        //{
        //    try
        //    {
        //        T_Park park = _parkService.GetModels(p => p.ID == id).FirstOrDefault();
        //        if (_parkService.Delete(park))
        //            return Content("1:删除成功！");
        //        else
        //            return Content("5:删除失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //public ActionResult Select(string cityCode, string regionCode)
        //{
        //    //var lambList = _floorService.GetModels(f => true);
        //    var linqList = from park in _parkService.GetModels(p => true)
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //                   select new { city, region, park };
        //    if (!string.IsNullOrEmpty(regionCode))
        //    {
        //        linqList = from park in _parkService.GetModels(p => true)
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //                   where park.RegionCode == regionCode
        //                   select new { city, region, park };
        //    }
        //    else if (!string.IsNullOrEmpty(cityCode))
        //    {
        //        linqList = from park in _parkService.GetModels(p => true)
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //                   where region.CityCode == cityCode
        //                   select new { city, region, park };
        //    }
        //    ViewBag.parkList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_ParkTable");
        //}

        #endregion
    
    }
}
