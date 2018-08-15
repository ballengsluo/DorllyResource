using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Json;
using System.Data;


namespace Project.Presentation
{
    public partial class list : Abstract, System.Web.SessionState.IRequiresSessionState
    {
        Data data = new Data();
        private string parkID = "";
        private string kindID = "null";
        private string searchName = "null";
        private string orderstr = "b.Level";
        private int page = 1;
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];

            if (action == "index")
            {
                cityName = context.Request["cityName"];
                kindID = context.Request["kindID"];
                indexactoion(context);
            }
            else if (action == "search")
            {
                parkID = context.Request["parkID"];
                kindID=context.Request["kindID"];
                searchName = context.Request["searchName"];
                orderstr = context.Request["orderstr"];
                page = ParseIntForString(context.Request["page"]);
                searchaction(context);
            }
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

                string cityID = HttpContext.Current.Session["cityID"].ToString();

                #region 园区类型
                StringBuilder parkli = new StringBuilder("");
                DataTable selectParkDt = data.PopulateDataSet("select ID from T_Park where RegionID in(select ID from T_Region where CityID='" + cityID + "') and Enable=1").Tables[0];
                foreach (DataRow it in selectParkDt.Rows)
                {
                    parkID+=it["ID"].ToString()+",";
                }

                DataTable ParkDt = data.PopulateDataSet("select ID,Name from T_Park where Enable=1").Tables[0];
                if (parkID == "")
                {
                    parkli.Append("<li class='active' id='parknull'>全部</li>");
                    foreach (DataRow it in ParkDt.Rows)
                    {
                        parkli.Append("<li id='" + it["ID"].ToString() + "'>" + it["Name"].ToString() + "</li>");
                    }
                    parkID = "null,";
                }
                else
                {
                    parkli.Append("<li id='parknull'>全部</li>");
                    foreach (DataRow it in ParkDt.Rows)
                    {
                        if (parkID.IndexOf(it["ID"].ToString()) >= 0)
                        {
                            parkli.Append("<li id='" + it["ID"].ToString() + "' class='active'>" + it["Name"].ToString() + "</li>");
                        }
                        else
                        {
                            parkli.Append("<li id='" + it["ID"].ToString() + "'>" + it["Name"].ToString() + "</li>");
                        }
                    }
                }
                collection.Add(new JsonStringValue("parkID", parkID));
                collection.Add(new JsonStringValue("parkli", parkli.ToString()));
                #endregion

                #region 资源类型
                StringBuilder kindli = new StringBuilder("");
                DataTable kindDt = data.PopulateDataSet("select ID,Name from T_ResourceKind order by ID").Tables[0];
                if (kindID == "null")
                {
                    kindli.Append("<li class='active' id='kindnull'>全部</li>");
                    foreach (DataRow it in kindDt.Rows)
                    {
                        kindli.Append("<li id='" + it["ID"].ToString() + "'>" + it["Name"].ToString() + "</li>");
                    }
                }
                else
                {
                    kindli.Append("<li id='kindnull'>全部</li>");
                    foreach (DataRow it in kindDt.Rows)
                    {
                        if (kindID == it["ID"].ToString())
                        {
                            kindli.Append("<li id='" + it["ID"].ToString() + "' class='active'>" + it["Name"].ToString() + "</li>");
                        }
                        else
                        {
                            kindli.Append("<li id='" + it["ID"].ToString() + "'>" + it["Name"].ToString() + "</li>");
                        }
                    }
                }
                collection.Add(new JsonStringValue("kindli", kindli.ToString()));
                #endregion
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

        private void searchaction(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            StringBuilder sb = new StringBuilder("");
            int code = 0;
            string info = "";
            try
            {
                Business.Business_T_ResourcePublic bc = new Business.Business_T_ResourcePublic();
                int r = 1;
                string releaseData = DateTime.Now.ToString("yyyy-MM-dd");
                if(parkID=="null")
                {
                    parkID = string.Empty;
                }
                if (kindID == "null")
                {
                    kindID = string.Empty;
                }
                if (searchName == "null")
                {
                    searchName = string.Empty;
                }

                if (orderstr == "Level")
                {
                    orderstr = "a.Level";
                }
                else if (orderstr == "OrderPrice")
                {
                    orderstr = "b.OrderPrice";
                }
                else if (orderstr == "RentArea")
                {
                    orderstr = "b.RentArea";
                }
                else if (orderstr == "BeginTime")
                {
                    orderstr = "a.BeginTime";
                }
                else
                {
                    orderstr = "a.Level";
                }

                foreach (Entity.Entity_T_ResourcePublic it in bc.GetT_ResourcePublicListQuery(parkID, kindID, searchName, releaseData, orderstr, page, pageSize))
                {
                    if (r % 2 == 1)
                    {
                        sb.Append("<div class='listbox'>");
                    }
                    sb.Append("<div class='listbox-item'>");
                    sb.Append("<div class='prod text-left animate-box'>");
                    sb.Append("<div class='product text-center' style='background-image: url(" + it.CoverImg + ");'>");
                    sb.Append("<a href='detail.html?resourceID=" + it.ResourceID + "' target='_blank' class='view'>");
                    sb.Append("<i class='icon-plus'></i>");
                    sb.Append("</a>");
                    sb.Append("</div>");
                    sb.Append("<a href='detail.html?resourceID=" + it.ResourceID + "' target='_blank'>");
                    sb.Append("<div class='item-row'>");
                    sb.Append("<h3>" + it.ResourceName + "</h3>");
                    sb.Append("<span class='price'>￥" + it.OrderPrice.ToString("#0.00") + "</span>");
                    sb.Append("</div>");
                    sb.Append("<div class='item-row'>");
                    if (it.ResourceKindID == "1")
                    {
                        sb.Append("<div>" + it.BuildingName + "-" + it.FloorName + "-" + it.ResourceName + "</div>");
                        sb.Append("<span>元/㎡</span>");
                    }
                    else if (it.ResourceKindID == "2")
                    {
                        sb.Append("<div>" + it.BuildingName + "-" + it.FloorName + "-" + it.ParentName + "-" + it.ResourceName + "</div>");
                        sb.Append("<span>元/个</span>");
                    }
                    else if (it.ResourceKindID == "3" || it.ResourceKindID == "4")
                    {
                        sb.Append("<div></div>");
                        sb.Append("<span>元/位</span>");
                    }
                    else
                    {
                        sb.Append("<div></div>");
                        sb.Append("<span></span>");
                    }
                    sb.Append("</div>");
                    sb.Append("<div class='div-map'><i class='icon-location-pin'></i><span>" + it.CityName + "</span></div>");
                    sb.Append("</a>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    if (r % 2 == 0)
                    {
                        sb.Append("</div>");
                    }
                    r++;
                }

                if (r > 1 && r % 2 == 1)
                {
                    sb.Append("</div>");
                }

                if (r > 1)
                {
                    collection.Add(new JsonNumericValue("footCode", 1));
                }
                else
                {
                    collection.Add(new JsonNumericValue("footCode", 0));
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
