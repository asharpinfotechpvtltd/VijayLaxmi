using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class SitesIncharge
    {
        [Key]
        public int id { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public int SiteId { get; set; }
        public bool IsloginEnabled { get; set; }
        public Int64 AadharNo { get; set; }
        public DateTime Date { get; set; }
    }
}
