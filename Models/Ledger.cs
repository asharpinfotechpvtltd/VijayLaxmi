namespace VijayLaxmi.Models
{
    public class Ledger
    {

        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public double? PaidAmount { get; set; }
        public double? ReceivedAmount { get; set; }
    }
}

