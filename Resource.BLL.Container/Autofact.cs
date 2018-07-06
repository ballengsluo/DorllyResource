using System;
using Autofac;
using Resource.IBLL;
using Resource.BLL;

namespace Resource.BLL.Container
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
							builder.RegisterType<BuildingService>().As<IBuildingService>();     
							builder.RegisterType<CityService>().As<ICityService>();     
							builder.RegisterType<CubicleService>().As<ICubicleService>();     
							builder.RegisterType<FloorService>().As<IFloorService>();     
							builder.RegisterType<HomePageService>().As<IHomePageService>();     
							builder.RegisterType<MeetingRoomService>().As<IMeetingRoomService>();     
							builder.RegisterType<MenuService>().As<IMenuService>();     
							builder.RegisterType<PageFootService>().As<IPageFootService>();     
							builder.RegisterType<PagePositionService>().As<IPagePositionService>();     
							builder.RegisterType<ParkService>().As<IParkService>();     
							builder.RegisterType<PermissionService>().As<IPermissionService>();     
							builder.RegisterType<RegionService>().As<IRegionService>();     
							builder.RegisterType<RentTypeService>().As<IRentTypeService>();     
							builder.RegisterType<RGroupService>().As<IRGroupService>();     
							builder.RegisterType<RImageService>().As<IRImageService>();     
							builder.RegisterType<RoleService>().As<IRoleService>();     
							builder.RegisterType<RoomService>().As<IRoomService>();     
							builder.RegisterType<RPriceService>().As<IRPriceService>();     
							builder.RegisterType<RTypeService>().As<IRTypeService>();     
							builder.RegisterType<StageService>().As<IStageService>();     
							builder.RegisterType<UserService>().As<IUserService>();     
				
            autoContainer = builder.Build();
        }
    } 
}
