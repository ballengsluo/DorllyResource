using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace Resource.Web.Controllers
{
    public class ResourceController : ResourceBusinessController
    {
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrUpdate(string id, FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["ID"])) id = form["ID"];
            return SaveResourceInfo(id, form);
        }

        [HttpPost]
        public ActionResult Open(string id)
        {
            try
            {
                var rs = dc.Set<T_Resource>().Where(a => a.ID == id).FirstOrDefault();
                rs.Enable = true;
                dc.Set<T_Resource>().AddOrUpdate(rs);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public ActionResult Close(string id)
        {
            try
            {
                var rs = dc.Set<T_Resource>().Where(a => a.ID == id).FirstOrDefault();
                rs.Enable = false;
                dc.Set<T_Resource>().AddOrUpdate(rs);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        [HttpPost]
        public ActionResult Del(string id)
        {
            try
            {
                var rs = dc.Set<T_Resource>().Where(a => a.ID == id).FirstOrDefault();
                //删除价格
                List<T_ResourcePrice> priceList = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == id).ToList();
                foreach (var item in priceList)
                {
                    dc.Set<T_ResourcePrice>().Remove(item);
                }
                //删除图片
                DelImg(rs.ID);
                dc.Set<T_Resource>().Remove(rs);
                if (dc.SaveChanges() > 0) return Json(Result.Success());
                return Json(Result.Fail());
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
    }
}