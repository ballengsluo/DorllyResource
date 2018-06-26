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
        IResourceService _resourceService = Container.Resolve<IResourceService>();
        IResRoomService _roomService = Container.Resolve<IResRoomService>();
        // GET: PublicRoom
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {          
            return View();
        }
        
        // GET: PublicRoom/Details/5
        public ActionResult Details(string cityCode, string regionCode, string parkCode, string stageCode, string buildingCode, string floorCode)
        {
            var list = _roomService.GetModels(a => true);
            if (!string.IsNullOrEmpty(floorCode))
                list = _roomService.GetModels(a => a.FCODE == floorCode);
            else if (!string.IsNullOrEmpty(buildingCode))
                list = _roomService.GetModels(a => a.BCODE == buildingCode);
            else if (!string.IsNullOrEmpty(stageCode))
                list = _roomService.GetModels(a => a.SCODE == stageCode);
            else if (!string.IsNullOrEmpty(parkCode))
                list = _roomService.GetModels(a => a.PCODE == parkCode);
            else if (!string.IsNullOrEmpty(regionCode))
                list = _roomService.GetModels(a => a.GCODE == regionCode);
            else if (!string.IsNullOrEmpty(cityCode))
                list = _roomService.GetModels(a => a.CCODE == cityCode);
            ViewBag.roomList = list.ToList();
            return PartialView("_RoomTable");
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
