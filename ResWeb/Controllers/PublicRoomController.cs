using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.BLL;
using Resource.IBLL;
using Resource.Model;

namespace ResWeb.Controllers
{
    public class PublicRoomController : Controller
    {
        IRImageService _imgServcie = Container.Resolve<IRImageService>();
        // GET: PublicRoom
        public ActionResult Index()
        {
            return View();
        }

        // GET: PublicRoom/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublicRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublicRoom/Create
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

        // GET: PublicRoom/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PublicRoom/Edit/5
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

        // GET: PublicRoom/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublicRoom/Delete/5
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

        public ActionResult UploadImg()
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
