namespace VijayLaxmi.StoredProcedure
{
    public class SPRequirement
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public bool Hostatus { get; set; }
        public bool SiteStatus { get; set; }
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }
    }
}
