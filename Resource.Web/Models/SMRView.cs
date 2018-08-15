using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class SMRView
    {
        public string RowPointer { get; set; }
        public string CRNo { get; set; }
        public string CRName { get; set; }
        public string ApplyUserName { get; set; }
        public string ApplyTel { get; set; }
        public DateTime CRBegReservedDate { get; set; }
        public DateTime CREndReservedDate { get; set; }
        public string Status { get; set; }

    }
}