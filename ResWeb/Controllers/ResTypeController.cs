using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.IBLL;
using Resource.Model;
using Resource.BLL;

namespace ResWeb.Controllers
{
    public class ResTypeController : Controller
    {
        private IRTypeService _typeService = Container.Resolve<IRTypeService>();
        // GET: ResType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTypeDropDownList()
        {
            var uList = _typeService.GetModels(u => true).ToList();
            ViewData["resTypeList"] = new SelectList(uList, "ID", "Name");
            return PartialView("_GetTypeDropDownList");
        }
    }
}