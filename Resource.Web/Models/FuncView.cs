using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resource.Model;
namespace Resource.Web.Models
{
    public class FuncView
    {
        public List<T_RoleFunc> GetFunc(T_User user, string menuPath)
        {
            List<T_RoleFunc> rmfList = new List<T_RoleFunc>();
            foreach (var role in user.T_UserRole)
            {
                foreach (var func in role.T_Role.T_RoleFunc)
                {

                    foreach (var rmf in rmfList)
                    {
                        if (func.FuncID == rmf.FuncID) continue;
                    }
                    rmfList.Add(func);
                }
            }
            rmfList = rmfList.Where(a => a.T_MenuFunc.T_Menu.Path.ToLower() == menuPath.ToLower()).ToList();
            return rmfList.Where(a => a.T_MenuFunc.T_Menu.Path.ToLower() == menuPath.ToLower()).ToList();
        }

    }
}