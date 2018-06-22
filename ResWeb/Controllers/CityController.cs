﻿using Resource.BLL;
using Resource.IBLL;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class CityController : Controller
    {
        private ICityService _cityService = Container.Resolve<ICityService>();
        // GET: City
        public ActionResult Index()
        {
            ViewBag.cityList = _cityService.GetModels(c => true).ToList();
            return View();
        }

        // GET: City/Details/5
        public ActionResult Details()
        {
            ViewBag.cityList = _cityService.GetModels(c => true).ToList();
            return PartialView("_CityTable");
        }

        // POST: City/Create
        [HttpPost]
        public ContentResult Create(FormCollection collection)
        {
            try
            {
                T_City city = new T_City();
                city.Code = collection["edit-code"];
                city.Name = collection["edit-name"];
                if (_cityService.Add(city))
                    return Content("1:添加成功！");
                else
                    return Content("5:添加失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: City/Edit/5
        public JsonResult Edit(int id)
        {
            T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
            return Json(city, JsonRequestBehavior.AllowGet);
        }

        // POST: City/Edit/5
        [HttpPost]
        public ContentResult Edit(int id, FormCollection collection)
        {
            try
            {
                T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
                city.Code = collection["edit-code"];
                city.Name = collection["edit-name"];
                if (_cityService.Update(city))
                    return Content("1:更新成功！");
                else
                    return Content("5:更新失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        // GET: City/Delete/5
        public ContentResult Delete(int id)
        {
            try
            {
                T_City city = _cityService.GetModels(c => c.ID == id).FirstOrDefault();
                if (_cityService.Delete(city))
                    return Content("1:删除成功！");
                else
                    return Content("5:删除失败！");
            }
            catch
            {
                return Content("3:数据异常！");
            }
        }

        public ActionResult Select(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ViewBag.cityList = _cityService.GetModels(c => true).ToList();
            }
            else
            {
                ViewBag.cityList = _cityService.GetModels(c => c.Name == name).ToList();
            }
            return PartialView("_CityTable");
        }

    }
}
