using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class OrderController : ResourceBusinessController
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            var list = dc.Set<V_Order>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            else list = list.Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.Stime != null) list = list.Where(a => a.CreateTime >= param.Stime);
            if (param.Etime != null) list = list.Where(a => a.CreateTime <= param.Etime);
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderByDescending(a => a.CreateTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = list.ToList() }, setting);
            return Content(obj);
        }
        public ActionResult Deal(string id)
        {
            var obj = dc.Set<T_Order>().Where(a => a.ID == id).FirstOrDefault();
            if (obj.Status == 2) return Content("<script>window.parent.layer.closeAll();window.parent.layer.msg('该预约已处理！');</script>");
            ViewBag.id = obj.ID;
            return View();
        }
        [HttpPost]
        public JsonResult Deal(string id, FormCollection form)
        {
            try
            {
                var obj = dc.Set<T_Order>().Where(a => a.ID == id).FirstOrDefault();
                obj.AuthMark = form["authMark"];
                obj.AuthUser = user.Account;
                obj.Status = 2;
                dc.SaveChanges();
                return Json(Result.Success(msg: "处理成功！"));
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
    }
}