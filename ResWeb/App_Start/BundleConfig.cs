using System.Web.Optimization;
//using WebHelpers.Mvc5;

namespace ResWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*************************************    CSS    ***********************************/
            bundles.Add(new StyleBundle("~/common/css").Include(
                "~/Plugins/font-awesome/css/font-awesome.min.css",
                "~/Plugins/bootstrap/css/bootstrap.min.css",
                 "~/Plugins/layui/css/layui.css"
            ));
            bundles.Add(new StyleBundle("~/main/css").Include(
                "~/Plugins/adminlte/css/adminlte.css",
                "~/Plugins/adminlte/css/skins/_all-skins.css"
            ));
            bundles.Add(new StyleBundle("~/ce/css").Include(               
               "~/Plugins/bootstrap-select/css/bootstrap-select.min.css",
               "~/Plugins/app/css/app.form.css"
           ));
            bundles.Add(new StyleBundle("~/index/css").Include(
                "~/Plugins/bootstrap-select/css/bootstrap-select.min.css",
                 "~/Plugins/app/css/app.func.css",
                 "~/Plugins/app/css/app.select.css",
                 "~/Plugins/app/css/app.table.css"
             ));
            /*************************************    JS    ***********************************/
            bundles.Add(new ScriptBundle("~/common/js").Include(
                "~/Plugins/jquery/jquery-2.2.4.js",
                "~/Plugins/bootstrap/js/bootstrap.min.js",
                "~/Plugins/layui/layui.js",
                 "~/Plugins/app/js/app.common.js"                
            ));
            bundles.Add(new ScriptBundle("~/main/js").Include(
               "~/Plugins/adminlte/js/adminlte.js",
               "~/Plugins/app/js/app.main.js"
             ));
            bundles.Add(new ScriptBundle("~/ce/js").Include(
                 "~/Plugins/jquery/jquery.unobtrusive-ajax.min.js",
                 "~/Plugins/bootstrap-select/js/bootstrap-select.min.js",
                 "~/Plugins/bootstrap-validator/js/bootstrapValidator.min.js"
             ));
           
        }
    }
}