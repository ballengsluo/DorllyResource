using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
using Resource.DAL;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ResWeb.Controllers
{
    public class FloorController : Controller
    {
        private IFloorService _FloorService = Container.Resolve<IFloorService>();
        // GET: Floor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDropData(string parentCode, string selectCode)
        {
            var list = _FloorService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _FloorService.GetModels(a => a.BuildingCode == parentCode);
            if (!string.IsNullOrEmpty(selectCode)) ViewData["dataList"] = new SelectList(list.ToList(), "FloorCode", "FloorName", selectCode);
            else ViewData["dataList"] = new SelectList(list.ToList(), "FloorCode", "FloorName");
            return PartialView("_FloorDrop");
        }
        public object GetData(int? id, string cityCode, string regionCode, string parkCode, string stageCode, string buildingCode)
        {
            StringBuilder sqlText = new StringBuilder(@"SELECT F.*,B.BuildingName,
                                                                                    S.StageCode,S.StageName,
                                                                                    P.ParkCode,P.ParkShortName,
                                                                                    R.RegionCode,R.RegionName,
                                                                                    C.CityCode,C.CityName FROM T_Floor F
                                                                                    Left Join T_Building B On F.BuildingCode=B.BuildingCode
                                                                                    Left Join T_Stage S On B.StageCode= s.StageCode
                                                                                    Left Join T_Park P On S.ParkCode= P.ParkCode
                                                                                    Left Join T_Region R On P.RegionCode= R.RegionCode
                                                                                    Left Join T_City C On R.CityCode= C.CityCode");
            List<SqlParameter> pList = new List<SqlParameter>();
            if (id != null)
            {
                sqlText.Append(" Where F.ID=@id");
                pList.Add(new SqlParameter("@id", id));
                var a = _FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0];
                //var row = _FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0].Rows[0];
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(a));

            }
            else if (!string.IsNullOrEmpty(buildingCode))
            {

                sqlText.Append(" Where B.BuildingCode=@buildingCode");
                pList.Add(new SqlParameter("@buildingCode", buildingCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0]));

            }
            else if (!string.IsNullOrEmpty(stageCode))
            {
                sqlText.Append(" Where S.StageCode=@stageCode");
                pList.Add(new SqlParameter("@stageCode", stageCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0]));

            }
            else if (!string.IsNullOrEmpty(parkCode))
            {
                sqlText.Append(" Where P.ParkCode=@parkCode");
                pList.Add(new SqlParameter("@parkCode", parkCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0]));

            }
            else if (!string.IsNullOrEmpty(regionCode))
            {
                sqlText.Append(" Where R.RegionCode=@regionCode");
                pList.Add(new SqlParameter("@regionCode", regionCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0]));

            }
            else if (!string.IsNullOrEmpty(cityCode))
            {
                sqlText.Append(" Where C.CityCode=@cityCode");
                pList.Add(new SqlParameter("@cityCode", cityCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text, pList.ToArray()).Tables[0]));
            }
            else
            {
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_FloorService.ExecuteSql(sqlText.ToString(), CommandType.Text).Tables[0]));
            }
        }

        public ActionResult Details(string cityCode, string regionCode, string parkCode, string stageCode, string buildingCode)
        {
            return PartialView("_FloorTable", GetData(null, cityCode, regionCode, parkCode, stageCode, buildingCode));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_Floor floor)
        {
            try
            {
                if (floor.BuildingCode == null || floor.BuildingCode == "") return Json(Result.get(4));
                if (_FloorService.Add(floor)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }
        public ActionResult Edit(int id)
        {
            //var temp = from a in _floorService.GetModels(a => true)
            //           join temp1 in _buildingService.GetModels(a => true) on a.BuildingCode equals temp1.Code into t0
            //           from b in t0
            //           join temp2 in _stageService.GetModels(a => true) on b.StageCode equals temp2.Code into t1
            //           from c in t1.DefaultIfEmpty()
            //           join temp3 in _parkService.GetModels(a => true) on c.ParkCode equals temp3.Code into t2
            //           from d in t2.DefaultIfEmpty()
            //           join temp4 in _regionService.GetModels(a => true) on d.RegionCode equals temp4.Code into t3
            //           from e in t3.DefaultIfEmpty()
            //           join temp5 in _cityService.GetModels(a => true) on e.CityCode equals temp5.Code into t4
            //           from f in t4
            //           select new { a.ID, a.Code, a.Name, a.BuildingCode, BuildingName = b.Name, b.StageCode, StageName = c.Name, c.ParkCode, ParkName = d.Name, d.RegionCode, RegionName = e.Name, e.CityCode, CityName = f.Name };

            return View(GetData(id, "", "", "", "", ""));
        }
        [HttpPost]
        public JsonResult Edit(T_Floor floor)
        {
            try
            {
                if (floor.BuildingCode == null || floor.BuildingCode == "") return Json(Result.get(4));
                if (_FloorService.Update(floor)) return Json(Result.get(1));
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
                T_Floor floor = _FloorService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_FloorService.Delete(floor)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }


        #region old
        //public ActionResult Details()
        //{
        //    var linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   select new { floor, build, stage, park };
        //    ViewBag.floorList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return View("_FloorTable");
        //}



        //// POST: Floor/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        T_Floor floor = new T_Floor();
        //        floor.Code = collection["edit-code"];
        //        floor.Name = collection["edit-name"];
        //        floor.BuildingCode = collection["edit-build"];
        //        if (_floorService.Add(floor))
        //            return Content("1:添加成功！");
        //        else
        //            return Content("5:添加失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Floor/Edit/5
        //public JsonResult Edit(int id)
        //{
        //    var linqList = from floor in _floorService.GetModels(f => f.ID == id)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   join region in _regionService.GetModels(r => true) on park.RegionCode equals region.Code
        //                   join city in _cityService.GetModels(c => true) on region.CityCode equals city.Code
        //                   select new { floor, build, stage, park, region, city };
        //    //var floorObject = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList)); 
        //    return Json(linqList, JsonRequestBehavior.AllowGet);
        //}

        //// POST: Floor/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        T_Floor floor = _floorService.GetModels(f => f.ID == id).FirstOrDefault();
        //        floor.Code = collection["edit-code"];
        //        floor.Name = collection["edit-name"];
        //        floor.BuildingCode = collection["edit-build"];
        //        if (_floorService.Update(floor))
        //            return Content("1:添加成功！");
        //        else
        //            return Content("5:添加失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Floor/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        T_Floor floor = _floorService.GetModels(f => f.ID == id).FirstOrDefault();
        //        if (_floorService.Delete(floor))
        //            return Content("1:删除成功！");
        //        else
        //            return Content("5:删除失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}


        //public ActionResult Select(string cityCode, string regionCode, string parkCode, string stageCode, string buildingCode)
        //{
        //    //var lambList = _floorService.GetModels(f => true);
        //    var linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   select new { floor, build, stage, park };
        //    if (!string.IsNullOrEmpty(buildingCode))
        //    {
        //        linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   where build.Code == buildingCode
        //                   select new { floor, build, stage, park };
        //        //lambList = lambList.Where(f => f.BuildingCode == buildingCode);
        //    }
        //    else if (!string.IsNullOrEmpty(stageCode))
        //    {
        //        linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   where stage.Code == stageCode
        //                   select new { floor, build, stage, park };

        //        //var text = _buildService.GetModels(b => b.StageCode == stageCode).Select(b => b.Code);
        //        //lambList = lambList.Where(f => text.Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(parkCode))
        //    {
        //        linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   where park.Code == parkCode
        //                   select new { floor, build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //            (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => s.ParkCode == parkCode).Select(s => s.Code).Contains(b.StageCode))
        //        //                       ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(regionCode))
        //    {
        //        linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   where region.Code == regionCode
        //                   select new { floor, build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //             (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => true).Where(s =>
        //        //                     (_parkService.GetModels(p => p.RegionCode == regionCode).Select(p => p.Code).Contains(s.ParkCode))
        //        //                         ).Select(s => s.Code).Contains(b.StageCode))
        //        //                             ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }
        //    else if (!string.IsNullOrEmpty(cityCode))
        //    {
        //        linqList = from floor in _floorService.GetModels(f => true)
        //                   join build in _buildService.GetModels(b => true) on floor.BuildingCode equals build.Code
        //                   join stage in _stageService.GetModels(s => true) on build.StageCode equals stage.Code
        //                   join park in _parkService.GetModels(p => true) on stage.ParkCode equals park.Code
        //                   join region in _regionService.GetModels(p => true) on park.RegionCode equals region.Code
        //                   where region.CityCode == cityCode
        //                   select new { floor, build, stage, park };
        //        //lambList = lambList.Where(f =>
        //        //             (_buildService.GetModels(b => true).Where(b =>
        //        //                 (_stageService.GetModels(s => true).Where(s =>
        //        //                    (_parkService.GetModels(p => true).Where(p =>
        //        //                         (_regionService.GetModels(r => r.CityCode == cityCode).Select(r => r.Code).Contains(p.RegionCode))
        //        //                            ).Select(p => p.Code).Contains(s.ParkCode))
        //        //                               ).Select(s => s.Code).Contains(b.StageCode))
        //        //                                  ).Select(b => b.Code)).Contains(f.BuildingCode));
        //    }

        //    ViewBag.floorList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_FloorTable");
        //}

        #endregion

        #region 下拉框联动

        //public JsonResult GetRegion(string pcode)
        //{
        //    var lambList = _regionService.GetModels(z => z.CityCode == pcode).ToList();
        //    return Json(lambList, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetPark(string pcode)
        //{
        //    var lambList = _parkService.GetModels(z => z.RegionCode == pcode).ToList();
        //    return Json(lambList, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetStage(string pcode)
        //{
        //    var lambList = _stageService.GetModels(z => z.ParkCode == pcode).ToList();
        //    return Json(lambList, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetBuilding(string pcode)
        //{
        //    var lambList = _buildingService.GetModels(z => z.StageCode == pcode).ToList();
        //    return Json(lambList, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}
