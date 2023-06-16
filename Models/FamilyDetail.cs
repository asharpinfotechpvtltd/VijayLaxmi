using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class FamilyDetail
    {
        [Key]
        public int Id { get; set; }
        public Int64 EmployeeAAdhar { get; set; }
        public string Relation { get; set; }
        public string Name { get; set; }
        public string DateofBirth { get; set; }
        public Int64 AAdhar { get; set; }
        public string Alternateno { get; set; }
    }
}
