namespace VijayLaxmi.StoredProcedure
{
    public class SpBankTransaction
    {
        public long AccountNumber { get; set; }
        public string AccountName { get; set; }
        public double OpeningBalance { get; set; }
        public double TotalDebit { get; set; }
        public double TotalCredit { get; set; }
        public double AvailableBalance { get; set; }

    }
}
