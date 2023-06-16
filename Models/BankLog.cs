using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class BankLog
    {
        [Key]
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; } 
        public string Narration { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
