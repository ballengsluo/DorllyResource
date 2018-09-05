using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class Result
    {
        public Result() { }
        public Result(int model)
        {
            if (model == 1)
            {
                Flag = 1;
                Msg = "保存成功！";
            }
        }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string ExInfo { get; set; }
    }
}