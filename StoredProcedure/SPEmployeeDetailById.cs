using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.StoredProcedure
{
    public class SPEmployeeDetailById
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Designation { get; set; }
        public string DesignationName { get; set; }
        public string SiteName { get; set; }
        public bool Islogin { get; set; }
        public string Contactno { get; set; }
        public string Email { get; set; }
        public string Employeetype { get; set; }
        public string Addresslocal { get; set; }
        public string AddressPermanent { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AAdharno { get; set; }
        public string Pancard { get; set; }
        public string Password { get; set; }
        public string PPF { get; set; }
        public string EmployeeRole { get; set; }
        public string Image { get; set; }
        public string UserDocument { get; set; }
        public string FathersName { get; set; }
        public int Site { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public double Salary { get; set; }
        public bool IsVerified { get; set; }
    }
}
