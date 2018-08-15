﻿using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class A_RegionController : Controller
    {
        // GET: Region
        public ActionResult Index()
        {
            T_User user = RouteData.Values["user"] as T_User;
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(T_Region region)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                dc.Set<T_Region>().Add(region);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        public ActionResult Edit(string id)
        {
            DbContext dc = DbContextFactory.Create();
            var obj = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(region, "", form.AllKeys, new string[] { "Enable" }))
                {
                    if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                }
                return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }

        }

        [HttpPost]
        public JsonResult Del(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Region>().Remove(region);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        public JsonResult Open(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = true;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        public JsonResult Close(string id)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = false;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        public JsonResult Search(SearchParam param)
        {
            DbContext dc = DbContextFactory.Create();
            var list = from a in dc.Set<T_Region>()
                       join b in dc.Set<T_City>() on a.CityID equals b.ID into t1
                       from city in t1.DefaultIfEmpty()
                       select new { a.ID, a.CityID, a.Name, a.Enable, CityName = city.Name };
            if (!string.IsNullOrEmpty(param.CityID)) list = list.Where(a => a.CityID == param.CityID);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList(string pid)
        {
            DbContext dc = DbContextFactory.Create();
            var list = dc.Set<T_Region>().Where(a => true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.CityID == pid);
            return Json(list.Select(a => new { a.ID, a.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}