
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resource.Web.Filters;

namespace Resource.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SysAuthorizeAttribute());
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new JsonNetResultAttritube());
        }
    }
}