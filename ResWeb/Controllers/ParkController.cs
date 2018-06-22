using Newtonsoft.Json;
using Resource.BLL;
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
            ViewData["regionList"] = new SelectList(_regionService.GetModels(r => true), "code", "name");
            ViewData["cityList"] = new SelectList(_cityService.GetModels(c => true), "code", "name");
            return View();
        }

        // GET: Park/Details/5
        public ActionResult Details()
        {
            var linqList = from park in _parkService.GetModels(p => true)
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           select new { city, region, park };
            ViewBag.parkList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_ParkTable");
        }



        // POST: Park/Create
        [HttpPost]
        public ContentResult Create(FormCollection collection)
        {
            try
            {
                T_Park park = new T_Park();
                park.Code = collection["edit-code"];
                park.Name = collection["edit-name"];
                park.RegionCode = collection["edit-regionName"];
                park.Addr = collection["edit-addr"];
                park.GisX = collection["edit-x"];
                park.GisY = collection["edit-y"];
                if (_parkService.Add(park))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Park/Edit/5
        public JsonResult Edit(int id)
        {
            var obj = from park in _parkService.GetModels(p => p.ID == id)
                      join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                      join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                      select new { city, region, park };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // POST: Park/Edit/5
        [HttpPost]
        public ContentResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_Park park = _parkService.GetModels(p => p.ID == id).FirstOrDefault();
                park.Code = collection["edit-code"];
                park.Name = collection["edit-name"];
                park.RegionCode = collection["edit-regionName"];
                park.Addr = collection["edit-addr"];
                park.GisX = collection["edit-x"];
                park.GisY = collection["edit-y"];
                if (_parkService.Update(park))
                    return Content("1:更新成功！");
                else
                    return Content("5:更新失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Park/Delete/5
        public ContentResult Delete(int id)
        {
            try
            {
                T_Park park = _parkService.GetModels(p => p.ID == id).FirstOrDefault();
                if (_parkService.Delete(park))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        public ActionResult Select(string cityCode, string regionCode)
        {
            //var lambList = _floorService.GetModels(f => true);
            var linqList = from park in _parkService.GetModels(p => true)
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           select new { city, region, park };
            if (!string.IsNullOrEmpty(regionCode))
            {
                linqList = from park in _parkService.GetModels(p => true)
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           where park.RegionCode == regionCode
                           select new { city, region, park };
            }
            else if (!string.IsNullOrEmpty(cityCode))
            {
                linqList = from park in _parkService.GetModels(p => true)
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           where region.CityCode == cityCode
                           select new { city, region, park };
            }
            ViewBag.parkList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_ParkTable");
        }
    }
}
