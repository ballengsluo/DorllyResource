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
    public class S_ResourceController : BaseController
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
            var list = dc.Set<V_RSS_Info>().Where(a => a.REnable == true && a.Enable == true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else
                list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.RID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.RName.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.IntType != null) list = list.Where(a => a.BSType == param.IntType);
            if (param.Stime != null)
                list = list.Where(a => param.Stime >= a.BegTime && param.Stime <= a.EndTime);
            else
                list = list.Where(a => DateTime.Now >= a.BegTime && DateTime.Now <= a.EndTime);
            int count = list.Count();
            list = list.OrderByDescending(a => a.BegTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new { a.ID, a.RID, a.RName, a.ResourceKindName, a.GroupName, a.Loc1Name, a.BSType, a.ContactName, a.BegTime, a.EndTime, a.UpUser, a.UpTime });
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
            else
                list = list.Where(a => ParkList.Contains(a.Loc1));
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
            obj.BegTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00");
            obj.EndTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00");
            return View(obj);
        }
        public ActionResult OReserve()
        {
            var obj = new T_ResourceStatus();
            obj.BegTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00");
            obj.EndTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00");
            return View(obj);
        }
        [HttpPost]
        public ActionResult Reserve(int id, FormCollection form)
        {
            try
            {
                DateTime begTime = Convert.ToDateTime(form["Time"].Split('~')[0].Trim());
                DateTime endTime = Convert.ToDateTime(form["Time"].Split('~')[1].Trim());
                string rid=form["RID"];
                bool add = false;
                var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
                if (obj == null)
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.BegTime <= begTime && a.EndTime >= begTime && a.Enable == true && a.RID == rid).Count();
                    if (count > 0) return Json(new Result { Flag = 2, Msg = "存在使用时间冲突,请选择有效时间段!" });
                    obj = new T_ResourceStatus();
                    obj.BSID = Guid.NewGuid().ToString();
                    obj.SysID = 3;
                    obj.Enable = true;
                    add = true;
                }
                else
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.BegTime <= begTime && a.EndTime >= begTime && a.ID != id).Count();
                    if (count > 0) return Json(new Result { Flag = 2, Msg = "存在使用时间冲突,请选择有效时间段!" });
                }
                if (!TryUpdateModel(obj, "", form.AllKeys, new string[] { "ID", "BSID", "SysID" }))
                    return Json(new Result { Flag = 2, Msg = "数据错误！", ExInfo = "来自TryUpdateModel返回false" });
                obj.BegTime = begTime;
                obj.EndTime = endTime;
                obj.UpTime = DateTime.Now;
                obj.UpUser = user.Account;
                if (add) dc.Set<T_ResourceStatus>().Add(obj);
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                return Json(new Result { Flag = 2, Msg = "保存失败！", ExInfo = "来自SaveChanges返回false" });

            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "数据异常！", ExInfo = ex.StackTrace });
            }
        }
        public ActionResult Free(int id)
        {
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
                return Json(new Result { Flag = 2, Msg = "时间格式有误！" });
            }
            var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            obj.Enable = false;
            obj.EndType = Convert.ToInt32(form["EndType"]);
            obj.RealEndTime = endTime;
            if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
            return Json(new Result { Flag = 2, Msg = "保存失败！", ExInfo = "来自SaveChanges返回false" });
        }


    }
}