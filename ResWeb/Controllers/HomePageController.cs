using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
namespace ResWeb.Controllers
{
    public class HomePageController : Controller
    {
        IPagePositionService _positionService = Container.Resolve<IPagePositionService>();
        IHomePageService _imageService = Container.Resolve<IHomePageService>();
        ICityService _cityServcie = Container.Resolve<ICityService>();
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Most()
        {
            return View();
        }

        public ActionResult Foot()
        {
            return View();
        }
        //public ActionResult Check(string cityCode, int? positionId, int? status)
        //{
        //    var list1 = _imageService.GetModels(a => true);
        //    if (!string.IsNullOrEmpty(cityCode)) list1=list1.Where(a => a.CityCode == cityCode);
        //    if (positionId != null) list1 = list1.Where(a => a.PPID == positionId);
        //    if (status != null)
        //    {
        //        bool s = Convert.ToBoolean(status);
        //        list1 = list1.Where(a => a.Status == s);
        //    }

        //    var list2 = from a in list1
        //                join b in _positionService.GetModels(a => true) on a.PPID equals b.ID into temp1
        //                from c in temp1.DefaultIfEmpty()
        //                join d in _cityServcie.GetModels(a => true) on a.CityCode equals d.CityCode into temp2
        //                from e in temp2.DefaultIfEmpty()
        //                select new {a.ID,a.PPID,a.ImgURL,a.OrderNum,a.Status,a.UpdateTime,a.Title,a.SubTitle,a.UpdateUser, PositionName=c.PPName??"",CityName=e.Name??"" };

        //    return PartialView("_MostTable",JsonConvert.DeserializeObject(JsonConvert.SerializeObject(list2.ToList())));
        //}
        public JsonResult GetHPType(int type)
        {
            return Json(_positionService.GetModels(a => a.PPType == type).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}