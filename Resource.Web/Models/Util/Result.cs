using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class Result
    {
        public Result()
        {
        }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string ExMsg { get; set; }

        public static Result Success(string msg = "操作成功！", string exmsg = "")
        {
            return new Result { Flag = 1, Msg = msg, ExMsg = exmsg };
        }
        public static Result Fail(string msg = "操作失败！", string exmsg = "")
        {
            return new Result { Flag = 2, Msg = msg, ExMsg = exmsg };
        }
        public static Result Exception(string msg = "数据异常！", string exmsg = "")
        {
            return new Result { Flag = 3, Msg = msg, ExMsg = exmsg };
        }
    }
}