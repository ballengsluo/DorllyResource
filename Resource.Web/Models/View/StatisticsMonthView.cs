using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resource.Web.Models
{
    public class StatisticsMonthView
    {
        public string ParkID { get; set; }
        public string ParkName { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string MonthTime { get; set; }
        public string RentRate { get; set; }
    }
}