//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.BLL
{
    using Resource.IBLL;
    using Resource.Model;
    using Resource.IDAL;
    using Resource.DAL;
    using System;
    using System.Collections.Generic;
    
    public partial class StageService:BaseService<T_Stage>,IStageService
    {
    	private IStageDal StageDal=DAL.Container.Resolve<IStageDal>();
    	public override void SetDal()
        {
                Dal = StageDal;
        }
    }
}
