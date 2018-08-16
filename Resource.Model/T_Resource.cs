//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class T_Resource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Resource()
        {
            this.T_ResourceImg = new HashSet<T_ResourceImg>();
            this.T_ResourcePrice = new HashSet<T_ResourcePrice>();
            this.T_ResourcePublic = new HashSet<T_ResourcePublic>();
            this.T_Order = new HashSet<T_Order>();
        }
    
        public string ID { get; set; }
        public string Name { get; set; }
        public string ParkID { get; set; }
        public string ParentID { get; set; }
        public string GroupID { get; set; }
        public string CustID { get; set; }
        public int ResourceKindID { get; set; }
        public string ResourceTypeID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> OrderPrice { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> Enable { get; set; }
        public Nullable<System.DateTime> RentBeginTime { get; set; }
        public Nullable<System.DateTime> RentEndTime { get; set; }
        public Nullable<decimal> Area { get; set; }
        public Nullable<decimal> RentArea { get; set; }
        public Nullable<decimal> StartArea { get; set; }
        public string RangeArea { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<int> RentNum { get; set; }
        public Nullable<int> StartNum { get; set; }
        public string RangeNum { get; set; }
        public string Size { get; set; }
        public string RentSize { get; set; }
        public string StartSize { get; set; }
        public string RangeSize { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string Location { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ResourceImg> T_ResourceImg { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ResourcePrice> T_ResourcePrice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ResourcePublic> T_ResourcePublic { get; set; }
        public virtual T_ResourceKind T_ResourceKind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Order> T_Order { get; set; }
    }
}
