using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.Model;
using Resource.Web.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Resource.Web.Controllers
{
    public class ImageController : Controller
    {

        #region 资源图片处理

        public ActionResult ResourceImgList(string resourceID)
        {
            DbContext dc = DbContextFactory.Create();
            var list = dc.Set<T_ResourceImg>().Where(a => a.ResourceID == resourceID).ToList();
            return PartialView("_ImgBox", list);
        }

        [HttpPost]
        public JsonResult SaveResourceImg(string resourceID)
        {
            try
            {
                if (Request.Files.Count <= 0 || string.IsNullOrEmpty(resourceID))
                    return Json(ResponseResult.GetResult(ResultEnum.Errorr));
                T_ResourceImg img = new T_ResourceImg();
                DbContext dc = DbContextFactory.Create();
                img.ID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                img.ResourceID = resourceID;
                img.IsCover = false;
                HttpPostedFileBase hpImg = Request.Files[0];
                string[] filetypes = hpImg.ContentType.Split('/');
                if (filetypes[1] != "jpg" && filetypes[1] != "gif" && filetypes[1] != "png" && filetypes[1] != "bmg" && filetypes[1] != "jpeg")
                    return Json(ResponseResult.GetResult(ResultEnum.Errorr));
                Regex regex = new Regex("[\\/:*?\"<>|]");
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + filetypes[1];
                string path = "~/Content/Resource/" + regex.Replace(img.ResourceID, "");
                if (Directory.Exists(Server.MapPath(path)) == false)
                    Directory.CreateDirectory(Server.MapPath(path));
                path = path + "/" + filename;
                hpImg.SaveAs(Server.MapPath(path));
                img.ImgUrl = path.Replace("~", "..");
                dc.Set<T_ResourceImg>().Add(img);
                if (dc.SaveChanges() > 0)
                    return Json(new { result = 1, img });
                else
                    return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception)
            {
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        [HttpPost]
        public JsonResult SetResourceCovert(string imgID, string resourceID)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_ResourceImg img = dc.Set<T_ResourceImg>().Where(a => a.ID == imgID).FirstOrDefault();
                dc.Database.ExecuteSqlCommand("update T_ResourceImg set iscover=0 where ResourceID=@rid", new SqlParameter("@rid", resourceID));
                img.IsCover = true;
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception)
            {
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        [HttpPost]
        public JsonResult DelResourceImg(string imgID)
        {
            try
            {
                DbContext dc = DbContextFactory.Create();
                T_ResourceImg img = dc.Set<T_ResourceImg>().Where(a => a.ID == imgID).FirstOrDefault();
                string filepath = Server.MapPath(img.ImgUrl.Replace("..", "~"));
                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
                dc.Set<T_ResourceImg>().Remove(img);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception)
            {
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }


        #endregion



    }
}
