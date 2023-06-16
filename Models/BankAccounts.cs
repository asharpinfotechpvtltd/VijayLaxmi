using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class BankAccounts
    {
        [Key]
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public double OpeningBalance { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
