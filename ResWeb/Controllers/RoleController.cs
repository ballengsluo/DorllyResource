using Resource.BLL.Container;
using Resource.IBLL;
using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResWeb.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService rs = Container.Resolve<IRoleService>();
        // GET: Role
        public ActionResult Index()
        {
            List<T_Role> rList = rs.GetModels(r => true).ToList();
            return View(rList);
        }
        public ActionResult Details()
        {
            List<T_Role> rList = rs.GetModels(r => true).ToList();
            return PartialView("_RoleTable", rList);
        }
        public JsonResult Edit(int id)
        {
            var role = rs.GetModels(r => r.ID == id).Select(r=>new { r.RoleName,r.RoleDesc});
            return Json(role,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ContentResult Create(string roleName, string roleDesc)
        {
            T_Role role = new T_Role();
            role.RoleDesc = roleDesc;
            role.RoleName = roleName;
            if (rs.Add(role)) return Content("1:添加成功！");
            else return Content("5:添加失败！");
        }
        [HttpPost]
        public ContentResult Edit(int id, string roleName, string roleDesc)
        {
            T_Role role = rs.GetModels(r => r.ID == id).FirstOrDefault();
            role.RoleDesc = roleDesc;
            role.RoleName = roleName;
            if (rs.Update(role)) return Content("1:更新成功！");
            else return Content("5:更新失败！");
        }
        [HttpPost]
        public ContentResult Delete(int id)
        {
            T_Role role = rs.GetModels(r => r.ID == id).FirstOrDefault();
            if (rs.Delete(role)) return Content("1:删除成功！");
            else return Content("5:删除失败！");
        }

    }
}