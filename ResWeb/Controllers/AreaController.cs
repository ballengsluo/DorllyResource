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
    public class AreaController : Controller
    {
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IParkService _parkService = Container.Resolve<IParkService>();
        private IStageService _stageService = Container.Resolve<IStageService>();
        private IBuildingService _buildService = Container.Resolve<IBuildingService>();
        private IFloorService _floorService = Container.Resolve<IFloorService>();
        // GET: Area
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<T_City> cityList = _cityService.GetModels(c => true).ToList();
            List<T_Park> parkList = _parkService.GetModels(c => true).ToList();
            ViewData["cityList"] = new SelectList(cityList, "Code", "Name");
            ViewData["parkList"] = new SelectList(parkList, "Code", "Name");
            return View();
        }

        #region 城市操作
        public ActionResult CityTable()
        {
            ViewBag.cityList = _cityService.GetModels(c => true).ToList();
            return PartialView("_CityTable");
        }

        [HttpPost]
        public ContentResult AddCity(string code, string name)
        {
            T_City city = new T_City();
            city.Code = code;
            city.Name = name;
            if (_cityService.Add(city))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }

        public JsonResult GetCity(int id)
        {
            T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
            return Json(city, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectCity(string name)
        {
            List<T_City> cityList = _cityService.GetModels(c => c.Name == name).ToList();
            ViewBag.cityList = cityList;
            return PartialView("_CityTable");
        }
        [HttpPost]
        public ContentResult UpdateCity(int id, string code, string name)
        {
            T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
            city.Code = code;
            city.Name = name;
            if (_cityService.Update(city))
                return Content("1:修改成功！");
            else
                return Content("5:添加失败！");
        }

        public ContentResult DeleteCity(int id)
        {
            T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
            if (_cityService.Delete(city))
                return Content("1:修改成功！");
            else
                return Content("5:添加失败！");
        }

        #endregion

        #region 行政区域操作

        public ActionResult RegionTable()
        {
            var lambList = _regionService.GetModels(r => true).Join(_cityService.GetModels(c => true), region => region.CityCode, city => city.Code, (region, city) => new { region, city }).ToList();
            ViewBag.regionList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lambList));
            return PartialView("_RegionTable");
        }
        [HttpPost]
        public ContentResult AddRegion(string code, string cityCode, string name)
        {
            T_Region region = new T_Region();
            region.Code = code;
            region.CityCode = cityCode;
            region.Name = name;
            if (_regionService.Add(region))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }
        #endregion

        #region 园区操作
        public ActionResult ParkTable()
        {
            var lambList = from park in _parkService.GetModels(p => true)
                           join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
                           join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
                           select new { park, region, city };

            ViewBag.parkList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lambList));
            return PartialView("_ParkTable");
        }

        [HttpPost]
        public ContentResult AddPark(FormCollection form)
        {
            T_Park park = new T_Park();

            if (_parkService.Add(park))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }
        #endregion

        #region 建设期操作
        public ActionResult StageTable()
        {
            var lambList = from stage in _stageService.GetModels(s => true)
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { stage, park };

            ViewBag.stageList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lambList));
            return PartialView("_StageTable");
        }

        [HttpPost]
        public ContentResult AddStage(string code, string parkCode, string name)
        {
            T_Stage stage = new T_Stage();
            stage.Code = code;
            stage.ParkCode = parkCode;
            stage.Name = name;
            if (_stageService.Add(stage))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }
        #endregion

        #region 建筑操作
        public ActionResult BuildingTable()
        {
            var lambList = from build in _buildService.GetModels(b => true)
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { build, stage, park };
            ViewBag.buildList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lambList));
            return PartialView("_BuildingTable");
        }

        [HttpPost]
        public ContentResult AddBuilding(string code, string stageCode, string name)
        {
            T_Building building = new T_Building();
            building.Code = code;
            building.StageCode = stageCode;
            building.Name = name;
            if (_buildService.Add(building))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }

        #endregion

        #region 楼层操作

        [HttpPost]
        public ContentResult AddFloor(FormCollection form)
        {
            T_Floor floor = new T_Floor();
            if (_floorService.Add(floor))
                return Content("1:添加成功！");
            else
                return Content("5:添加失败！");
        }
        public ActionResult FloorTable()
        {
            var lambList = from floor in _floorService.GetModels(f => true)
                           join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
                           join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
                           join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
                           select new { floor, build, stage, park };
            ViewBag.floorList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lambList));
            return PartialView("_FloorTable");
        }
        #endregion






    }
}