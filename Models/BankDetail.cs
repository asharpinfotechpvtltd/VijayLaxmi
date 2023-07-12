using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class BankDetail
    {
        [Key]
        public int Id { get; set; }
        public Int64 AAdharNo { get; set; }
        public string Bankname { get; set; }
        public string Accountholdername { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCODE { get; set; }
        public string BranchName { get; set; }
#nullable enable
        public string? Filename { get; set; }
        
       
    }
}
