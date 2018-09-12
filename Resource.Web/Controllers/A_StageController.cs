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
    public class A_StageController : BaseController
    {
        // GET: Stage
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public JsonResult Search(SearchParam param)
        {

            var list = dc.Set<V_Stage>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
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
        public JsonResult Create(T_Stage stage)
        {
            try
            {
                dc.Set<T_Stage>().Add(stage);
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

            var obj = dc.Set<V_Stage>().Where(a => a.ID == id).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public JsonResult Edit(string id, FormCollection form)
        {
            try
            {
                T_Stage stage = dc.Set<T_Stage>().Where(a => a.ID == id).FirstOrDefault();
                if (TryUpdateModel(stage, "", form.AllKeys, new string[] { "Enable" }))
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

                T_Stage stage = dc.Set<T_Stage>().Where(a => a.ID == id).FirstOrDefault();
                dc.Set<T_Stage>().Remove(stage);
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

                T_Stage stage = dc.Set<T_Stage>().Where(a => a.ID == id).FirstOrDefault();
                stage.Enable = true;
                dc.Set<T_Stage>().AddOrUpdate(stage);
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

                T_Stage stage = dc.Set<T_Stage>().Where(a => a.ID == id).FirstOrDefault();
                stage.Enable = false;
                dc.Set<T_Stage>().AddOrUpdate(stage);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }

        public JsonResult GetList(string pid)
        {

            var list = dc.Set<T_Stage>().Where(a => true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.ParkID == pid);
            return Json(list.Select(a => new { a.ID, a.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
