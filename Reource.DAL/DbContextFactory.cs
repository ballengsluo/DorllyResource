using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resource.Model;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Resource.DAL
{
    public class DbContextFactory
    {
        /// <summary>
        /// 单例模式，创建线程唯一EF上下文对象
        /// </summary>
        /// <returns>only thread DbContext</returns>
        public static  DbContext Create() {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
           
            if (dbContext == null)
            {
                dbContext = new DorllyResEntities();
                DorllyResEntities dc = new DorllyResEntities();
                //dbContext.Configuration.ProxyCreationEnabled = false;
                CallContext.SetData("DbContext",dbContext);
            }
            return dbContext;
        }

    }
}
