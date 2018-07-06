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
    public class Result
    {
        public static object get(int type)
        {
            switch (type)
            {
                case 1:
                    return new { result = 1, msg = "操作成功！" };
                case 2:
                    return new { result = 5, msg = "操作失败！" };
                case 3:
                    return new { result = 2, msg = "数据异常！" };
                case 4:
                    return new { result = 2, msg = "参数错误！" };
            }
            return new { result = 3, msg = "错误！" };
        }

    }
    public class CityController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string name)
        {
            var list = _cityService.GetModels(a => true);
            if (!string.IsNullOrEmpty(name)) list = list.Where(a => a.CityName.Contains(name));
            ViewBag.cityList = list.ToList();
            return PartialView("_CityTable");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(T_City city)
        {
            try
            {
                if (_cityService.Add(city)) return Json(Result.get(1));
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
            T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
            return View(city);
        }
        [HttpPost]
        public JsonResult Edit(T_City city)
        {
            try
            {
                if (_cityService.Update(city)) return Json(Result.get(1));
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
                T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_cityService.Delete(city)) return Json(Result.get(1));
                else return Json(Result.get(2));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return Json(Result.get(3));
            }
        }
        public ActionResult GetDropData(string selectCode)
        {
            var list = _cityService.GetModels(f => true).ToList();
            if (!string.IsNullOrEmpty(selectCode)) ViewData["dataList"] = new SelectList(list, "CityCode", "CityName", selectCode);
            else ViewData["dataList"] = new SelectList(list, "CityCode", "CityName");
            return PartialView("_CityDrop");
        }
    }
}
