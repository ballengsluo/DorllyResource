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
    public class RegionController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        // GET: Region
        public ActionResult Index()
        {
            ViewBag.regionList = _regionService.GetModels(r => true).ToList();
            ViewData["cityList"] = new SelectList(_cityService.GetModels(c => true).ToList(), "code", "name");
            return View();
        }

        // GET: Region/Details/5
        public ActionResult Details()
        {
            var obj = from region in _regionService.GetModels(p => true)
                      join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                      select new { city, region };
            ViewBag.regionList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
            return PartialView("_RegionTable");
        }

        // POST: Region/Create
        [HttpPost]
        public ContentResult Create(FormCollection collection)
        {
            try
            {
                T_Region region = new T_Region();
                region.Code = collection["edit-code"];
                region.Name = collection["edit-name"];
                region.CityCode = collection["edit-city"];
                if (_regionService.Add(region))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Region/Edit/5
        public JsonResult Edit(int id)
        {
            var obj = from region in _regionService.GetModels(p => p.ID == id)
                      join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                      select new { city, region };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // POST: Region/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_Region region = _regionService.GetModels(r => r.ID == id).FirstOrDefault();
                region.Code = collection["edit-code"];
                region.Name = collection["edit-name"];
                region.CityCode = collection["edit-city"];
                if (_regionService.Update(region))
                    return Content("1:修改成功！");
                else
                    return Content("5:修改失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                T_Region region = _regionService.GetModels(r => r.ID == id).FirstOrDefault();
                if (_regionService.Delete(region))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        public ActionResult Select(string cityCode)
        {
            var linqList = from region in _regionService.GetModels(p => true)
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           select new { city, region };
            if (!string.IsNullOrEmpty(cityCode))
            {
                linqList = from region in _regionService.GetModels(p => true)
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           where region.CityCode == cityCode
                           select new { city, region };
            }
            ViewBag.regionList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_RegionTable");
        }
    }
}
