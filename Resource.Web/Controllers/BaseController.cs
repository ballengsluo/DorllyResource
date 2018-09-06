using Resource.Model;
using Resource.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Controllers
{
    public class BaseController<T> : Controller where T : class,new()
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
        public List<string> ParkList
        {
            get
            {
                return user.Park.Split(',').ToList();
            }
        }
        public Expression<Func<T, bool>> ModelLambda { get; set; }

        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        // GET: Base/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Base/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Base/Edit/5

        public ActionResult AddOrUpdate(Expression<Func<T, bool>> whereLambda, FormCollection fc, params string[] ignoreField)
        {
            try
            {
                bool add = false;
                T t = dc.Set<T>().Where(whereLambda).FirstOrDefault();
                if (t == null)
                {
                    t = new T();
                    add = true;
                }
                if (TryUpdateModel(t, "", fc.AllKeys, ignoreField))
                {
                    if (add) dc.Set<T>().Add(t);
                    if (dc.SaveChanges() > 0) Json(new Result { Flag = 1, Msg = "保存成功！" });
                }
                return Json(new Result { Flag = 2, Msg = "保存失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Result { Flag = 3, Msg = "保存异常！", ExMsg = ex.StackTrace });
            }
        }

        // POST: Base/Delete/5
        [HttpPost]
        public ActionResult Del(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
