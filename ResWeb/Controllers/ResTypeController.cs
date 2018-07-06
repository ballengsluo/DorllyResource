using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.IBLL;
using Resource.BLL.Container;

namespace ResWeb.Controllers
{
    public class ResTypeController : Controller
    {
        private IRTypeService _typeService = Container.Resolve<IRTypeService>();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDropData()
        {
            var uList = _typeService.GetModels(a => true).ToList();
            ViewData["dataList"] = new SelectList(uList, "ID", "Name");
            return PartialView("_RTypeDrop");
        }

    }
}