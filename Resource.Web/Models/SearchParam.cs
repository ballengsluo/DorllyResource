using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class SearchParam
    {
        public string CityID { get; set; }
        public string RegionID { get; set; }
        public string ParkID { get; set; }
        public string StageID { get; set; }
        public string BuildingID { get; set; }
        public string FloorID { get; set; }
        public string GroupID { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }        
        public string SubName { get; set; }
        public int? IntType { get; set; }
        public string StrType { get; set; }
        public int? IntStatus { get; set; }
        public bool? BooStatus { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
       

        public string City { get; set; }
        public string Region { get; set; }
        public string Park { get; set; }
        public string Stage { get; set; }
        public string Build { get; set; }
        public string Floor { get; set; }
        public string Group { get; set; }
        public string Room { get; set; }
        public string Cust { get; set; }
        public string Type { get; set; }
        public int? Kind { get; set; }
        public int? Status { get; set; }
        public bool? Enable { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? Stime { get; set; }
        public DateTime? Etime { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }


        #region 备选参数

        public string StrParam1 { get; set; }
        public string StrParam2 { get; set; }
        public string StrParam3 { get; set; }
        public string StrParam4 { get; set; }
        public string StrParam5 { get; set; }
        public string StrParam6 { get; set; }
        public string StrParam7 { get; set; }
        public string StrParam8 { get; set; }

        public int IntParam1 { get; set; }
        public int IntParam2 { get; set; }
        public int IntParam3 { get; set; }
        public int IntParam4 { get; set; }
        public int IntParam5 { get; set; }
        public int IntParam6 { get; set; }
        
        #endregion
    }
}