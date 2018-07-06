using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.IBLL;
using Resource.Model;
using Resource.BLL.Container;
using Newtonsoft.Json;

namespace ResWeb.Controllers
{

    public class GroupController : Controller
    {
        private IRGroupService _groupService = Container.Resolve<IRGroupService>();
        private IRTypeService ts = Container.Resolve<IRTypeService>();
        // GET: RGourp
        public ActionResult Index()
        {
            var gList = _groupService.GetModels(g => true).ToList();
            var tList = ts.GetModels(t => true).ToList();
            List<SelectListItem> sList = new List<SelectListItem>();
            sList.Add(new SelectListItem { Text = "有效", Value = "0" });
            sList.Add(new SelectListItem { Text = "停用", Value = "1" });
            ViewData["typeList"] = new SelectList(tList, "ID", "Name");
            ViewData["statusList"] = sList;
            return View(gList);
        }
        public ActionResult Details()
        {
            var gList = _groupService.GetModels(g => true).ToList();
            return PartialView("_GroupTable", gList.ToList());
        }

        public ActionResult Select(int? typeId, string parkId, bool? status)
        {
            var gList = _groupService.GetModels(g => true);
            if (typeId != null)
                gList = gList.Where(g => g.ResourceTypeID == typeId);
            if (!string.IsNullOrEmpty(parkId))
                gList = gList.Where(g => g.ParkCode == parkId);
            if (status != null)
                gList = gList.Where(g => g.Status == status);
            return PartialView("_GroupTable", gList.ToList());
        }

        public ContentResult CloseOrOpen(int id, int type)
        {
            var group = _groupService.GetModels(g => g.ID == id).FirstOrDefault();
            if (type == 1)
            {
                group.Status = false;
                if (_groupService.Update(group)) return Content("1:停用成功！");
                else return Content("5:停用失败！");
            }
            else
            {
                group.Status = true;
                if (_groupService.Update(group)) return Content("1:启用成功！");
                else return Content("5:启用失败！");
            }
        }

        public ContentResult Delete(int id)
        {
            var group = _groupService.GetModels(g => g.ID == id).FirstOrDefault();
            if (_groupService.Delete(group)) return Content("1:删除成功！");
            else return Content("5:删除失败！");
        }

        [HttpPost]
        public ContentResult Create(FormCollection form)
        {
            T_RGroup group = new T_RGroup();
            group.GroupCode = form["groupCode"];
            group.GroupName = form["groupName"];
            group.ResourceTypeID = Convert.ToInt32(form["type"]);
            group.ParkCode = form["park"];
            group.Status = true;
            group.UpdateTime = DateTime.Now;
            if (_groupService.Add(group)) return Content("1:添加成！");
            else return Content("5:添加失败！");
        }

        public JsonResult Edit(int id)
        {
            var group = _groupService.GetModels(g => g.ID == id).Select(g => new { g.GroupCode, g.GroupName, g.ResourceTypeID, g.ParkCode });
            return Json(group, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ContentResult Edit(FormCollection form)
        //{
        //    int id = Convert.ToInt32(form["groupid"]);
        //    var group = _groupService.GetModels(g => g.ID == id).FirstOrDefault();
        //    group.GroupCode = form["groupCode"];
        //    group.GroupName = form["groupName"];
        //    group.RTypeID = Convert.ToInt32(form["f_type"]);
        //    group.ParkCode = form["f_park"];
        //    if (_groupService.Update(group)) return Content("1:更新成功！");
        //    else return Content("5:更新失败！");
        //}

        public ActionResult GetDropData(string RGroupCode)
        {
            if (string.IsNullOrEmpty(RGroupCode)) ViewData["dataList"] = new SelectList(_groupService.GetModels(a => true).ToList(), "GroupCode", "GroupName");
            else ViewData["dataList"] = new SelectList(_groupService.GetModels(a => a.GroupCode== RGroupCode).ToList(), "GroupCode", "GroupName");
            return PartialView("_GroupDrop");
        }
        


    }
}