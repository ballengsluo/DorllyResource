using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Model.DB
{
    public class SQLHelper : DBHelper
    {
        public SQLHelper(string conStr)
            : base(conStr)
        { }
        string ConStr = string.Empty;
        SqlConnection SqlConObj;
        SqlCommand SqlCmdObj;
        SqlDataAdapter SqlDaObj;
        protected override DbCommand DbCommandObject
        {
            get
            {
                if (SqlCmdObj == null)
                {
                    SqlCmdObj = new SqlCommand();
                }
                return SqlCmdObj;
            }
        }

        protected override DbConnection DbConnectionObject
        {
            get
            {
                if (SqlConObj == null)
                {
                    SqlConObj = new SqlConnection(ConStr);
                }
                return SqlConObj;
            }
        }

        protected override DbDataAdapter DbDataAdapterObject
        {
            get
            {
                if (SqlDaObj == null)
                {
                    SqlDaObj = new SqlDataAdapter();
                }
                return SqlDaObj;
            }
        }
    }
}
