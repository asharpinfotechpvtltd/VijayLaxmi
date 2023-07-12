using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.LoanAdvance
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Loan_Advance> Loan_Advance { get; set; }
        public List<Loan_AdvanceAdjustment> Adjustments { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? loanid, Int64 AAdharNo)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                Employee = await _context.TblEmployees.SingleOrDefaultAsync(e => e.AAdharno == AAdharNo);
                Loan_Advance = await _context.TblLoanAdvance.Where(m => m.AAdharNo == AAdharNo).ToListAsync();
                Adjustments = await _context.TblLoanAdvanceAdjustment.Where(m => m.LoanAdvanceAccountnumber == loanid).ToListAsync();
                return Page();
            }
        }
    }
}
