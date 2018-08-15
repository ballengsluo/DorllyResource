using Resource.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class ExtraInfo
    {
        public static string CreateInfo(T_User user, DbContext dc)
        {
            T_ExtraInfo ei = new T_ExtraInfo();
            string infoID = Guid.NewGuid().ToString();
            ei.ID = infoID;
            ei.CreateTime = DateTime.Now;
            ei.CreateUser = user.Account;
            ei.UpdateTime = DateTime.Now;
            ei.UpdateUser = user.Account;
            dc.Set<T_ExtraInfo>().Add(ei);
            return infoID;
        }
        public static void UpdateInfo(string infoID, T_User user, DbContext dc)
        {

            T_ExtraInfo ei = dc.Set<T_ExtraInfo>().Where(a => a.ID == infoID).FirstOrDefault();
            ei.UpdateTime = DateTime.Now;
            ei.UpdateUser = user.Account;
            dc.Set<T_ExtraInfo>().AddOrUpdate(ei);
        }
    }
}