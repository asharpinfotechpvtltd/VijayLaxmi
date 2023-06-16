using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }
        public string DesignationName { get; set; }
    }
}
