
using Resource.IBLL;
using Resource.Model;
using Resource.IDAL;
using Resource.DAL.Container;
namespace Resource.BLL
{
	 public partial class BuildingService :BaseService<T_Building>,IBuildingService
    {
		private IBuildingDAL dal=Container.Resolve<IBuildingDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class CityService :BaseService<T_City>,ICityService
    {
		private ICityDAL dal=Container.Resolve<ICityDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class CubicleService :BaseService<T_Cubicle>,ICubicleService
    {
		private ICubicleDAL dal=Container.Resolve<ICubicleDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class FloorService :BaseService<T_Floor>,IFloorService
    {
		private IFloorDAL dal=Container.Resolve<IFloorDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class HomePageService :BaseService<T_HomePage>,IHomePageService
    {
		private IHomePageDAL dal=Container.Resolve<IHomePageDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class MeetingRoomService :BaseService<T_MeetingRoom>,IMeetingRoomService
    {
		private IMeetingRoomDAL dal=Container.Resolve<IMeetingRoomDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class MenuService :BaseService<T_Menu>,IMenuService
    {
		private IMenuDAL dal=Container.Resolve<IMenuDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class PageFootService :BaseService<T_PageFoot>,IPageFootService
    {
		private IPageFootDAL dal=Container.Resolve<IPageFootDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class PagePositionService :BaseService<T_PagePosition>,IPagePositionService
    {
		private IPagePositionDAL dal=Container.Resolve<IPagePositionDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class ParkService :BaseService<T_Park>,IParkService
    {
		private IParkDAL dal=Container.Resolve<IParkDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class PermissionService :BaseService<T_Permission>,IPermissionService
    {
		private IPermissionDAL dal=Container.Resolve<IPermissionDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RegionService :BaseService<T_Region>,IRegionService
    {
		private IRegionDAL dal=Container.Resolve<IRegionDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RentTypeService :BaseService<T_RentType>,IRentTypeService
    {
		private IRentTypeDAL dal=Container.Resolve<IRentTypeDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RGroupService :BaseService<T_RGroup>,IRGroupService
    {
		private IRGroupDAL dal=Container.Resolve<IRGroupDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RImageService :BaseService<T_RImage>,IRImageService
    {
		private IRImageDAL dal=Container.Resolve<IRImageDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RoleService :BaseService<T_Role>,IRoleService
    {
		private IRoleDAL dal=Container.Resolve<IRoleDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RoomService :BaseService<T_Room>,IRoomService
    {
		private IRoomDAL dal=Container.Resolve<IRoomDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RPriceService :BaseService<T_RPrice>,IRPriceService
    {
		private IRPriceDAL dal=Container.Resolve<IRPriceDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class RTypeService :BaseService<T_RType>,IRTypeService
    {
		private IRTypeDAL dal=Container.Resolve<IRTypeDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class StageService :BaseService<T_Stage>,IStageService
    {
		private IStageDAL dal=Container.Resolve<IStageDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	 public partial class UserService :BaseService<T_User>,IUserService
    {
		private IUserDAL dal=Container.Resolve<IUserDAL>();
		public override void SetDal()
        {
                Dal = dal;
        }
    }   
	
}
