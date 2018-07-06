using System;
using Autofac;
using Resource.IDAL;
using Resource.DAL;

namespace Resource.DAL.Container
{

	 public class Container
    {
		public static IContainer autoContainer = null;

        public static T Resolve<T>()
        {
            try
            {
                if (autoContainer == null)
                    Initialise();
            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错:" + ex.Message);
            }
            return autoContainer.Resolve<T>();
        }

		public static void Initialise()
        {
            ContainerBuilder builder = new ContainerBuilder();
							builder.RegisterType<BuildingDAL>().As<IBuildingDAL>();     
							builder.RegisterType<CityDAL>().As<ICityDAL>();     
							builder.RegisterType<CubicleDAL>().As<ICubicleDAL>();     
							builder.RegisterType<FloorDAL>().As<IFloorDAL>();     
							builder.RegisterType<HomePageDAL>().As<IHomePageDAL>();     
							builder.RegisterType<MeetingRoomDAL>().As<IMeetingRoomDAL>();     
							builder.RegisterType<MenuDAL>().As<IMenuDAL>();     
							builder.RegisterType<PageFootDAL>().As<IPageFootDAL>();     
							builder.RegisterType<PagePositionDAL>().As<IPagePositionDAL>();     
							builder.RegisterType<ParkDAL>().As<IParkDAL>();     
							builder.RegisterType<PermissionDAL>().As<IPermissionDAL>();     
							builder.RegisterType<RegionDAL>().As<IRegionDAL>();     
							builder.RegisterType<RentTypeDAL>().As<IRentTypeDAL>();     
							builder.RegisterType<RGroupDAL>().As<IRGroupDAL>();     
							builder.RegisterType<RImageDAL>().As<IRImageDAL>();     
							builder.RegisterType<RoleDAL>().As<IRoleDAL>();     
							builder.RegisterType<RoomDAL>().As<IRoomDAL>();     
							builder.RegisterType<RPriceDAL>().As<IRPriceDAL>();     
							builder.RegisterType<RTypeDAL>().As<IRTypeDAL>();     
							builder.RegisterType<StageDAL>().As<IStageDAL>();     
							builder.RegisterType<UserDAL>().As<IUserDAL>();     				
            autoContainer = builder.Build();
        }
    }   

}
