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
    public class StageController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        private IParkService _parkService = Container.Resolve<IParkService>();
        private IStageService _stageService = Container.Resolve<IStageService>();
        // GET: Stage
        public ActionResult Index()
        {
            ViewData["cityList"] = new SelectList(_cityService.GetModels(c => true).ToList(), "code", "name");
            ViewData["regionList"] = new SelectList(_regionService.GetModels(c => true).ToList(), "code", "name");
            ViewData["parkList"] = new SelectList(_parkService.GetModels(c => true).ToList(), "code", "name");
            return View();
        }
        public ActionResult GetDropData(string parentCode)
        {
            var list = _stageService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _stageService.GetModels(a => a.ParkCode == parentCode);
            ViewData["dataList"] = new SelectList(list.ToList(), "Code", "Name");
            return PartialView("_GetStageDropDownList");
        }
        // GET: Stage/Details/5
        public ActionResult Details()
        {
            var linqList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { stage, park };
            ViewBag.stageList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_StageTable");
        }

        // POST: Stage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                T_Stage stage = new T_Stage();
                stage.Code = collection["edit-code"];
                stage.Name = collection["edit-name"];
                stage.ParkCode = collection["edit-parkName"];
                if (_stageService.Add(stage))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Stage/Edit/5
        public JsonResult Edit(int id)
        {
            var obj = from stage in _stageService.GetModels(s => s.ID == id)
                      join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                      join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
                      join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                      select new { stage, park, region, city };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // POST: Stage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_Stage stage = _stageService.GetModels(s => s.ID == id).FirstOrDefault();
                stage.Code = collection["edit-code"];
                stage.Name = collection["edit-name"];
                stage.ParkCode = collection["edit-parkName"];
                if (_stageService.Update(stage))
                    return Content("1:更新成功！");
                else
                    return Content("5:更新失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Stage/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                T_Stage stage = _stageService.GetModels(s => s.ID == id).FirstOrDefault();
                if (_stageService.Delete(stage))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

      
        public ActionResult Select(string cityCode, string regionCode, string parkCode)
        {
            var linqList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { stage, park };
            if (!string.IsNullOrEmpty(parkCode))
            {
                linqList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where park.Code == parkCode
                           select new { stage, park };
            }
            else if (!string.IsNullOrEmpty(regionCode))
            {
                linqList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.Code == regionCode
                           select new { stage, park };
            }
            else if (!string.IsNullOrEmpty(cityCode))
            {
                linqList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.CityCode == cityCode
                           select new { stage, park };
            }

            ViewBag.stageList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_StageTable");
        }
    }
}
