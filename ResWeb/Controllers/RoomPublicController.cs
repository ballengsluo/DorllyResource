using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class RoomPublicController : Controller
    {
        // GET: RoomPublic
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoomPublic/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomPublic/Create
        public ActionResult Create()
        {
            ViewBag.test = "aaa";
            return View();
        }

        // POST: RoomPublic/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        // POST: RoomPublic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
