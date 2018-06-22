using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.BLL;
using Resource.IBLL;
using Resource.Model;
namespace ResWeb.Controllers
{
    public class FloorController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        private IParkService _parkService = Container.Resolve<IParkService>();
        private IStageService _stageService = Container.Resolve<IStageService>();
        private IBuildingService _buildService = Container.Resolve<IBuildingService>();
        private IFloorService _floorService = Container.Resolve<IFloorService>();
        // GET: Floor
        public ActionResult Index()
        {
            ViewData["cityList"] = new SelectList(_cityService.GetModels(c => true), "code", "name");
            ViewData["regionList"] = new SelectList(_regionService.GetModels(c => true), "code", "name");
            ViewData["parkList"] = new SelectList(_parkService.GetModels(c => true), "code", "name");
            ViewData["stageList"] = new SelectList(_stageService.GetModels(c => true), "code", "name");
            ViewData["buildList"] = new SelectList(_buildService.GetModels(c => true), "code", "name");
            return View();
        }

        // GET: Floor/Details/5
        public ActionResult Details()
        {
            var linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { floor, build, stage, park };
            ViewBag.floorList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return View("_FloorTable");
        }



        // POST: Floor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                T_Floor floor = new T_Floor();
                floor.Code = collection["edit-code"];
                floor.Name = collection["edit-name"];
                floor.BuildingCode = collection["edit-build"];
                if (_floorService.Add(floor))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Floor/Edit/5
        public JsonResult Edit(int id)
        {
            var linqList = from floor in _floorService.GetModels(f => f.ID==id)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           select new { floor, build, stage, park ,region,city};
            //var floorObject = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList)); 
            return Json(linqList, JsonRequestBehavior.AllowGet);
        }

        // POST: Floor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_Floor floor = _floorService.GetModels(f => f.ID == id).FirstOrDefault();
                floor.Code = collection["edit-code"];
                floor.Name = collection["edit-name"];
                floor.BuildingCode = collection["edit-build"];
                if (_floorService.Update(floor))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: Floor/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                T_Floor floor = _floorService.GetModels(f => f.ID == id).FirstOrDefault();
                if (_floorService.Delete(floor))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }


        public ActionResult Select(string cityCode, string regionCode, string parkCode, string stageCode, string buildingCode)
        {
            //var lambList = _floorService.GetModels(f => true);
            var linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { floor, build, stage, park };
            if (!string.IsNullOrEmpty(buildingCode))
            {
                linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where build.Code == buildingCode
                           select new { floor, build, stage, park };
                //lambList = lambList.Where(f => f.BuildingCode == buildingCode);
            }
            else if (!string.IsNullOrEmpty(stageCode))
            {
                linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where stage.Code == stageCode
                           select new { floor, build, stage, park };

                //var text = _buildService.GetModels(b => b.StageCode == stageCode).Select(b => b.Code);
                //lambList = lambList.Where(f => text.Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(parkCode))
            {
                linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           where park.Code == parkCode
                           select new { floor, build, stage, park };
                //lambList = lambList.Where(f =>
                //            (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => s.ParkCode == parkCode).Select(s => s.Code).Contains(b.StageCode))
                //                       ).Select(b => b.Code)).Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(regionCode))
            {
                linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.Code == regionCode
                           select new { floor, build, stage, park };
                //lambList = lambList.Where(f =>
                //             (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => true).Where(s =>
                //                     (_parkService.GetModels(p => p.RegionCode == regionCode).Select(p => p.Code).Contains(s.ParkCode))
                //                         ).Select(s => s.Code).Contains(b.StageCode))
                //                             ).Select(b => b.Code)).Contains(f.BuildingCode));
            }
            else if (!string.IsNullOrEmpty(cityCode))
            {
                linqList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
                           where region.CityCode == cityCode
                           select new { floor, build, stage, park };
                //lambList = lambList.Where(f =>
                //             (_buildService.GetModels(b => true).Where(b =>
                //                 (_stageService.GetModels(s => true).Where(s =>
                //                    (_parkService.GetModels(p => true).Where(p =>
                //                         (_regionService.GetModels(r => r.CityCode == cityCode).Select(r => r.Code).Contains(p.RegionCode))
                //                            ).Select(p => p.Code).Contains(s.ParkCode))
                //                               ).Select(s => s.Code).Contains(b.StageCode))
                //                                  ).Select(b => b.Code)).Contains(f.BuildingCode));
            }

            ViewBag.floorList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
            return PartialView("_FloorTable");
        }


        #region 下拉框联动

        public JsonResult GetRegion(string pcode)
        {
            var lambList = _regionService.GetModels(z => z.CityCode == pcode).ToList();
            return Json(lambList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPark(string pcode)
        {
            var lambList = _parkService.GetModels(z => z.RegionCode == pcode).ToList();
            return Json(lambList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStage(string pcode)
        {
            var lambList = _stageService.GetModels(z => z.ParkCode == pcode).ToList();
            return Json(lambList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBuilding(string pcode)
        {
            var lambList = _buildService.GetModels(z => z.StageCode == pcode).ToList();
            return Json(lambList, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
