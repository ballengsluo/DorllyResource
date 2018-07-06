using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.DBUtility
{
    public class Test
    {
        static string strConn = ConfigurationManager.ConnectionStrings["constr"].ToString();
        SQLHelper mysow = new SQLHelper(strConn);
        public void test()
        {
           DataSet ds= mysow.GetDataSet("Select * from t_city",System.Data.CommandType.Text);
        }
    }
}
