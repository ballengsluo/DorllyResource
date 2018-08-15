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
    public class ResourceController : BaseController
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
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
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
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                else return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }
        [HttpPost]
        public ActionResult Del(string id)
        {
            try
            {

                var rs = dc.Set<T_Resource>().Where(a => a.ID == id).FirstOrDefault();
                List<T_ResourcePrice> priceList = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == id).ToList();
                foreach (var item in priceList)
                {
                    dc.Set<T_ResourcePrice>().Remove(item);
                }
                DelImg(rs.ID);
                dc.Set<T_Resource>().Remove(rs);
                if (dc.SaveChanges() > 0) return Json(ResponseResult.GetResult(ResultEnum.Success));
                return Json(ResponseResult.GetResult(ResultEnum.Fail));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return Json(ResponseResult.GetResult(ResultEnum.Exception));
            }
        }

        #region oooo
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(string id, FormCollection form)
        //{
        //    //if (string.IsNullOrEmpty(form["ID"])) return Json(ResponseResult.GetResult(ResultEnum.Fail));
        //    //string id = form["ID"];
        //    return SaveResourceInfo(id, form);
        //    #region old
        //    //List<string> imgStr = new List<string>();
        //    //try
        //    //{
        //    //    bool success = false;
        //    //    if (string.IsNullOrEmpty(form["ID"])) return Json(ResponseResult.GetResult(ResultEnum.Fail));
        //    //    T_Resource rs = new T_Resource();
        //    //    if (TryUpdateModel(rs, "", form.AllKeys))
        //    //    {
        //    //        imgStr = SaveImg(rs.ID);
        //    //        rs.CreateTime = DateTime.Now;
        //    //        rs.CreateUser = user.Account;
        //    //        rs.UpdateTime = DateTime.Now;
        //    //        rs.UpdateUser = user.Account;
        //    //        rs.Enable = true;
        //    //        rs.Status = 2;
        //    //        dc.Set<T_Resource>().Add(rs);
        //    //        var price = dc.Set<T_ResourcePrice>().Where(a => a.ResourceID == rs.ID).FirstOrDefault();
        //    //        if (price != null) dc.Set<T_ResourcePrice>().Remove(price);
        //    //        price = new T_ResourcePrice();
        //    //        price.ResourceID = rs.ID;
        //    //        if (TryUpdateModel(price, "", form.AllKeys, new string[] { "ID" }))
        //    //        {
        //    //            dc.Set<T_ResourcePrice>().Add(price);
        //    //            if (dc.SaveChanges() > 0) success = true;
        //    //        }

        //    //    }
        //    //    if (success) return Json(ResponseResult.GetResult(ResultEnum.Success));
        //    //    DelImg(imgStr);
        //    //    return Json(ResponseResult.GetResult(ResultEnum.Fail));

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    DelImg(imgStr);
        //    //    System.Diagnostics.Debug.Print(ex.ToString());
        //    //    return Json(ResponseResult.GetResult(ResultEnum.Exception));
        //    //} 
        //    #endregion
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Edit(string id, FormCollection form)
        //{
        //    return SaveResourceInfo(id, form);
        //} 
        #endregion

    }
}