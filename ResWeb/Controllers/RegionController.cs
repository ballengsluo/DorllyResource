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
    public class RegionController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        private IRegionService _regionService = Container.Resolve<IRegionService>();
        // GET: Region
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Details(string cityCode)
        //{
        //    var list = _regionService.GetModels(a => true);
        //    if (!string.IsNullOrEmpty(cityCode)) list = list.Where(a => a.CityCode == cityCode);
        //    var tempList = from a in list
        //                   join b in _cityService.GetModels(a => true) on a.CityCode equals b.Code into t1
        //                   from c in t1.DefaultIfEmpty()
        //                   select new { a.ID, a.Code, a.CityCode, a.Name, CityName = c.Name };
        //    return PartialView("_RegionTable",JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tempList.ToList())) );
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_Region region)
        {
            try
            {
                if (_regionService.Add(region)) return Json(Result.get(1));
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
            T_Region region = _regionService.GetModels(c => c.ID == id).FirstOrDefault();
            return View(region);
        }
        [HttpPost]
        public JsonResult Edit(T_Region region)
        {
            try
            {
                if (region.CityCode == null || region.CityCode=="") return Json(Result.get(2));
                if (_regionService.Update(region)) return Json(Result.get(1));
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
                T_Region region = _regionService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_regionService.Delete(region)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }

        public ActionResult GetDropData(string parentCode,string selectCode)
        {
            var list = _regionService.GetModels(a => true);
            if (!string.IsNullOrEmpty(parentCode)) list = _regionService.GetModels(a => a.CityCode == parentCode);
            if (!string.IsNullOrEmpty(selectCode)) ViewData["dataList"] = new SelectList(list.ToList(), "RegionCode", "RegionName", selectCode);
            else ViewData["dataList"] = new SelectList(list.ToList(), "RegionCode", "RegionName");
            return PartialView("_RegionDrop");
        }

        #region old
        //// GET: Region/Details/5
        //public ActionResult Details()
        //{
        //    var obj = from region in _regionService.GetModels(p => true)
        //              join region in _regionService.GetModels(c => true) on region.CityCode equals region.Code
        //              select new { region, region };
        //    ViewBag.regionList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
        //    return PartialView("_RegionTable");
        //}

        //// POST: Region/Create
        //[HttpPost]
        //public ContentResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        T_Region region = new T_Region();
        //        region.Code = collection["edit-code"];
        //        region.Name = collection["edit-name"];
        //        region.CityCode = collection["edit-region"];
        //        if (_regionService.Add(region))
        //            return Content("1:添加成功！");
        //        else
        //            return Content("5:添加失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //GET: Region/Edit/5
        //public JsonResult Edit(int id)
        //{
        //    var obj = from region in _regionService.GetModels(p => p.ID == id)
        //              join region in _regionService.GetModels(c => true) on region.CityCode equals region.Code
        //              select new { region, region };
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //// POST: Region/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        T_Region region = _regionService.GetModels(r => r.ID == id).FirstOrDefault();
        //        region.Code = collection["edit-code"];
        //        region.Name = collection["edit-name"];
        //        region.CityCode = collection["edit-region"];
        //        if (_regionService.Update(region))
        //            return Content("1:修改成功！");
        //        else
        //            return Content("5:修改失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //// GET: Region/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        T_Region region = _regionService.GetModels(r => r.ID == id).FirstOrDefault();
        //        if (_regionService.Delete(region))
        //            return Content("1:删除成功！");
        //        else
        //            return Content("5:删除失败！");
        //    }
        //    catch
        //    {
        //        return Content("3:数据异常！");
        //    }
        //}

        //public ActionResult Select(string regionCode)
        //{
        //    var linqList = from region in _regionService.GetModels(p => true)
        //                   join region in _regionService.GetModels(c => true) on region.CityCode equals region.Code
        //                   select new { region, region };
        //    if (!string.IsNullOrEmpty(regionCode))
        //    {
        //        linqList = from region in _regionService.GetModels(p => true)
        //                   join region in _regionService.GetModels(c => true) on region.CityCode equals region.Code
        //                   where region.CityCode == regionCode
        //                   select new { region, region };
        //    }
        //    ViewBag.regionList = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(linqList));
        //    return PartialView("_RegionTable");
        //}

        #endregion
    }
}
