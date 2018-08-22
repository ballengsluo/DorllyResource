using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Resource.Web.Controllers
{
    public class BaseController : Controller
    {
        public DbContext dc
        {
            get
            {
                return DbContextFactory.Create();
            }
        }
        public T_User user
        {
            get
            {

                T_User user = RouteData.Values["user"] as T_User;
                ViewBag.user = user;
                return user;

            }
        }
        public JsonResult Service(DbContext dc)
        {
            if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
            else return Json(ResponseResult.GetResult(ResultEnum.Fail));
        }

        #region 资源业务

        public List<string> SaveImg(string resourceID)
        {
            List<string> imgList = new List<string>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase hpfb = Request.Files[i];
                    string[] filetypes = hpfb.ContentType.Split('/');
                    if (filetypes[1] != "jpg"
                        && filetypes[1] != "gif"
                        && filetypes[1] != "png"
                        && filetypes[1] != "bmg"
                        && filetypes[1] != "jpeg") continue;
                    T_ResourceImg img = new T_ResourceImg();
                    img.ID = Guid.NewGuid().ToString();
                    img.ResourceID = resourceID;
                    img.IsCover = false;
                    string filename = Guid.NewGuid().ToString() + "." + filetypes[1];
                    string path = "~/Content/Resource/" + new Regex("[\\/:*?\"<>|]").Replace(img.ResourceID, "");
                    string oldpath = path + "/old";
                    if (Directory.Exists(Server.MapPath(path)) == false) Directory.CreateDirectory(Server.MapPath(path));
                    if (Directory.Exists(Server.MapPath(oldpath)) == false) Directory.CreateDirectory(Server.MapPath(oldpath));
                    string filepath = path + "/" + filename;
                    string oldfilepath = oldpath + "/" + filename;
                    hpfb.SaveAs(Server.MapPath(oldfilepath));
                    MakeThumNail(Server.MapPath(oldfilepath), Server.MapPath(filepath), 800, 580, "HW");
                    //删除原图
                    System.IO.File.Delete(Server.MapPath(oldfilepath));
                    imgList.Add(filepath);
                    img.ImgUrl = filepath.Replace("~", "..");
                    dc.Set<T_ResourceImg>().Add(img);
                }
            }
            return imgList;
        }

        /// <summary>
        /// 缩放图像
        /// </summary>
        /// <param name="originalImagePath">图片原始路径</param>
        /// <param name="thumNailPath">保存路径</param>
        /// <param name="width">缩放图的宽</param>
        /// <param name="height">缩放图的高</param>
        /// <param name="model">缩放模式</param>
        public static void MakeThumNail(string originalImagePath, string thumNailPath, int width, int height, string model)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int thumWidth = width;      //缩略图的宽度
            int thumHeight = height;    //缩略图的高度

            int x = 0;
            int y = 0;

            int originalWidth = originalImage.Width;    //原始图片的宽度
            int originalHeight = originalImage.Height;  //原始图片的高度

            switch (model)
            {
                case "HW":      //指定高宽缩放,可能变形
                    break;
                case "W":       //指定宽度,高度按照比例缩放
                    thumHeight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":       //指定高度,宽度按照等比例缩放
                    thumWidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut":
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                    {
                        originalHeight = originalImage.Height;
                        originalWidth = originalImage.Height * thumWidth / thumHeight;
                        y = 0;
                        x = (originalImage.Width - originalWidth) / 2;
                    }
                    else
                    {
                        originalWidth = originalImage.Width;
                        originalHeight = originalWidth * height / thumWidth;
                        x = 0;
                        y = (originalImage.Height - originalHeight) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);

            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量查值法
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量，低速度呈现平滑程度
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            graphic.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

            try
            {
                bitmap.Save(thumNailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                graphic.Dispose();
            }
        }

        public void DelImg(string resourceID)
        {
            var obj = dc.Set<T_ResourceImg>();
            var imgList = obj.Where(a => a.ResourceID == resourceID).ToList();
            foreach (var item in imgList)
            {
                string path = Server.MapPath(item.ImgUrl.Replace("..", "~"));
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                obj.Remove(item);
            }
        }

        public void DelImg(T_ResourceImg img)
        {
            string path = Server.MapPath(img.ImgUrl.Replace("..", "~"));
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        }

        public void DelImg(List<string> imgList)
        {
            foreach (var item in imgList)
            {
                string path = Server.MapPath(item);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
        }
        [HttpPost]
        public JsonResult DelImgAction(string id)
        {
            var img = dc.Set<T_ResourceImg>().Where(a => a.ID == id).FirstOrDefault();
            dc.Set<T_ResourceImg>().Remove(img);
            if (dc.SaveChanges() > 0)
            {
                DelImg(img);
                return Json(ResponseResult.GetResult(ResultEnum.Success));
            }
            else return Json(ResponseResult.GetResult(ResultEnum.Fail));

        }
        public bool SavePrice(string resourceID, FormCollection form)
        {
            bool success = false;
            bool add = false;
            var obj = dc.Set<T_ResourcePrice>();
            var price = obj.Where(a => a.ResourceID == resourceID).FirstOrDefault();
            if (price == null)
            {
                price = new T_ResourcePrice();
                add = true;
            }
            if (TryUpdateModel(price, "", form.AllKeys, new string[] { "ID" }))
            {
                if (add)
                {
                    price.ResourceID = resourceID;
                    obj.Add(price);
                }
                success = true;
            }
            return success;
        }

        public bool SaveResource(string id, FormCollection form)
        {
            bool success = false;
            bool add = false;
            var obj = dc.Set<T_Resource>();
            var resource = obj.Where(a => a.ID == id).FirstOrDefault();
            if (resource == null)
            {
                resource = new T_Resource();
                resource.CreateTime = DateTime.Now;
                resource.CreateUser = user.Account;
                resource.Enable = true;
                resource.Status = 2;
                add = true;
            }
            if (TryUpdateModel(resource, "", form.AllKeys, new string[] { "Enable", "Status" }))
            {
                resource.UpdateTime = DateTime.Now;
                resource.UpdateUser = user.Account;
                if(resource.ResourceKindID==1) resource.ParentID = form["FloorID"];
                if (add) obj.Add(resource);
                success = true;
            }
            return success;
        }

        public JsonResult SaveResourceInfo(string id, FormCollection form)
        {
            List<string> imgList = new List<string>();
            try
            {
                imgList = SaveImg(id);
                if (SaveResource(id, form) && SavePrice(id, form) && dc.SaveChanges() > 0)
                    return Json(ResponseResult.GetResult(ResultEnum.Success));
                DelImg(imgList);
                return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                DelImg(imgList);
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        #endregion

        public bool SavePublic(string id, string resourcID, FormCollection form)
        {
            bool success = false;
            bool add = false;
            var obj = dc.Set<T_ResourcePublic>();
            var pub = obj.Where(a => a.ID == id).FirstOrDefault();
            if (pub == null)
            {

                pub = new T_ResourcePublic();
                pub.ID = id;
                pub.CreateTime = DateTime.Now;
                pub.CreateUser = user.Account;
                pub.ResourceID = resourcID;
                pub.Status = 1;
                add = true;
            }
            if (TryUpdateModel(pub, "", form.AllKeys, new string[] { "ID", "Status" }))
            {
                pub.UpdateTime = DateTime.Now;
                pub.UpdateUser = user.Account;
                pub.BeginTime = Convert.ToDateTime(form["PubTime"].Split('~')[0].Trim());
                pub.EndTime = Convert.ToDateTime(form["PubTime"].Split('~')[1].Trim());
                if (add) obj.Add(pub);
                success = true;
            }
            return success;
        }

        public JsonResult SavePubInfo(string id, string resourceID, FormCollection form)
        {
            List<string> imgList = new List<string>();
            try
            {
                imgList = SaveImg(resourceID);
                if (SaveResource(resourceID, form)
                    && SavePrice(resourceID, form)
                    && SavePublic(id, resourceID, form)
                    && dc.SaveChanges() > 0)
                    return Json(ResponseResult.GetResult(ResultEnum.Success));
                DelImg(imgList);
                return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                DelImg(imgList);
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
    }
}