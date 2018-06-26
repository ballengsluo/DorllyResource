using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.BLL;
using Resource.IBLL;
using Resource.Model;
using Newtonsoft.Json;

namespace ResWeb.Controllers
{
    public class BuildingController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        private IParkService _parkService = Container.Resolve<IParkService>();
        private IStageService _stageService = Container.Resolve<IStageService>();
        private IBuildingService _buildService = Container.Resolve<IBuildingService>();
        // GET: Building
        public ActionResult Index()
        {
            ViewData["cityList"] = new SelectList(_cityService.GetModels(c => true).ToList(), "code", "name");
            ViewData["regionList"] = new SelectList(_regionService.GetModels(c => true).ToList(), "code", "name");
            ViewData["parkList"] = new SelectList(_parkService.GetModels(c => true).ToList(), "code", "name");
            ViewData["stageList"] = new SelectList(_stageService.GetModels(c => true).ToList(), "code", "name");
            return View();
        }

        // GET: Building/Details/5
        public ActionResult Details()
        {
            var linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { build, stage, park };
            ViewBag.buildList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_BuildingTable");
        }
        public ActionResult GetDropData(string parentCode)
        {
            var list = _buildService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _buildService.GetModels(a => a.StageCode == parentCode);
            ViewData["dataList"] = new SelectList(list.ToList(), "Code", "Name");
            return PartialView("_GetBuildingDropDownList");
        }
        // POST: Building/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                T_Building building = new T_Building();
                building.Code = collection["edit-code"];
                building.Name = collection["edit-name"];
                building.StageCode = collection["edit-stageName"];
                building.GisX = collection["edit-x"];
                building.GisY = collection["edit-y"];
                if (_buildService.Add(building))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Building/Edit/5
        public ActionResult Edit(int id)
        {
            var obj = from build in _buildService.GetModels(b => b.ID == id)
                      join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                      join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                      join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
                      join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                      select new { build, stage, park, region, city };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // POST: Building/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_Building building = _buildService.GetModels(b => b.ID == id).FirstOrDefault();
                building.Code = collection["edit-code"];
                building.Name = collection["edit-name"];
                building.StageCode = collection["edit-stageName"];
                building.GisX = collection["edit-x"];
                building.GisY = collection["edit-y"];
                if (_buildService.Update(building))
                    return Content("1:更新成功！");
                else
                    return Content("5:更新失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Building/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                T_Building building = _buildService.GetModels(b => b.ID == id).FirstOrDefault();
                if (_buildService.Delete(building))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }
        public ActionResult Select(string cityCode, string regionCode, string parkCode, string stageCode)
        {
            //var lambList = _floorService.GetModels(f => true);
            var linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { build, stage, park };
            if (!string.IsNullOrEmpty(stageCode))
            {
                linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where stage.Code == stageCode
                           select new { build, stage, park };
                //var text = _buildService.GetModels(b => b.StageCode == stageCode).Select(b => b.Code);
                //lambList = lambList.Where(f => text.Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(parkCode))
            {
                linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where park.Code == parkCode
                           select new { build, stage, park };
                //lambList = lambList.Where(f =>
                //            (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => s.ParkCode == parkCode).Select(s => s.Code).Contains(b.StageCode))
                //                       ).Select(b => b.Code)).Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(regionCode))
            {
                linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.Code == regionCode
                           select new { build, stage, park };
                //lambList = lambList.Where(f =>
                //             (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => true).Where(s =>
                //                     (_parkService.GetModels(p => p.RegionCode == regionCode).Select(p => p.Code).Contains(s.ParkCode))
                //                         ).Select(s => s.Code).Contains(b.StageCode))
                //                             ).Select(b => b.Code)).Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(cityCode))
            {
                linqList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.CityCode == cityCode
                           select new { build, stage, park };
                //lambList = lambList.Where(f =>
                //             (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => true).Where(s =>
                //                    (_parkService.GetModels(p => true).Where(p =>
                //                         (_regionService.GetModels(r => r.CityCode == cityCode).Select(r => r.Code).Contains(p.RegionCode))
                //                            ).Select(p => p.Code).Contains(s.ParkCode))
                //                               ).Select(s => s.Code).Contains(b.StageCode))
                //                                  ).Select(b => b.Code)).Contains(f.BuildingCode));
            }

            ViewBag.buildList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_BuildingTable");
        }

    }
}
