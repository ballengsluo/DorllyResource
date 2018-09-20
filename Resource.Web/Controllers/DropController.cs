using Newtonsoft.Json;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class DropController : ResourceBusinessController
    {
        #region 节点下拉
        public ActionResult CityDrop(string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_City>().ToList();
            ViewData["dataList"] = new SelectList(list, "ID", "Name", id);
            //if (!string.IsNullOrEmpty(id)) ViewData["dataList"] = new SelectList(list, "ID", "Name", id);
            //else ViewData["dataList"] = new SelectList(list, "ID", "Name");
            return PartialView();
        }
        public ActionResult CityDropList()
        {
            var list = dc.Set<T_City>().ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult RegionDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_Region>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.CityID == pid);
            else list = list.Where(a => a.ID == "1");
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult RegionDropList(string pid)
        {
            var list = dc.Set<T_Region>().Where(a => a.CityID == pid && a.Enable == true).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult ParkDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var plist = user.T_UserData.Select(a => a.DataID).ToList();
            var list = dc.Set<T_Park>().Where(a => plist.Contains(a.ID));
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.RegionID == pid);
            var result = list.Select(a => new { a.ID, a.Name }).ToList();
            ViewData["dataList"] = new SelectList(result, "ID", "Name", id);
            return PartialView();
        }
        public ActionResult ParkDropList(string pid)
        {
            var list = dc.Set<T_Park>().Where(a => a.RegionID == pid && a.Enable == true).Select(a => new { a.ID, a.Name }).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult StageDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_Stage>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.ParkID == pid);
            else list = list.Where(a => a.ID == "1");
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult StageDropList(string pid)
        {
            var list = dc.Set<T_Stage>().Where(a => a.ParkID == pid && a.Enable == true).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult BuildingDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_Building>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.StageID == pid);
            else list = list.Where(a => a.ID == "1");
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult BuildingDropList(string pid)
        {
            var list = dc.Set<T_Building>().Where(a => a.StageID == pid && a.Enable == true).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult FloorDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_Floor>().Where(a => a.Enable == true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.BuildingID == pid);
            else list = list.Where(a => a.ID == "1");
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult FloorDropList(string pid)
        {
            var list = dc.Set<T_Floor>().Where(a => a.BuildingID == pid && a.Enable == true).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult RMDrop(string pid, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_Resource>().Where(a => a.ResourceKindID == 1 && a.Enable == true);
            if (!string.IsNullOrEmpty(pid)) list = list.Where(a => a.Loc4 == pid);
            ViewData["dataList"] = new SelectList(list.Select(a => new { a.ID, a.Name }).ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult RMDropList(string pid)
        {
            var list = dc.Set<T_Resource>().Where(a => a.ResourceKindID == 1 && a.Loc4 == pid && a.Enable == true).Select(a => new { a.ID, a.Name }).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        #endregion
        public ActionResult GroupDrop(int kind, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_ResourceGroup>().Where(a => a.ResourceKindID == kind && a.Enable == true).Select(a => new { a.ID, a.Name });
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult GroupDropList(int kind)
        {
            var list = dc.Set<T_ResourceGroup>().Where(a => a.Enable == true && a.ResourceKindID == kind).Select(a => new { a.ID, a.Name }).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult ResourceKindDrop(int? id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_ResourceKind>();
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult ResourceKindDropList()
        {
            var list = dc.Set<T_ResourceKind>().Select(a => new { a.ID, a.Name }).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult ResourceTypeDrop(int kind, string id, int model)
        {
            ViewBag.model = model;
            var list = dc.Set<T_ResourceType>().Where(a => a.ResourceKindID == kind);
            ViewData["dataList"] = new SelectList(list.ToList(), "ID", "Name", id);
            return PartialView();
        }
        public ActionResult ResourceTypeDropList(int kind)
        {
            var list = dc.Set<T_ResourceType>().Where(a => a.ResourceKindID == kind).Select(a => new { a.ID, a.Name }).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }
    }
}