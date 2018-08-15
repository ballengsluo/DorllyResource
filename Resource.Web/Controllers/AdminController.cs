﻿using Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Model.DB;
using System.Data;
using Newtonsoft.Json;

namespace Resource.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            List<T_RoleMenu> rmList = new List<T_RoleMenu>();
            foreach (var role in user.T_UserRole)
            {
                foreach (var rm in role.T_Role.T_RoleMenu)
                {
                    foreach (var item in rmList)
                    {
                        if (rm.MenuID == item.MenuID) continue;
                    }
                    rmList.Add(rm);
                }
            }
            return View(rmList);
        }

        public ActionResult Main()
        {
            SQLHelper sq = SQLFactory.Create();
            DataSet ds = sq.GetDataSet("Pro_MainResourceCount",CommandType.StoredProcedure);
            ViewBag.AreaCount = ds.Tables[0].Compute("SUM(Area)","true");
            ViewBag.NumberCount = ds.Tables[0].Compute("SUM(Number)", "true");
            ViewBag.rm = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[0]));
            ViewBag.cb = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[1]));
            ViewBag.mr = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[2]));
            ViewBag.ad = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ds.Tables[3]));
            return View();
        }
    }
}