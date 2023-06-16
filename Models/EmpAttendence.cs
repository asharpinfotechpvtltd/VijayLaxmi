using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VijayLaxmi.Models
{
    public class EmpAttendence
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public int Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Attendence { get; set; }
        public int Marked { get; set; }
        public string InTime { get; set; }
        public string Outtime { get; set; }
        public string WorkTime { get; set; }
        public int SiteId { get; set; } 
        public DateTime FetchedDate { get;set; }


    }
}
