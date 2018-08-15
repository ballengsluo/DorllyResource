using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Json;

namespace Project.Presentation
{
    public partial class index : Abstract, System.Web.SessionState.IRequiresSessionState
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
            else if (action == "getSlides")
                getSlidesactoion(context);
            else if (action == "getMenu")
                getMenuactoion(context);
            else if (action == "getBanner")
                getBanneractoion(context);
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

                string sql = "select CSPhone,QRCode1,QRCode2 from T_PageFoot where Position=1";
                string CSPhone = data.PopulateDataSet(sql).Tables[0].Rows[0]["CSPhone"].ToString();
                string QRCode1 = data.PopulateDataSet(sql).Tables[0].Rows[0]["QRCode1"].ToString();
                string QRCode2 = data.PopulateDataSet(sql).Tables[0].Rows[0]["QRCode2"].ToString();

                collection.Add(new JsonStringValue("cSPhone", CSPhone));
                collection.Add(new JsonStringValue("qRCode1", QRCode1));
                collection.Add(new JsonStringValue("qRCode2", QRCode2));
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

        private void getSlidesactoion(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            StringBuilder sb = new StringBuilder("");
            int code = 0;
            string info = "";

            try
            {
                string cityID = HttpContext.Current.Session["cityID"].ToString();
                Business.Business_T_HomePage bc = new Business.Business_T_HomePage();

                sb.Append("<ul class='slides'>");
                foreach (Entity.Entity_T_HomePage it in bc.GetT_HomePageListQuery(cityID, "2", "4"))
                {
                    sb.Append("<li class='holder' style='background-image: url(" + it.ImgUrl + ");'>");
                    sb.Append("<div class='overlay-gradient'></div>");
                    sb.Append("<div class='container'>");
                    sb.Append("<div class='col-md-10 col-md-offset-1 text-center  slider-text'>");
                    sb.Append("<div class='slider-text-inner desc'>");
                    sb.Append("<h1>" + it.Title + "</h1>");
                    sb.Append("<p>" + it.SubTitle + "</p>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            catch
            {
                code = 1;
                info = "获取记录出错1！";
            }

            collection.Add(new JsonStringValue("listHtml", sb.ToString()));
            collection.Add(new JsonNumericValue("retCode", code));
            collection.Add(new JsonStringValue("retInfo", info));
            context.Response.Write(collection.ToString());
        }

        private void getMenuactoion(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            StringBuilder sb = new StringBuilder("");
            int code = 0;
            string info = "";

            try
            {
                string cityID = HttpContext.Current.Session["cityID"].ToString();
                Business.Business_T_HomePage bc = new Business.Business_T_HomePage();

                foreach (Entity.Entity_T_HomePage it in bc.GetT_HomePageListQuery(cityID, "3", "4"))
                {
                    sb.Append("<div class='text-center' style='flex:1'>");
                    sb.Append("<div class='services'>");
                    sb.Append("<img src='"+it.ImgUrl+"'>");
                    sb.Append("<div class='desc'>");
                    sb.Append("<h3><a href='"+it.LinkUrl+"'>'"+it.Title+"'</a></h3>");
                    sb.Append("<p>"+it.SubTitle+"</p>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
            }
            catch
            {
                code = 1;
                info = "获取记录出错1！";
            }

            collection.Add(new JsonStringValue("listHtml", sb.ToString()));
            collection.Add(new JsonNumericValue("retCode", code));
            collection.Add(new JsonStringValue("retInfo", info));
            context.Response.Write(collection.ToString());
        }

        private void getBanneractoion(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            StringBuilder sb = new StringBuilder("");
            int code = 0;
            string info = "";

            try
            {
                string cityID = HttpContext.Current.Session["cityID"].ToString();
                Business.Business_T_HomePage bc = new Business.Business_T_HomePage();

                foreach (Entity.Entity_T_HomePage it in bc.GetT_HomePageListQuery(cityID, "1", "4"))
                {
                    sb.Append("<li class='container'>");
                    sb.Append("<a href='"+it.LinkUrl+"'>");
                    sb.Append("<img src='" + it.ImgUrl + "'>");
                    sb.Append("<div class='animate-box'>");
                    sb.Append("<div class='text-center fh5co-text'>");
                    sb.Append("<h3>"+it.Title+"</h3>");
                    sb.Append("<p>");
                    sb.Append(it.SubTitle);
                    sb.Append("</p>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</a>");
                    sb.Append("</li>");
                }
            }
            catch
            {
                code = 1;
                info = "获取记录出错1！";
            }

            collection.Add(new JsonStringValue("listHtml", sb.ToString()));
            collection.Add(new JsonNumericValue("retCode", code));
            collection.Add(new JsonStringValue("retInfo", info));
            context.Response.Write(collection.ToString());
        }
    }
}
