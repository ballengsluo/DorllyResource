using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class SearchParam
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Park { get; set; }
        public string Stage { get; set; }
        public string Build { get; set; }
        public string Floor { get; set; }
        public string Group { get; set; }
        public string Room { get; set; }
        public string Cust { get; set; }
        public string SType { get; set; }
        public int? IType { get; set; }
        public int? Kind { get; set; }
        public int? Status { get; set; }
        public bool? Enable { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? Stime { get; set; }
        public DateTime? Etime { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
}