//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.DAL
{
    using Autofac;
    using Resource.IDAL;
     public  class Container
        {
            /// <summary>
            /// IOC 容器
            /// </summary>
            public static IContainer container = null;
    
            /// <summary>
            /// 获取 IDal 的实例化对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T Resolve<T>()
            {
                try
                {
                    if (container == null)
                    {
                        Initialise();
                    }
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception("IOC实例化出错!" + ex.Message);
                }
    
                return container.Resolve<T>();
            }
    		/// <summary>
            /// 初始化
            /// </summary>
            public static void Initialise()
            {
                var builder = new ContainerBuilder();
                //格式：builder.RegisterType<xxxx>().As<Ixxxx>().InstancePerLifetimeScope();
    			            builder.RegisterType<BuildingDal>().As<IBuildingDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<CityDal>().As<ICityDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<FloorDal>().As<IFloorDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<MenuDal>().As<IMenuDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<ParkDal>().As<IParkDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<PermissionDal>().As<IPermissionDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RegionDal>().As<IRegionDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<ResourceDal>().As<IResourceDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RGroupDal>().As<IRGroupDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RImageDal>().As<IRImageDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RoleDal>().As<IRoleDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RPriceDal>().As<IRPriceDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<RTypeDal>().As<IRTypeDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<SProviderDal>().As<ISProviderDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<StageDal>().As<IStageDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<UnitDal>().As<IUnitDal>().InstancePerLifetimeScope();   
    			            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerLifetimeScope();   
    			      
                container = builder.Build();
            }
    }
}