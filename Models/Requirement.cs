namespace VijayLaxmi.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Qty { get; set; }
        public bool Hostatus { get; set; }
        public bool SiteStatus { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
