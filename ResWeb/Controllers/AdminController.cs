using Resource.IBLL;
using Resource.BLL.Container;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ResWeb.Controllers
{
    public class AdminController : Controller
    {
        private IMenuService ms = Container.Resolve<IMenuService>();

        // GET: Admin
        public ActionResult Index()
        {
            List<T_Menu> mList = ms.GetModels(p=>true).ToList();
            return View(mList);
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Test() {
            return View();
        }

    }
}