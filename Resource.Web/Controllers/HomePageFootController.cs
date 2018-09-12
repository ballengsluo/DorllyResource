using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class HomePageFootController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Edit(int id)
        {
            
            T_PageFoot pf = dc.Set<T_PageFoot>().Where(a => a.Position == id).FirstOrDefault();
            if (pf == null) pf = new T_PageFoot();
            var obj = JsonConvert.SerializeObject(pf);
            return Content(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection form)
        {
            string code1 = string.Empty;
            string code2 = string.Empty;
            try
            {
                
                T_User user = RouteData.Values["user"] as T_User;
                var pf = dc.Set<T_PageFoot>().Where(a => a.Position == id).FirstOrDefault();
                pf.UpdateTime = DateTime.Now;
                pf.UpdateUser = user.Account;
                try
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase pfcode1 = Request.Files["QRCode1"];
                        if (!string.IsNullOrEmpty(pfcode1.FileName)) pf.QRCode1 = SaveImg(pfcode1);
                        HttpPostedFileBase pfcode2 = Request.Files["QRCode2"];
                        if (!string.IsNullOrEmpty(pfcode2.FileName)) pf.QRCode2 = SaveImg(pfcode2);
                    }
                }
                catch (Exception)
                {
                }
                if (TryUpdateModel(pf, "", form.AllKeys))
                {
                    dc.Set<T_PageFoot>().AddOrUpdate(pf);
                    if (dc.SaveChanges() > 0) return Json(Result.Success());
                }
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult PositionDrop(int? id)
        {
            List<SelectListItem> type = new List<SelectListItem>
            {
                new SelectListItem{Text="网页底部",Value="1"},
                new SelectListItem{Text="关于我们",Value="2"},
                new SelectListItem{Text="关于多丽",Value="3"}
            };
            if (id != null) ViewData["dataList"] = new SelectList(type, "Value", "Text");
            else ViewData["dataList"] = new SelectList(type, "Value", "Text", id);
            return PartialView();
        }
        public string SaveImg(HttpPostedFileBase hpImg)
        {
            string path = string.Empty;
            try
            {
                string[] filetypes = hpImg.ContentType.Split('/');
                if (filetypes[1] == "jpg" || filetypes[1] == "gif" || filetypes[1] == "png" || filetypes[1] == "bmg" || filetypes[1] == "jpeg")
                {
                    string filePath = "~/Content/QRCode/";
                    if (Directory.Exists(Server.MapPath(filePath)) == false)
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    filePath = filePath + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + filetypes[1];
                    hpImg.SaveAs(Server.MapPath(filePath));
                    path = filePath.Replace("~", "..");
                }
            }
            catch (Exception ex)
            {

            }
            return path;
        }
        public void DelImg(string path)
        {
            path = path.Replace("..", "~");
            if (System.IO.File.Exists(Server.MapPath(path))) System.IO.File.Delete(Server.MapPath(path));
        }
    }
}
