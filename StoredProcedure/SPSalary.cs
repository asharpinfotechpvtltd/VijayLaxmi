namespace VijayLaxmi.StoredProcedure
{
    public class SPSalary
    {
        public Int64 ID { get; set; }
        public Int64 AadharNo { get; set; }

        public string Name { get; set; }
        public double InhandSalary { get; set; }
        public double PerHourRate { get; set; }
        public double TotalWorkingdays { get; set; }
        public double TotalWorkingHours { get; set; }
        public double MonthlyWorkingHours { get; set; }
        public Int32 TotalAttendance { get; set; }
        public double Empworkinghours { get; set; }
        public double Monthlysalary { get; set; }

    }
}
