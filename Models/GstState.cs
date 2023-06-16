using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class GstState
    {
        public string StateName { get; set; }
        public float GSTStateCode { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
