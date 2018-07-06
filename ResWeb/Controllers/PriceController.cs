using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
namespace ResWeb.Controllers
{
    public class PriceController : Controller
    {
        IRPriceService _priceResource = Container.Resolve<IRPriceService>();
        // GET: Price
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPriceByResCode(string resourceCode)
        {
            var price = _priceResource.GetModels(a => a.ResourceCode == resourceCode).FirstOrDefault();
            return Json(price, JsonRequestBehavior.AllowGet);
        }
    }
}