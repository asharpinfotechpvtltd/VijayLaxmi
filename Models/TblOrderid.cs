using System;
using System.Collections.Generic;

namespace VijayLaxmi.Models
{
    public partial class TblOrderid
    {
        public int Id { get; set; }
        public string ServicePeriod { get; set; } = null!;
        public double Amount { get; set; }       
        public DateTime Date { get; set; }
        public string Orderid { get; set; } = null!;       
        public string PaymentStatus { get; set; } = null!;
        public int Siteid { get; set; }
        public string? VendorCode { get; set; }
        
    }
}
