using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class ResponseResult
    {
        public static object GetResult(ResultEnum enums, Exception ex = null)
        {
            switch (enums)
            {
                case ResultEnum.Success:
                    return new { result = 1, msg = "操作成功！" };
                case ResultEnum.Fail:
                    return new { result = 5, msg = "操作失败！" };
                case ResultEnum.Exception:
                    return new { result = 2, msg = "数据异常！", ex = ex.Message+ex.InnerException.Message };
                case ResultEnum.Errorr:
                    return new { result = 2, msg = "参数错误！" };
                case ResultEnum.Nullable:
                    return new { result = 1, msg = "数据无改变！" };
            }
            return new { result = 3, msg = "未知异常-_-！" };
        }
    }
}