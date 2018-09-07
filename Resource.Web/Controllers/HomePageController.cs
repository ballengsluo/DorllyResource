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
    public class HomePageController : BaseController
    {
        public ActionResult Index()
        {
            string menuName = "/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            List<T_RoleFunc> rmfList = new FuncView().GetFunc(user, menuName);
            return View(rmfList);
        }
        public JsonResult Search(SearchParam param)
        {

            var list = from a in dc.Set<T_HomePage>()
                       join b in dc.Set<T_City>() on a.CityID equals b.ID into t1
                       from c in t1.DefaultIfEmpty()
                       select new
                       {
                           a.CityID,
                           a.OrderNum,
                           a.ID,
                           a.Position,
                           a.ImgUrl,
                           a.LinkUrl,
                           a.Title,
                           a.SubTitle,
                           a.Status,
                           CityName = c.Name
                       };
            if (!string.IsNullOrEmpty(param.City)) list = list.Where(a => a.CityID == param.City);
            if (param.IType > 0) list = list.Where(a => a.Position == param.IType);
            if (param.Status > 0) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderBy(a => a.OrderNum).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            return Json(new { count, data = list.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            return View(hp);
        }
        #region 增删改
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(T_HomePage hp)
        {
            string newPath = string.Empty;
            try
            {

                newPath = SaveImg();
                hp.ImgUrl = newPath;
                hp.Status = 1;
                T_User user = RouteData.Values["user"] as T_User;
                hp.CreateTime = DateTime.Now;
                hp.CreateUser = user.Account;
                hp.UpdateTime = DateTime.Now;
                hp.UpdateUser = user.Account;
                dc.Set<T_HomePage>().Add(hp);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                else return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(newPath)) DelImg(newPath);
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Edit(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            if (hp.Status == 4 || hp.Status == 6)
                return Content("<script>window.parent.layer.closeAll();window.parent.layer.msg('该数据不允许编辑!');</script>");
            return View(hp);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            string oldPath = string.Empty;
            string newPath = string.Empty;
            try
            {

                T_User user = RouteData.Values["user"] as T_User;
                T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
                if (Request.Files.Count > 0 && !string.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    oldPath = hp.ImgUrl;
                    newPath = SaveImg();
                }
                if (!string.IsNullOrEmpty(newPath)) hp.ImgUrl = newPath;
                hp.UpdateTime = DateTime.Now;
                hp.UpdateUser = user.Account;
                if (TryUpdateModel(hp, "", form.AllKeys, new string[] { "ImgUrl", "Status" }))
                {
                    if (dc.SaveChanges() > 0)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(oldPath)) DelImg(oldPath);
                        }
                        catch (Exception)
                        {
                        }
                        return Json(Result.Success());
                    }
                }
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(newPath)) DelImg(newPath);
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        #endregion
        #region 状态业务
        [HttpPost]
        public JsonResult Del(int id)
        {
            try
            {

                T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
                if (hp.Status == 4) return Json(Result.Fail(msg: "请选择正确的操作！"));
                string path = hp.ImgUrl;
                dc.Set<T_HomePage>().Remove(hp);
                if (dc.SaveChanges() > 0)
                {
                    if (!string.IsNullOrEmpty(path)) DelImg(path);
                    return Json(Result.Success());
                }
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public JsonResult Pass(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            hp.Status = 2;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            else return Json(Result.Fail());
        }
        public JsonResult Notpass(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            hp.Status = 3;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            else return Json(Result.Fail());
        }
        public JsonResult Pub(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            if (hp.Status == 4) return Json(Result.Fail(msg: "状态一致，无从改变！"));
            else if (hp.Status != 1 && hp.Status != 5) return Json(Result.Fail(msg: "请选择正确的操作！"));
            hp.Status = 4;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            else return Json(Result.Fail());
        }
        public JsonResult Unpub(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            if (hp.Status == 5) return Json(Result.Fail(msg: "状态一致，无从改变！"));
            else if (hp.Status != 4) return Json(Result.Fail(msg: "请选择正确的操作！"));
            hp.Status = 5;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            else return Json(Result.Fail());
        }
        public JsonResult Off(int id)
        {

            T_HomePage hp = dc.Set<T_HomePage>().Where(a => a.ID == id).FirstOrDefault();
            if (hp.Status == 6) return Json(Result.Fail(msg: "状态一致，无从改变！"));
            else if (hp.Status == 4) return Json(Result.Fail(msg: "请选择正确的操作！"));
            hp.Status = 6;
            if (dc.SaveChanges() > 0) return Json(Result.Success());
            else return Json(Result.Fail());
        }
        #endregion
        #region 图片处理
        public string SaveImg()
        {
            string path = string.Empty;
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase hpImg = Request.Files[0];
                    string[] filetypes = hpImg.ContentType.Split('/');
                    if (filetypes[1] == "jpg" || filetypes[1] == "gif" || filetypes[1] == "png" || filetypes[1] == "bmg" || filetypes[1] == "jpeg")
                    {
                        string filePath = "~/Content/Home/";
                        if (Directory.Exists(Server.MapPath(filePath)) == false)
                            Directory.CreateDirectory(Server.MapPath(filePath));
                        filePath = filePath + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + filetypes[1];
                        hpImg.SaveAs(Server.MapPath(filePath));
                        path = filePath.Replace("~", "..");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }
            return path;
        }
        public void DelImg(string path)
        {
            path = path.Replace("..", "~");
            if (System.IO.File.Exists(Server.MapPath(path))) System.IO.File.Delete(Server.MapPath(path));
        }
        #endregion
    }
}