using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model;
using Newtonsoft.Json;
namespace Resource.Web.Controllers
{
    public class S_ResourceController : ResourceBusinessController
    {
        // GET: S_Resource
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            var list = dc.Set<V_ResourceStatus>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ResourceID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.ResourceName.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.IType != null) list = list.Where(a => a.BusinessType == param.IType);
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            if (param.Stime != null) list = list.Where(a => param.Stime <= a.RentEndTime && param.Etime >= a.RentBeginTime);
            int count = list.Count();
            list = list.OrderByDescending(a => a.RentBeginTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new
            {
                a.ID,
                a.SysID,
                a.Status,
                a.ResourceID,
                a.ResourceName,
                a.ResourceKindName,
                a.GroupName,
                a.Loc1Name,
                a.BusinessType,
                a.CustShortName,
                a.RentBeginTime,
                a.RentEndTime,
                a.UpdateTime,
                a.UpdateUser
            });
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = result.ToList() }, setting).Replace(" 00:00", "");
            return Content(obj);
        }
        public ActionResult RSIndex()
        {
            return View();
        }
        public ActionResult RSSearch(SearchParam param)
        {
            var list = dc.Set<V_Resource>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            int count = list.Count();
            list = list.OrderByDescending(a => a.ResourceKindID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new { a.ID, a.Name, a.Loc1Name, a.GroupName,a.RentArea });
            var obj = JsonConvert.SerializeObject(new { count = count, data = result.ToList() });
            return Content(obj);
        }
        public ActionResult MUse()
        {
            return View();
        }
        public ActionResult CUse()
        {
            return View();
        }
        [HttpPost]
        public JsonResult MUse(FormCollection form)
        {
            try
            {
                T_ResourceStatus status = new T_ResourceStatus();
                if (!TryUpdateModel(status, "", form.AllKeys))
                    return Json(Result.Fail(msg: "操作数据失败,请检查数据的准确性！"));
                var begin = status.RentBeginTime;
                var end = status.RentEndTime;
                if (begin > end || CheckTime(begin, end,status.ResourceID))
                    return Json(Result.Fail(msg: "时间冲突,请选择正确的时间！"));
                status.SysID = 3;
                status.BusinessID = Guid.NewGuid().ToString();
                status.BusinessType = 6;
                status.Status = 2;
                status.UpdateTime = DateTime.Now;
                status.UpdateUser = user.Account;
                dc.Set<T_ResourceStatus>().Add(status);
                dc.SaveChanges();
                return Json(Result.Success());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public JsonResult CUse(FormCollection form)
        {
            try
            {
                T_ResourceStatus status = new T_ResourceStatus();
                if (!TryUpdateModel(status, "", form.AllKeys))
                    return Json(Result.Fail(msg: "操作数据失败,请检查数据的准确性！"));
                var begin = status.RentBeginTime;
                var end = status.RentEndTime;
                if (begin > end || CheckTime(begin, end,status.ResourceID))
                    return Json(Result.Fail(msg: "时间冲突,请选择正确的时间！"));
                status.SysID = 3;
                status.BusinessID = Guid.NewGuid().ToString();
                status.BusinessType = 5;
                status.Status = 1;
                status.UpdateTime = DateTime.Now;
                status.UpdateUser = user.Account;
                dc.Set<T_ResourceStatus>().Add(status);
                dc.SaveChanges();
                return Json(Result.Success());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }

        [HttpPost]
        public ActionResult Reserve(int id, FormCollection form)
        {
            try
            {
                DateTime begTime = Convert.ToDateTime(form["Time"].Split('~')[0].Trim());
                DateTime endTime = Convert.ToDateTime(form["Time"].Split('~')[1].Trim());
                string rid = form["ResourceID"];
                bool add = false;
                var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
                if (obj == null)
                {
                    //开始时间处于区间
                    int count = dc.Set<T_ResourceStatus>().Where(a =>
                        (begTime > a.RentBeginTime && begTime < a.RentEndTime) ||
                        (endTime > a.RentBeginTime && endTime < a.RentEndTime) &&
                        a.ResourceID == rid).Count();
                    if (count > 0) return Json(Result.Fail(msg: "存在使用时间冲突,请选择有效时间段!"));
                    obj = new T_ResourceStatus();
                    obj.SysID = 3;
                    obj.BusinessID = Guid.NewGuid().ToString();
                    obj.BusinessType = 5;
                    add = true;
                }
                else
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a =>
                        (begTime > a.RentBeginTime && begTime < a.RentEndTime) ||
                        (endTime > a.RentBeginTime && endTime < a.RentEndTime) &&
                        a.ResourceID == rid && a.ID != id).Count();
                    if (count > 0) return Json(Result.Fail(msg: "存在使用时间冲突,请选择有效时间段!"));
                }
                if (!TryUpdateModel(obj, "", form.AllKeys, new string[] { "ID", "BusinessID", "SysID", "BusinessType", "Enable" }))
                    return Json(Result.Fail());
                obj.RentBeginTime = begTime;
                obj.RentEndTime = endTime;
                obj.UpdateTime = DateTime.Now;
                obj.UpdateUser = user.Account;
                if (add) dc.Set<T_ResourceStatus>().Add(obj);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Free(int id)
        {
            var status = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            return View(status);
        }
        [HttpPost]
        public ActionResult Free(int id, FormCollection form)
        {
            DateTime endTime = DateTime.Now;
            var status = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            if (status.SysID != 3) return Json(Result.Fail(msg: "业务系统数据不允许更改！"));
            try
            {
                endTime = Convert.ToDateTime(form["RentEndTime"]);
            }
            catch (Exception)
            {
                return Json(Result.Fail(msg: "时间格式有误！"));
            }
            try
            {
                if ((endTime <= status.RentBeginTime && status.RentBeginTime< endTime) ||
                    dc.Set<T_ResourceStatus>().Where(a => a.ID != status.ID && a.RentBeginTime <= endTime && a.RentEndTime <= endTime).Count() > 0)
                {
                    return Json(Result.Fail(msg: "结束时间和其他占用时间有冲突！"));
                }
                status.RentEndTime = endTime;
                status.Status = 3;
                dc.SaveChanges();
                return Json(Result.Success());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }

        }

        public bool CheckTime(DateTime begin, DateTime end,string resourceID)
        {
            bool success = false;
            int count = dc.Set<T_ResourceStatus>().Where(a => a.ResourceID==resourceID &&
                        ((a.RentBeginTime <= begin && begin <= a.RentEndTime) ||
                        (a.RentBeginTime <= end && end <= a.RentEndTime))
                        ).Count();
            if (count > 0) success = true;
            return success;
        }
    }
}