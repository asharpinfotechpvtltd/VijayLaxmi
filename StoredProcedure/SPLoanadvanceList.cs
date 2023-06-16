namespace VijayLaxmi.StoredProcedure
{
    public class SPLoanadvanceList
    {
        public int Id { get; set; }
        public Int64 AAdharNo { get; set; }
        public string Name { get; set; }
        public string Contactno { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string ApplicationDocument { get; set; }
        public string SignedDocument { get; set; }
        public bool Status { get; set; }
        public DateTime Loan_AdvanceDate { get; set; }
        public string Fathersname { get; set; }
        public string Sitename { get; set; }
    }
}
