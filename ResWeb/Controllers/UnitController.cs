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
    public class UnitController : Controller
    {
        private IUnitService _unitService = Container.Resolve<IUnitService>();
        // GET: Unit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUnitDropDownList()
        {
            var uList = _unitService.GetModels(u=>true).ToList();
            ViewData["unitList"] = new SelectList(uList,"UnitCode","UnitName");
            return PartialView("_GetUnitDropDownList");
        }
    }
}