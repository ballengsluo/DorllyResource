using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class CubicleController : Controller
    {
        // GET: Cubicle
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cubicle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cubicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cubicle/Create
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

        // GET: Cubicle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cubicle/Edit/5
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

        // GET: Cubicle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cubicle/Delete/5
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
