using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.StoredProcedure
{
    public class SPSiteInCharge
    {
        
        public int Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public string Name { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public bool IsloginEnabled { get; set; }
    }
}
