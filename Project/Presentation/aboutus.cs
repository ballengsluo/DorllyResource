using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Json;

namespace Project.Presentation
{
    public partial class aboutus : Abstract, System.Web.SessionState.IRequiresSessionState
    {
        Data data = new Data();
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];

            if (action == "index")
            {
                cityName = context.Request["cityName"];
                indexactoion(context);
            }
            else if (action == "getContent")
                getContentactoion(context);
            else
            {
                JsonObjectCollection collection = new JsonObjectCollection();
                collection.Add(new JsonNumericValue("retCode", 100));
                context.Response.Write(collection.ToString());
            }
        }

        private void indexactoion(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int code = 0;
            string info = "";

            try
            {
                checkSession(cityName);
                collection.Add(new JsonStringValue("cityName", HttpContext.Current.Session["cityName"].ToString()));

                string sql = "select CSPhone from T_PageFoot where Position=2";
                string CSPhone = data.PopulateDataSet(sql).Tables[0].Rows[0]["CSPhone"].ToString();

                collection.Add(new JsonStringValue("cSPhone", CSPhone));
            }
            catch
            {
                code = 1;
                info = "获取记录出错1！";
            }

            collection.Add(new JsonNumericValue("retCode", code));
            collection.Add(new JsonStringValue("retInfo", info));
            context.Response.Write(collection.ToString());
        }

        private void getContentactoion(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int code = 0;
            string info = "";

            try
            {
                string sql = "select Content from T_PageFoot where Position=2";
                string Content = data.PopulateDataSet(sql).Tables[0].Rows[0]["Content"].ToString();

                collection.Add(new JsonStringValue("content", Content));
            }
            catch
            {
                code = 1;
                info = "获取记录出错1！";
            }
            collection.Add(new JsonNumericValue("retCode", code));
            collection.Add(new JsonStringValue("retInfo", info));
            context.Response.Write(collection.ToString());
        }
    }
}
