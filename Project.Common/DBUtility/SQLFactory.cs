using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.DBUtility
{
    public class SQLFactory
    {
        public static SQLHelper Create()
        {
            string conn = ConfigurationManager.ConnectionStrings["DorllyResourceConnection"].ToString();
            return new SQLHelper(conn);
        }
    }
}
