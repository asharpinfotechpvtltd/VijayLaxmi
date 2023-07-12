using System.ComponentModel.DataAnnotations;

namespace VijayLaxmi.Models
{
    public class ManualSalaries
    {
        [Key]
        public int id { get; set; }
        public Int64 Empcode { get; set; }
        public string AAdharno { get; set; }
        public double TotalWorkingHours { get; set; }
        public double TotalAttendance { get; set; }
        public double MonthlySalary { get; set; }
        public double ManualSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Siteid { get; set; }
    }
}
