using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Json;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Project.Presentation
{
    /// <summary>
    /// 所有页面的基类，派生于BasePage
    /// </summary>
    /// <author>tz</author>
    /// <date>2011-07-28</date>
    public class Abstract : IHttpHandler
    {
        public static string cityName = "";
        Data obj = new Data();
        public int pageSize = 20;

        public virtual void ProcessRequest(HttpContext context)
        {
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        protected DateTime GetDate()
        {
            return DateTime.Parse(obj.PopulateDataSet("select DT=getdate()").Tables[0].Rows[0]["DT"].ToString());
        }
        protected System.DateTime ParseDateForString(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return DateTime.MinValue.AddYears(1900);
            }

            return DateTime.Parse(val);
        }
        protected System.DateTime ParseSearchDateForString(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return default(DateTime);
            }

            return DateTime.Parse(val);
        }
        protected string ParseStringForDate(System.DateTime? date)
        {
            if (null == date)
                return "";
            if (DateTime.MinValue.AddYears(1900).Equals(date))
                return "";

            return ((System.DateTime)date).ToString("yyyy-MM-dd", null);
        }
        protected string ParseStringForDateTime(System.DateTime? date)
        {
            if (null == date)
                return "";
            if (DateTime.MinValue.AddYears(1900).Equals(date))
                return "";


            return ((System.DateTime)date).ToString("yyyy-MM-dd HH:mm", null);
        }
        protected decimal ParseDecimalForString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return decimal.Parse(val);
        }
        protected int ParseIntForString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return Int32.Parse(val);
        }
        protected string creatFileName(string expandedName)
        {
            Random rand = new Random();
            char[] code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                sb.Append(code[rand.Next(code.Length)]);
            }
            string fileName = sb.ToString() + "." + expandedName;
            return fileName;
        }
        protected string getRandom()
        {
            Random rand = new Random();
            char[] code = "1234567890".ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = 0; j < 6; j++)
            {
                sb.Append(code[rand.Next(code.Length)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">要截取的长度</param>
        /// <param name="flag">截取后是否加省略号(true加,false不加)</param>
        /// <returns></returns>
        public static string CutString(string str, int len, bool flag)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bts = ascii.GetBytes(str);
            int _len = 0;
            string _outString = "";
            for (int i = 0; i < bts.Length; i++)
            {
                if ((int)bts[i] == 63)//两个字符
                {
                    _len += 2;
                    if (_len > len)//截取的长度若是最后一个占两个字节，则不截取
                    {
                        break;
                    }
                }
                else
                {
                    _len += 1;
                }

                try
                {
                    _outString += str.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (_len >= len)
                {
                    break;
                }
            }
            if (str != _outString && flag == true)//判断是否添加省略号
            {
                _outString += "...";
            }
            return _outString;
        }

        /// <summary>
        /// 判断是否存在city缓存值，有则不必重新定位
        /// </summary>
        /// <param name="cityName"></param>
        public void checkSession(string cityName)
        {
            if (HttpContext.Current.Session["cityID"] == null)
            {
                GetCity(cityName);
            }
            else if (HttpContext.Current.Session["cityID"].ToString() == "")
            { GetCity(cityName); }
            else { }
        }

        /// <summary>
        /// 根据IP选择城市，或默认城市
        /// </summary>
        /// <param name="CityName">城市</param>
        public void GetCity(string CityName)
        {
            //if (HttpContext.Current.Session["CurrParkNo"] == null)
            //HttpContext.Current.Session["openid"].ToString() == ""
            string cityNameSession = "";

            string sql = "select top 1 * from T_City where CharIndex(Name,'" + CityName + "')>0 and Enable=1 order by ID";
            DataTable dt = obj.PopulateDataSet(sql).Tables[0];

            DataRow[] dtRows = dt.Select();
            if (dtRows.Length > 0)
            {
                cityNameSession = dt.Rows[0]["Name"].ToString();
                HttpContext.Current.Session["cityID"] = dt.Rows[0]["ID"].ToString();
                HttpContext.Current.Session["cityName"] = cityNameSession;
            }
            else
            {
                string distanceSql = "select * from T_City where Enable=1 and IsDefault=1";
                DataTable distanceDt = obj.PopulateDataSet(distanceSql).Tables[0];

                cityNameSession = distanceDt.Rows[0]["Name"].ToString();
                HttpContext.Current.Session["cityID"] = distanceDt.Rows[0]["ID"].ToString();
                HttpContext.Current.Session["cityName"] = cityNameSession;
            }
        }

        private static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }

        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}

