using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Resource.BLL;
using Resource.IBLL;
using Resource.Model;

namespace ResWeb.Controllers
{
    public class ImageController : Controller
    {
        IRImageService _imgServcie = Container.Resolve<IRImageService>();
        public ActionResult GetResImg()
        {
            List<T_RImage> imgList = _imgServcie.GetModels(i => true).ToList();
            return PartialView("_ImgBox", imgList);
        }
        public ActionResult GetResImgByID(string resourceID)
        {
            List<T_RImage> imgList = _imgServcie.GetModels(i => i.ResourceID==resourceID).ToList();
            return PartialView("_ImgBox", imgList);
        }
        [HttpPost]
        public JsonResult SaveResImg(FormCollection collection)
        {
            try
            {
                //判断上传文件的a数目
                if (Request.Files.Count > 0)
                {
                    T_RImage img = new T_RImage();
                    string product = collection["product"];
                    img.ResourceID = product;
                    //获取文件
                    HttpPostedFileBase uploadImg = Request.Files[0];//获取上传的图片
                    ////判断上传文件大小，小于5M
                    //if (proImage.ContentLength > 20 * 1024 * 1024)
                    //{
                    //    return Content("Error1");
                    //}
                    //截取图片类型：image/png
                    string[] filetypes = uploadImg.ContentType.Split('/');
                    //判断文件的类型
                    if (filetypes[1] == "jpg" || filetypes[1] == "gif" || filetypes[1] == "png" || filetypes[1] == "bmg" || filetypes[1] == "jpeg")
                    {
                        Regex regex = new Regex("[\\/:*?\"<>|]");
                        string filename = regex.Replace(product, "") + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + filetypes[1]; //给上传文件重命名
                        string path = "~/Content/image/public/" + regex.Replace(product, "");
                        if (Directory.Exists(Server.MapPath(path)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(path));
                        }
                        path = path + "/" + filename;
                        uploadImg.SaveAs(Server.MapPath(path));
                        img.ImgURL = path.Replace("~", "..");
                        img.IsCover = false;
                        if (_imgServcie.Add(img))
                        {
                            string imgID = _imgServcie.GetModels(i => true).Select(i => i.ID).Max().ToString();
                            string tempID = Guid.NewGuid().ToString();
                            return Json(new { result = "1", msg = "图片保存成功！", path = img.ImgURL, id = imgID, tempid = tempID });
                        }
                        else
                            return Json(new { result = "5", msg = "删除失败，请检查数据！" });
                    }
                    else
                    {
                        return Json(new { result = "0", msg = "图片格式不符合要求！" });
                    }
                }
                else
                {
                    return Json(new { result = "0", msg = "无数据！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "2", msg = "重大错误：" + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteResImg(int id)
        {
            try
            {
                T_RImage img = _imgServcie.GetModels(i => i.ID == id).FirstOrDefault();
                if (_imgServcie.Delete(img))
                {
                    System.IO.File.Delete(Server.MapPath(img.ImgURL.Replace("..", "~")));
                    return Json(new { result = "1", msg = "删除成功！" });
                }
                    
                else
                    return Json(new { result = "5", msg = "删除失败，请检查数据！" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "2", msg = "重大错误：" + ex.Message });
            }

        }
        public JsonResult UpdateResImg(int id)
        {
            try
            {
                T_RImage img = _imgServcie.GetModels(i => i.ID == id).FirstOrDefault();

                List<T_RImage> iList = _imgServcie.GetModels(i => i.ResourceID == img.ResourceID).ToList();
                foreach (var item in iList)
                {
                    item.IsCover = false;
                    _imgServcie.Update(item);
                }
                img.IsCover = true;
                if (_imgServcie.Update(img))
                    return Json(new { result = "1", msg = "设置封面成功！" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "5", msg = "设置封面失败，请检查数据！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "2", msg = "重大错误：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
