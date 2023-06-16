namespace VijayLaxmi.Models
{
    public class Loan_Advance
    {

        public int Id { get; set; }
        public Int64 AAdharNo { get; set; }
        public string Type { get; set; }
        public string Narration { get; set; }
        public string ApplicationDocument { get; set; }
        public string SignedDocument { get; set; }
        public double Amount { get; set; }
        public DateTime Loan_AdvanceDate { get; set; }
        public bool Status { get; set; }
        public double EmiAmount { get; set; }
        public DateTime EmiDate { get; set; }
    }
}

