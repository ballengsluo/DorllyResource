using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class PageFootController : Controller
    {
        // GET: HomeFoot
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeFoot/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeFoot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeFoot/Create
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

        // GET: HomeFoot/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeFoot/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeFoot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeFoot/Delete/5
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
