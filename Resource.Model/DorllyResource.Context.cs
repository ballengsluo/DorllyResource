﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DorllyResourceEntities : DbContext
    {
        public DorllyResourceEntities()
            : base("name=DorllyResourceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<V_Building> V_Building { get; set; }
        public virtual DbSet<V_Floor> V_Floor { get; set; }
        public virtual DbSet<V_Func> V_Func { get; set; }
        public virtual DbSet<V_Park> V_Park { get; set; }
        public virtual DbSet<V_Public> V_Public { get; set; }
        public virtual DbSet<V_Resource> V_Resource { get; set; }
        public virtual DbSet<V_Stage> V_Stage { get; set; }
        public virtual DbSet<T_Building> T_Building { get; set; }
        public virtual DbSet<T_City> T_City { get; set; }
        public virtual DbSet<T_Cust> T_Cust { get; set; }
        public virtual DbSet<T_Floor> T_Floor { get; set; }
        public virtual DbSet<T_HomePage> T_HomePage { get; set; }
        public virtual DbSet<T_LoginInfo> T_LoginInfo { get; set; }
        public virtual DbSet<T_Menu> T_Menu { get; set; }
        public virtual DbSet<T_MenuFunc> T_MenuFunc { get; set; }
        public virtual DbSet<T_PageFoot> T_PageFoot { get; set; }
        public virtual DbSet<T_Park> T_Park { get; set; }
        public virtual DbSet<T_Region> T_Region { get; set; }
        public virtual DbSet<T_Resource> T_Resource { get; set; }
        public virtual DbSet<T_ResourceGroup> T_ResourceGroup { get; set; }
        public virtual DbSet<T_ResourceImg> T_ResourceImg { get; set; }
        public virtual DbSet<T_ResourceKind> T_ResourceKind { get; set; }
        public virtual DbSet<T_ResourcePrice> T_ResourcePrice { get; set; }
        public virtual DbSet<T_ResourcePublic> T_ResourcePublic { get; set; }
        public virtual DbSet<T_ResourceType> T_ResourceType { get; set; }
        public virtual DbSet<T_Role> T_Role { get; set; }
        public virtual DbSet<T_RoleFunc> T_RoleFunc { get; set; }
        public virtual DbSet<T_RoleMenu> T_RoleMenu { get; set; }
        public virtual DbSet<T_Stage> T_Stage { get; set; }
        public virtual DbSet<T_UserData> T_UserData { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<T_ResourceStatus> T_ResourceStatus { get; set; }
        public virtual DbSet<V_ResourceStatus> V_ResourceStatus { get; set; }
        public virtual DbSet<T_Order> T_Order { get; set; }
        public virtual DbSet<V_Order> V_Order { get; set; }
    
        public virtual ObjectResult<Pro_GetFunc_Result> Pro_GetFunc(string account, string menuPath)
        {
            var accountParameter = account != null ?
                new ObjectParameter("Account", account) :
                new ObjectParameter("Account", typeof(string));
    
            var menuPathParameter = menuPath != null ?
                new ObjectParameter("MenuPath", menuPath) :
                new ObjectParameter("MenuPath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_GetFunc_Result>("Pro_GetFunc", accountParameter, menuPathParameter);
        }
    }
}
