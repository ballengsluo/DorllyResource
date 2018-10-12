using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
namespace Resource.Web.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.func = Func.GetFunc(user.Account, MenuPath);
            return View();
        }
        public ActionResult Search(SearchParam param)
        {
            var list = dc.Set<V_Order>().Where(a => ParkList.Contains(a.Loc1));
            if (!string.IsNullOrEmpty(param.Park)) list = list.Where(a => a.Loc1 == param.Park);
            if (!string.IsNullOrEmpty(param.ID)) list = list.Where(a => a.ID.Contains(param.ID));
            if (!string.IsNullOrEmpty(param.Name)) list = list.Where(a => a.Name.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Group)) list = list.Where(a => a.GroupID == param.Group);
            if (param.Kind != null) list = list.Where(a => a.ResourceKindID == param.Kind);
            if (param.Stime != null) list = list.Where(a => a.CreateTime >= param.Stime);
            if (param.Etime != null) list = list.Where(a => a.CreateTime <= param.Etime);
            if (param.Status != null) list = list.Where(a => a.Status == param.Status);
            int count = list.Count();
            list = list.OrderByDescending(a => a.CreateTime).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize);
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
            var obj = JsonConvert.SerializeObject(new { count = count, data = list.ToList() }, setting);
            return Content(obj);
        }
        public ActionResult Deal(string id)
        {
            var obj = dc.Set<T_Order>().Where(a => a.ID == id).FirstOrDefault();
            if (obj.Status == 2) return Content("<script>window.parent.layer.closeAll();window.parent.layer.msg('该预约已处理！');</script>");
            ViewBag.id = obj.ID;
            return View();
        }
        [HttpPost]
        public JsonResult Deal(string id, FormCollection form)
        {
            try
            {
                var obj = dc.Set<T_Order>().Where(a => a.ID == id).FirstOrDefault();
                obj.AuthMark = form["authMark"];
                obj.AuthUser = user.Account;
                obj.Status = 2;
                dc.SaveChanges();
                return Json(Result.Success(msg: "处理成功！"));
            }
            catch (Exception ex)
            {
                return Json(Result.Exception(exmsg: ex.StackTrace));
            }
        }
        public ActionResult Check(string id)
        {
            ViewBag.oid = id;
            return View();
        }

        public string GetOrderInfo(string id)
        {
            try
            {
                var order = dc.Set<V_Order>().Where(a => a.ID == id).Select(a => new
                {
                    a.ID,
                    a.CreateTime,
                    a.CustName,
                    a.CustPhone,
                    a.Status,
                    a.AuthUser,
                    a.AuthMark,
                    a.ResourceID,
                    a.Name,
                    a.ResourceTypeName,
                    a.ResourceKindName,
                    a.Loc1Name,
                    a.LocText,
                    a.Location
                }).FirstOrDefault();
                //IsoDateTimeConverter setting = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                JsonSerializerSettings setting = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.SerializeObject(new { Flag = 1, obj = order }, Formatting.Indented, setting);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Flag = 3, obj = ex });
            }
        }

        public FileResult GetFile(string id,string tt)
        {
            try
            {
                throw new Exception("dddd");
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet("baobiao");
                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue(id);
                row.CreateCell(0).SetCellValue(tt);
                MemoryStream ms = new MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", "xinxin.xls");
                
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}