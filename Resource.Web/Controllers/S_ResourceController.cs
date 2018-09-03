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
            if (param.Type != null) list = list.Where(a => a.BSType == param.Type);
            if (param.Stime != null)
                list = list.Where(a => param.Stime >= a.BegTime && param.Stime <= a.EndTime);
            else
                list = list.Where(a => DateTime.Now >= a.BegTime && DateTime.Now <= a.EndTime);
            int count = list.Count();
            list = list.OrderByDescending(a => a.BegTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            var result = list.Select(a => new { a.ID, a.RID, a.RName, a.ResourceKindName, a.GroupName, a.Loc1Name, a.BSType, a.CustSName, a.BegTime, a.EndTime, a.UpUser, a.UpTime });
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = result.ToList() }, setting).Replace(" 00:00", "");
            return Content(obj);
        }
        public ActionResult Reserve(int? id)
        {
            var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            if (obj == null)
            {
                obj = new T_ResourceStatus();
                obj.BegTime = DateTime.Now;
                obj.EndTime = DateTime.Now.AddDays(1);
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Reserve(int id, FormCollection form)
        {
            try
            {
                DateTime begTime = Convert.ToDateTime(form["Time"].Split('~')[0].Trim());
                DateTime endTime = Convert.ToDateTime(form["Time"].Split('~')[1].Trim());
                var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
                if (obj == null)
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.BegTime <= begTime && a.EndTime >= begTime).Count();
                    if (count > 0) return Json(new Result { Flag = 2, Msg = "存在使用时间冲突,请选择有效时间段!" });
                    obj = new T_ResourceStatus();
                    obj.BSID = Guid.NewGuid().ToString();
                    obj.SYSID = 1;

                }
                else
                {
                    int count = dc.Set<T_ResourceStatus>().Where(a => a.BegTime <= begTime && a.EndTime >= begTime && a.ID != id).Count();
                    if (count > 0) return Json(new Result { Flag = 2, Msg = "存在使用时间冲突,请选择有效时间段!" });
                }
                if (!TryUpdateModel(obj, "", form.AllKeys, new string[] { "ID", "BSID", "SYSID" }))
                    return Json(new Result { Flag = 2, Msg = "数据错误！", ExInfo = "来自TryUpdateModel返回false" });
                obj.BegTime = begTime;
                obj.EndTime = endTime;
                obj.UpTime = DateTime.Now;
                obj.UpUser = user.Account;
                if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
                return Json(new Result { Flag = 2, Msg = "保存失败！", ExInfo = "来自SaveChanges返回false" });

            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "数据异常！", ExInfo = ex.StackTrace });
            }
        }
        public ActionResult Cancle(int id)
        {
            var obj = dc.Set<T_ResourceStatus>().Where(a => a.ID == id).FirstOrDefault();
            obj.Enable = false;
            if (dc.SaveChanges() > 0) return Json(new Result { Flag = 1, Msg = "保存成功！" });
            return Json(new Result { Flag = 2, Msg = "保存失败！", ExInfo = "来自SaveChanges返回false" });
        }


    }
}