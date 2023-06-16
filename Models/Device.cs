using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Deviceid { get; set; }
        public Int64 SiteId { get; set; }
        public string ApiUrl { get; set; }
    }
}
