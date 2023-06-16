namespace VijayLaxmi.StoredProcedure
{
    public class SpSearchEmployeeList
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public Int64 AAdharno { get; set; }
        public string Contactno { get; set; }
        public string Email { get; set; }
        public string Employeetype { get; set; }
        public string SiteName { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsVerified { get; set; }
    }
}
