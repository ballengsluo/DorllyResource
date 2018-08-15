﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Resource.Model;
using Newtonsoft.Json;
using System.Data.Entity;
using Resource.Web.Models;
using System.Data.Entity.Migrations;

namespace Resource.Web.Controllers
{
    public class A_BuildingController : Controller
    {

        // GET: Building
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
        public JsonResult Create(T_Building building)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                dc.Set<T_Building>().Add(building);
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
            var obj = dc.Set<V_Building>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(building, "", form.AllKeys, new string[] { "Enable" }))
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
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Building>().Remove(building);
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
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                building.Enable = true;
                dc.Set<T_Building>().AddOrUpdate(building);
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
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                building.Enable = false;
                dc.Set<T_Building>().AddOrUpdate(building);
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
            var list = dc.Set<V_Building>().Where(a => true);
            if (!string.IsNullOrEmpty(param.StageID)) list = list.Where(a => a.StageID == param.StageID);
            else if (!string.IsNullOrEmpty(param.ParkID)) list = list.Where(a => a.ParkID == param.ParkID);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList(string pID)
        {
            DbContext dc = DbContextFactory.Create();
            var list = dc.Set<T_Building>().Where(a => true);
            if (!string.IsNullOrEmpty(pID)) list = list.Where(a => a.StageID == pID);
            return Json(list.Select(a => new { a.ID, a.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}