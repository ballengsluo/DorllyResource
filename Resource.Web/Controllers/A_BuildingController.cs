using System;
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
    public class A_BuildingController : BaseController
    {
        // GET: Building
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public JsonResult Search(SearchParam param)
        {
            
            var list = dc.Set<V_Building>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Stage)) list = list.Where(a => a.StageID == param.Stage);
            else if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
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
                building.Enable = true;
                dc.Set<T_Building>().Add(building);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                else return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Edit(string id)
        {
            
            var obj = dc.Set<V_Building>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(building, "", form.AllKeys, new string[] { "Enable" }))
                {
                    if (dc.SaveChanges() > 0) return Json(Result.Success());
                }
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public JsonResult Del(string id)
        {
            try
            {
                
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Building>().Remove(building);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public JsonResult Open(string id)
        {
            try
            {
                
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                building.Enable = true;
                dc.Set<T_Building>().AddOrUpdate(building);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public JsonResult Close(string id)
        {
            try
            {
                
                T_Building building = dc.Set<T_Building>().Where(a => a.ID == id).FirstOrDefault();
                building.Enable = false;
                dc.Set<T_Building>().AddOrUpdate(building);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                else return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
    }
}
