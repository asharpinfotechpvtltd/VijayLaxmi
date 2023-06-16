using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class Employee
    {

        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int Designation { get; set; }        
        public string Contactno { get; set; }
        public string Email { get; set; }
        public string Employeetype { get; set; }
        public string Addresslocal { get; set; }
        public string AddressPermanent { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public Int64 AAdharno { get; set; }
        public string? Pancard { get; set; }
        
        public string EPF { get; set; }
        public string EmployeeRole { get; set; }
        public string Image { get; set; }        
        public string FathersName { get; set; }
        public int Site { get; set; }        
        public DateTime AddedDate { get; set; }
        public double Salary { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public long? EsiNo { get; set; }
        public long? UANNo { get; set; }        
        public string? Reason { get; set; }
        public DateTime? Leavedate { get; set; }
    }
}

