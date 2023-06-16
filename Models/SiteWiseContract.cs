using System.ComponentModel.DataAnnotations;
using VijayLaxmi.Classes;

namespace VijayLaxmi.Models
{
    public class SiteWiseContract
    {
        [Key]
        public int ContractId { get; set; }
        public int SiteId { get; set; }
        public string ContractName { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
