using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.BLL.Container;
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
        private IBuildingService _buildingService = Container.Resolve<IBuildingService>();
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDropData(string parentCode, string selectCode)
        {
            var list = _buildingService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _buildingService.GetModels(a => a.StageCode == parentCode);
            if (!string.IsNullOrEmpty(selectCode)) ViewData["dataList"] = new SelectList(list.ToList(), "BuildingCode", "BuildingName", selectCode);
            else ViewData["dataList"] = new SelectList(list.ToList(), "BuildingCode", "BuildingName");
            return PartialView("_BuildingDrop");
        }

        //public ActionResult Details(string cityCode, string regionCode, string parkCode, string stageCode)
        //{
        //    var tempList = from a in _buildingService.GetModels(a => true)
        //                   join b in _stageService.GetModels(a => true) on a.StageCode equals b.Code into t1
        //                   from c in t1.DefaultIfEmpty()
        //                   join d in _parkService.GetModels(a => true) on c.ParkCode equals d.Code into t2
        //                   from e in t2.DefaultIfEmpty()
        //                   join f in _regionService.GetModels(a => true) on e.RegionCode equals f.Code into t3
        //                   from g in t3.DefaultIfEmpty()
        //                   join h in _cityService.GetModels(a => true) on g.CityCode equals h.Code into t4
        //                   from i in t4
        //                   select new { a.ID, a.Code, a.Name, a.GisX, a.GisY, a.StageCode, StageName = c.Name, c.ParkCode, ParkName = e.Name, e.RegionCode, RegionName = g.Name, g.CityCode, CityName = i.Name };
        //    if (!string.IsNullOrEmpty(stageCode)) tempList = tempList.Where(a => a.StageCode == stageCode);
        //    else if (!string.IsNullOrEmpty(parkCode)) tempList = tempList.Where(a => a.ParkCode == parkCode);
        //    else if (!string.IsNullOrEmpty(regionCode)) tempList = tempList.Where(a => a.RegionCode == regionCode);
        //    else if (!string.IsNullOrEmpty(cityCode)) tempList = tempList.Where(a => a.CityCode == cityCode);
        //    //var list = tempList.ToList();
        //    return PartialView("_BuildingTable", JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tempList.ToList())));
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_Building building)
        {
            try
            {
                if (building.StageCode == null || building.StageCode == "") return Json(Result.get(4));
                if (_buildingService.Add(building)) return Json(Result.get(1));
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
        //    var temp = from a in _buildingService.GetModels(a => a.ID==id)
        //               join b in _stageService.GetModels(a => true) on a.StageCode equals b.Code into t1
        //               from c in t1.DefaultIfEmpty()
        //               join d in _parkService.GetModels(a => true) on c.ParkCode equals d.Code into t2
        //               from e in t2.DefaultIfEmpty()
        //               join f in _regionService.GetModels(a => true) on e.RegionCode equals f.Code into t3
        //               from g in t3.DefaultIfEmpty()
        //               join h in _cityService.GetModels(a => true) on g.CityCode equals h.Code into t4
        //               from i in t4
        //               select new { a.ID, a.Code, a.Name, a.GisX, a.GisY, a.StageCode, StageName = c.Name, c.ParkCode, ParkName = e.Name, e.RegionCode, RegionName = g.Name, g.CityCode, CityName = i.Name };

        //    return View(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(temp.FirstOrDefault())));
        //}
        [HttpPost]
        public JsonResult Edit(T_Building building)
        {
            try
            {
                if (building.StageCode == null || building.StageCode == "") return Json(Result.get(4));
                if (_buildingService.Update(building)) return Json(Result.get(1));
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
                T_Building building = _buildingService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_buildingService.Delete(building)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }

        #region old

        //// GET: Building/Details/5
        //public ActionResult Details()
        //{
        //    var linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   select new { build, stage, park };
        //    ViewBag.buildList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_BuildingTable");
        //}

        //// POST: Building/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        T_Building building = new T_Building();
        //        building.Code = collection["edit-code"];
        //        building.Name = collection["edit-name"];
        //        building.StageCode = collection["edit-stageName"];
        //        building.GisX = collection["edit-x"];
        //        building.GisY = collection["edit-y"];
        //        if (_buildService.Add(building))
        //            return Content("1:添加成功！");
        //        else
        //            return Content("5:添加失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Building/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var obj = from build in _buildService.GetModels(b => b.ID == id)
        //              join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //              join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //              join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
        //              join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //              select new { build, stage, park, region, city };
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //// POST: Building/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        T_Building building = _buildService.GetModels(b => b.ID == id).FirstOrDefault();
        //        building.Code = collection["edit-code"];
        //        building.Name = collection["edit-name"];
        //        building.StageCode = collection["edit-stageName"];
        //        building.GisX = collection["edit-x"];
        //        building.GisY = collection["edit-y"];
        //        if (_buildService.Update(building))
        //            return Content("1:更新成功！");
        //        else
        //            return Content("5:更新失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Building/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        T_Building building = _buildService.GetModels(b => b.ID == id).FirstOrDefault();
        //        if (_buildService.Delete(building))
        //            return Content("1:删除成功！");
        //        else
        //            return Content("5:删除失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}
        //public ActionResult Select(string cityCode, string regionCode, string parkCode, string stageCode)
        //{
        //    //var lambList = _floorService.GetModels(f => true);
        //    var linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   select new { build, stage, park };
        //    if (!string.IsNullOrEmpty(stageCode))
        //    {
        //        linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   where stage.Code == stageCode
        //                   select new { build, stage, park };
        //        //var text = _buildService.GetModels(b => b.StageCode == stageCode).Select(b => b.Code);
        //        //lambList = lambList.Where(f => text.Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(parkCode))
        //    {
        //        linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   where park.Code == parkCode
        //                   select new { build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //            (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => s.ParkCode == parkCode).Select(s => s.Code).Contains(b.StageCode))
        //        //                       ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(regionCode))
        //    {
        //        linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   where region.Code == regionCode
        //                   select new { build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //             (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => true).Where(s =>
        //        //                     (_parkService.GetModels(p => p.RegionCode == regionCode).Select(p => p.Code).Contains(s.ParkCode))
        //        //                         ).Select(s => s.Code).Contains(b.StageCode))
        //        //                             ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(cityCode))
        //    {
        //        linqList = from build in _buildService.GetModels(b => true)
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   where region.CityCode == cityCode
        //                   select new { build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //             (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => true).Where(s =>
        //        //                    (_parkService.GetModels(p => true).Where(p =>
        //        //                         (_regionService.GetModels(r => r.CityCode == cityCode).Select(r => r.Code).Contains(p.RegionCode))
        //        //                            ).Select(p => p.Code).Contains(s.ParkCode))
        //        //                               ).Select(s => s.Code).Contains(b.StageCode))
        //        //                                  ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }

        //    ViewBag.buildList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_BuildingTable");
        //}

        #endregion
    }
}
