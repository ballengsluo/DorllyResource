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
            var list = dc.Set<V_Resource>().Where(a => true);
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.ParkID == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderBy(a => a.ResourceKindID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            JsonSerializerSettings setting = new JsonSerializerSettings{
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = list.ToList() }, setting);
            return Content(obj);
        }
        public ActionResult Reserve(string id)
        {
            return View();
        }

    }
}