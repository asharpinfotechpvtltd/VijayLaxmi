using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.Classes;

namespace Monash.Areas.Admin.Pages.Bill
{
    public class ProductBillModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductBillModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> SiteName { get; set; }
        public List<SelectListItem> Product { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> GstState { get; set; }


        public async Task<IActionResult> OnGet()
        {

            string EmpCode = HttpContext.Session.GetString("Login");
            SiteName = await _context.TblSite.Select(a => new SelectListItem { Text = a.SiteName, Value = Convert.ToString(a.Siteid) }).ToListAsync();
            GstState = await _context.TblGstState.Select(a => new SelectListItem { Text = a.StateName, Value = Convert.ToString(a.StateName) }).ToListAsync();
            return Page();



        }

        //[BindProperty]
        //public Bills Bills { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string servicePeriodfrom, string servicePeriodTo, string SiteName, double FinalAmount, string VendorCode)
        {
            GetUserDate date = new GetUserDate();

            GetOrderId orderid = new GetOrderId(_context);


            string Orderid = await orderid.GetOrder(servicePeriodfrom + "-" + servicePeriodTo, FinalAmount, SiteName, VendorCode);
            string Po = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Po);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    string hsnno = Convert.ToString(dt.Rows[i][0].ToString());
                    string description = Convert.ToString(dt.Rows[i][1]);
                    string Hrs = Convert.ToString(dt.Rows[i][2]);
                    string rate = Convert.ToString(dt.Rows[i][3]);
                    BillDescription child = new BillDescription()
                    {
                        Orderid = Orderid,
                        Hsnno = hsnno,
                        Hrs = Hrs,
                        Rate = rate,
                        Sitedescid = description
                    };
                    await _context.TblBillDescription.AddAsync(child);
                    await _context.SaveChangesAsync();
                }
            }

            return Redirect("ProductPrintBill?Orderid=" + Orderid);

        }



    }
}
