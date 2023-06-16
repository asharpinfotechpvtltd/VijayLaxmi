using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class Admin
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
