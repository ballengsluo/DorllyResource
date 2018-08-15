using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Resource.Web.Controllers
{
    public class S_ResourceController : BaseController
    {
        // GET: S_Resource
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            return View();
        }
    }
}