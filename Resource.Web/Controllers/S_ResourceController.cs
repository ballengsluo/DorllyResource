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
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }
        public ActionResult Search(SearchParam param)
        {
            var list = dc.Set<V_RSS_Info>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ResourceID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.ResourceName.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.IType != null) list = list.Where(a => a.BusinessType == param.IType);
            if (param.Enable != null) list = list.Where(a => a.Enable == param.Enable);
            if (param.Stime != null) list = list.Where(a => param.Stime <= a.RentEndTime && param.Etime >= a.RentBeginTime);
            int count = list.Count();
            list = list.OrderByDescending(a => a.RentBeginTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new
            {
                a.ID,
                a.Enable,
                a.SysID,
                a.ResourceID,
                a.ResourceName,
                a.ResourceKindName,
                a.GroupName,
                a.Loc1Name,
                a.BusinessType,
                a.CustLongName,
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
            var list = dc.Set<V_RS_Info>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            int count = list.Count();
            list = list.OrderByDescending(a => a.ResourceKindID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new { a.ID, a.Name, a.Loc1Name, a.GroupName });
            var obj = JsonConvert.SerializeObject(new { count = count, data = result.ToList() });
            return Content(obj);
        }
        public ActionResult IReserve()
        {
            var obj = new T_ResourceStatus();
            obj.RentBeginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00");
            obj.RentEndTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00");
            return View(obj);
        }
        public ActionResult OReserve()
        {
            var obj = new T_ResourceStatus();
            obj.RentBeginTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00");
            obj.RentEndTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00");
            return View(obj);
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
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.RentBeginTime <= begTime
                        && a.RentEndTime >= begTime
                        && a.Enable == true
                        && a.ResourceID == rid).Count();
                    if (count > 0) return Json(Result.Fail(msg: "存在使用时间冲突,请选择有效时间段!"));
                    obj = new T_ResourceStatus();
                    obj.BusinessID = Guid.NewGuid().ToString();
                    obj.SysID = 3;
                    obj.Enable = true;
                    add = true;
                }
                else
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.RentBeginTime <= begTime
                        && a.RentEndTime >= begTime && a.ID != id).Count();
                    if (count > 0) return Json(Result.Fail(msg: "存在使用时间冲突,请选择有效时间段!"));
                }
                if (!TryUpdateModel(obj, "", form.AllKeys, new string[] { "ID", "BusinessID", "SysID" }))
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
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult Free(int id, FormCollection form)
        {
            DateTime endTime = DateTime.Now;
            try
            {
                endTime = Convert.ToDateTime(form["RealEndTime"]);
            }
            catch (Exception)
            {
                return Json(Result.Fail(msg:"时间格式有误！"));
            }
            var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            obj.Enable = false;
            obj.EndType = Convert.ToInt32(form["EndType"]);
            obj.RentEndRealTime = endTime;
            obj.UpdateTime = DateTime.Now;
            obj.UpdateUser = user.Account;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            return Json(Result.Fail());
        }
    }
}