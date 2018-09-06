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
            this.Flag = 1;
            this.Msg = "保存成功！";
        }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string ExMsg { get; set; }
    }
}