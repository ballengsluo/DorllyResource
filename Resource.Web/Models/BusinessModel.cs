using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resource.Web.Models
{
    public class BusinessModel<T> where T:class
    {
        public int MyProperty { get; set; }
        //public static Result Add(DbContext dc,T t,FormCollection fc,params string[] ignoreField)
        //{
        //    Result result = new Result();
        //    if (TryUpdateModel(t, "", fc.AllKeys, ignoreField))
        //    {
        //        if (dc.SaveChanges() > 0)
        //        {

        //        }
        //    }

        //    return result;
        //}
    }
}