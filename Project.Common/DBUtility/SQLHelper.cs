using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.DBUtility
{
    public class SQLHelper : DBHelper
    {
        public SQLHelper(string connectionStr)
            : base(connectionStr)
        { }
        string ConnectionStr = string.Empty;
        SqlConnection SqlConnectionObject;
        SqlCommand SqlCommandObject;
        SqlDataAdapter SqlDataAdapterObject;
        protected override DbCommand DbCommandObject
        {
            get
            {
                if (SqlCommandObject == null)
                {
                    SqlCommandObject = new SqlCommand();
                }
                return SqlCommandObject;
            }
        }

        protected override DbConnection DbConnectionObject
        {
            get
            {
                if (SqlConnectionObject == null)
                {
                    SqlConnectionObject = new SqlConnection(ConnectionStr);
                }
                return SqlConnectionObject;
            }
        }

        protected override DbDataAdapter DbDataAdapterObject
        {
            get
            {
                if (SqlDataAdapterObject == null)
                {
                    SqlDataAdapterObject = new SqlDataAdapter();
                }
                return SqlDataAdapterObject;
            }
        }
    }
}
