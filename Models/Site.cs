namespace VijayLaxmi.Models
{
    public class Site
    {

        public long Siteid { get; set; }
        public string SiteName { get; set; }
        public string Address { get; set; }
        public string Gstno { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }        
        public int StateCode { get; set; }

    }
}
