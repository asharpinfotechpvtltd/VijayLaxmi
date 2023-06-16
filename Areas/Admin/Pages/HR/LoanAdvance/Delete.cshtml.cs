using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.HR.LoanAdvance
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Loan_Advance Loan_Advance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblLoanAdvance == null)
            {
                return NotFound();
            }

            var loan_advance = await _context.TblLoanAdvance.FirstOrDefaultAsync(m => m.Id == id);

            if (loan_advance == null)
            {
                return NotFound();
            }
            else 
            {
                Loan_Advance = loan_advance;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblLoanAdvance == null)
            {
                return NotFound();
            }
            var loan_advance = await _context.TblLoanAdvance.FindAsync(id);

            if (loan_advance != null)
            {
                loan_advance.Status = false;
                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
