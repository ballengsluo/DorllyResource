using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Resource.Model;
using Resource.Model.DB;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace Resource.Web.Models
{
    public class Func
    {
        public static List<T_MenuFunc> GetFunc(string account, string menuPath)
        {
            try
            {
                var spList = new List<SqlParameter> { 
                 new SqlParameter("Account", account), 
                 new SqlParameter("MenuPath", menuPath) 
                };
                DataSet ds = SQLFactory.Create().GetDataSet("Pro_GetFunc", CommandType.StoredProcedure, spList.ToArray());
                return JsonConvert.DeserializeObject<List<T_MenuFunc>>(JsonConvert.SerializeObject(ds.Tables[0]));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}