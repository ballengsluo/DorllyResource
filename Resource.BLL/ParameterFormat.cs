using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.BLL
{
    public class ParameterFormat
    {
        public static SqlDbType GetDbType(object obj)
        {
            string name = obj.GetType().Name;
            switch (name)
            {
                case "String":
                    return SqlDbType.NVarChar;
                case "Int32":
                    return SqlDbType.Int;
                case "DateTime":
                    return SqlDbType.DateTime;
                case "Boolean":
                    return SqlDbType.Bit;
                case "Decimal":
                    return SqlDbType.Decimal;
            }
            return SqlDbType.NVarChar;
        }

    }
}
