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
    
    public partial class T_LoginInfo
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> FailTime { get; set; }
    
        public virtual T_User T_User { get; set; }
    }
}