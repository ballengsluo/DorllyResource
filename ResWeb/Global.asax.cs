using ResWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ResWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //var asmFile = args[0];
            //Console.WriteLine("Making anonymous types public for '{0}'.", asmFile);
            //var asmDef = AssemblyDefinition.ReadAssembly(asmFile, new ReaderParameters            {
            //    ReadSymbols = true
            //});
            //var anonymousTypes = asmDef.Modules
            //    .SelectMany(m => m.Types)
            //    .Where(t => t.Name.Contains("<>f__AnonymousType"));

            //foreach (var type in anonymousTypes)
            //{
            //    type.IsPublic = true;
            //}

            //asmDef.Write(asmFile, new WriterParameters
            //{
            //    WriteSymbols = true
            //});
        }
    }
}
