using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Json;
using System.Data;

namespace Project.Presentation
{
    public partial class detail : Abstract, System.Web.SessionState.IRequiresSessionState
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
            else if (action == "load")
            {
                loadaction(context);
            }
            else if (action == "rentSubmit")
            {
                rentSubmitaction(context);
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

                string sql = "select CSPhone from T_PageFoot where Position=1";
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

        private void loadaction(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int code = 0;
            string info = "";

            try
            {
                string resourceID = context.Request["resourceID"];
                Business.Business_T_Resource bc = new Business.Business_T_Resource();
                int r = bc.load(resourceID);
                if (r > 0)
                {
                    collection.Add(new JsonNumericValue("ResourceStatus", r));

                    collection.Add(new JsonStringValue("Location", bc.Entity.Location));
                    collection.Add(new JsonStringValue("Container", bc.Entity.Content));

                    #region 租用面积,个数,大小
                    string RangeRent = "";
                    string StartRent = "";
                    if (bc.Entity.ResourceKindID == "1")//办公室
                    {
                        RangeRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.RangeArea + "</div>" +
                            "<div class='div-text'>可租面积</div>";

                        StartRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.StartArea + "㎡</div>" +
                           "<div class='div-text'>起租面积</div>";
                    }
                    else if (bc.Entity.ResourceKindID == "2")//工位
                    {
                        RangeRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.RangeNum + "</div>" +
                                                  "<div class='div-text'>可租个数</div>";

                        StartRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.StartNum + "个</div>" +
                           "<div class='div-text'>起租个数</div>";

                        bc.Entity.Name = "工位";
                    }
                    else if (bc.Entity.ResourceKindID == "3")//会议室
                    {
                        RangeRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.Area + "㎡</div>" +
                                                  "<div class='div-text'>可租面积</div>";

                        StartRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.Area + "㎡</div>" +
                           "<div class='div-text'>起租面积</div>";
                    }
                    else if (bc.Entity.ResourceKindID == "4")//广告
                    {
                        RangeRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.RangeSize + "</div>" +
                           "<div class='div-text'>可租大小</div>";

                        StartRent = "<div class='p-t p-b' style='font-size:18px;'>" + bc.Entity.StartSize + "㎡</div>" +
                           "<div class='div-text'>起租大小</div>";
                    }
                    collection.Add(new JsonStringValue("RangeRent", RangeRent));
                    collection.Add(new JsonStringValue("StartRent", StartRent));
                    #endregion

                    #region 轮播图
                    StringBuilder sb = new StringBuilder("");
                    Business.Business_T_ResourceImg tImg = new Business.Business_T_ResourceImg();
                    sb.Append("<ul class='slides'>");
                    foreach (Entity.Entity_T_ResourceImg it in tImg.GetT_ResourceImgListQuery(resourceID, "1"))
                    {
                        sb.Append("<li><img src='" + it.ImgUrl + "'</li>");
                    }
                    foreach (Entity.Entity_T_ResourceImg it in tImg.GetT_ResourceImgListQuery(resourceID, "0"))
                    {
                        sb.Append("<li><img src='" + it.ImgUrl + "'</li>");
                    }
                    sb.Append("</ul>");
                    collection.Add(new JsonStringValue("Slides", sb.ToString()));
                    #endregion

                    #region 价格
                    Business.Business_T_ResourcePrice bPrice = new Business.Business_T_ResourcePrice();
                    string Price = "";
                    int row = bPrice.loadResourceID(resourceID);
                    if (row > 0)
                    {
                        //年度
                        if (bPrice.Entity.YearEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.YearOutPrice.ToString("#0.00") + "/年<br />";
                        }
                        //季度
                        if (bPrice.Entity.QuaterEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.QuaterOutPrice.ToString("#0.00") + "/季度<br />";
                        }
                        //月度
                        if (bPrice.Entity.MonthEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.MonthOutPrice.ToString("#0.00") + "/月<br />";
                        }
                        //星期
                        if (bPrice.Entity.WeekEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.WeekOutPrice.ToString("#0.00") + "/周<br />";
                        }
                        //天
                        if (bPrice.Entity.DayEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.DayOutPrice.ToString("#0.00") + "/天<br />";
                        }
                        //半天
                        if (bPrice.Entity.HDayEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.HDayOutPrice.ToString("#0.00") + "/半天<br />";
                        }
                        //小时
                        if (bPrice.Entity.HourEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.HourOutPrice.ToString("#0.00") + "/小时<br />";
                        }
                        //单个
                        if (bPrice.Entity.SingleEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.SingleOutPrice.ToString("#0.00") + "/个<br />";
                        }
                        //延迟价
                        if (bPrice.Entity.DelayEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.DelayOutPrice.ToString("#0.00") + "/延迟<br />";
                        }
                        //平方
                        if (bPrice.Entity.MeterEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.MeterMinPrice.ToString("#0.00") + "/㎡~" + bPrice.Entity.MeterMaxPrice.ToString("#0.00") + "/㎡<br />";
                        }
                        //月区间价
                        if (bPrice.Entity.IMonthEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.IMonthMinPrice.ToString("#0.00") + "/月~" + bPrice.Entity.IMonthMaxPrice.ToString("#0.00") + "/月<br />";
                        }
                        //单个区间价
                        if (bPrice.Entity.ISingleEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.ISingleMinPrice.ToString("#0.00") + "/个~" + bPrice.Entity.ISingleMaxPrice.ToString("#0.00") + "/个<br />";
                        }
                        //单次区间价
                        if (bPrice.Entity.OnceEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.OnceMinPrice.ToString("#0.00") + "/次~" + bPrice.Entity.OnceMaxPrice.ToString("#0.00") + "/次<br />";
                        }
                        //其他区间价
                        if (bPrice.Entity.OtherEnable == true)
                        {
                            Price += "￥" + bPrice.Entity.OtherMinPrice.ToString("#0.00") + "~" + bPrice.Entity.OtherMaxPrice.ToString("#0.00") + "<br />";
                        }
                    }
                    collection.Add(new JsonStringValue("Price", Price));
                    #endregion

                    collection.Add(new JsonStringValue("ResourceName", bc.Entity.Name));
                }
                else
                {
                    collection.Add(new JsonNumericValue("ResourceStatus", r));
                }
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

        private void rentSubmitaction(HttpContext context)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int code = 0;
            string info = "";

            try
            {
                string ResourceID = context.Request["resourceID"];
                string CustName = context.Request["rentName"];
                string CustPhone = context.Request["rentTel"];

                string ResourceSql = "select * from T_Resource where ID='" + ResourceID + "'";
                DataTable ResourceDt = data.PopulateDataSet(ResourceSql).Tables[0];
                DataRow[] ResourceRows = ResourceDt.Select();
                if (ResourceRows.Length > 0)
                {
                    string parkID = ResourceDt.Rows[0]["ParkID"].ToString();

                    string CitySql = "select ID from T_City where ID=(select top 1 b.CityID from T_Park a left join T_Region b on a.RegionID=b.ID where a.ID='" + parkID + "')";
                    string cityID = data.PopulateDataSet(CitySql).Tables[0].Rows[0]["ID"].ToString();

                    string insertSQL = "insert into T_Order(ID,ResourceID,City,CustName,CustPhone,CreateTime,Status) " +
                        "values(newID(),'" + ResourceID + "','" + cityID + "','" + CustName + "','" + CustPhone + "',getDate(),'1')";
                    int r = data.ExecuteNonQuery(insertSQL);
                    if (r <= 0)
                    {
                        code = 1;
                    }
                }
                else
                {
                    code = 1;
                }
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
