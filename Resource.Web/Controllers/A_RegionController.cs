using Newtonsoft.Json;
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
    public class A_RegionController : BaseController
    {
        // GET: Region
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public JsonResult Search(SearchParam param)
        {
            
            var list = from a in dc.Set<T_Region>()
                       join b in dc.Set<T_City>() on a.CityID equals b.ID into t1
                       from city in t1.DefaultIfEmpty()
                       select new
                       {
                           a.ID,
                           a.CityID,
                           a.Name,
                           a.Enable,
                           CityName = city.Name
                       };
            if (!string.IsNullOrEmpty(param.City)) list = list.Where(a => a.CityID == param.City);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (param.Enable != null) list = list.Where(a => a.Enable == param.Enable);
            int count = list.Count();
            list = list.OrderBy(a => a.ID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count = count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
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
                region.Enable = true;
                dc.Set<T_Region>().Add(region);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Edit(string id)
        {
            
            var obj = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(region, "", form.AllKeys, new string[] { "Enable" }))
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
                
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Region>().Remove(region);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public JsonResult Open(string id)
        {
            try
            {
                
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = true;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public JsonResult Close(string id)
        {
            try
            {
                
                T_Region region = dc.Set<T_Region>().Where(a => a.ID == id).FirstOrDefault();
                region.Enable = false;
                dc.Set<T_Region>().AddOrUpdate(region);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
    }
}
