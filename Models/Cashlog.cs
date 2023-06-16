using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class Cashlog
    {
        [Key]
        public int Id { get; set; }    
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public string Narration { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
