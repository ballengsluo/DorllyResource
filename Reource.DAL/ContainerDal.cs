using Autofac;
using Resource.Model;
using Resource.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.DAL
{
    public class ContainerDal
    {
        public static IContainer container = null;

        public static T Resolve<T>()
        {
            try
            {
                if (container == null)
                    Initialise();

            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错:" + ex.Message);
            }
            return container.Resolve<T>();
        }

        public static void Initialise()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<MenuDal>().As<IMenuDal>().InstancePerLifetimeScope();
            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerLifetimeScope();
            builder.RegisterType<RoleDal>().As<IRoleDal>().InstancePerLifetimeScope();
            builder.RegisterType<RGroupDal>().As<IRGroupDal>().InstancePerLifetimeScope();
            builder.RegisterType<RTypeDal>().As<IRTypeDal>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
