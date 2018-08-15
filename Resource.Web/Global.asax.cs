using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Resource.Web.App_Start;

namespace Resource.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ViewEngines.Engines.Clear();//清除所有引擎
            ViewEngines.Engines.Add(new RazorViewEngine()); //添加Razor
        }
    }
}
