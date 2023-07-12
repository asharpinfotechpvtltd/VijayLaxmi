using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.GeneratedSalary
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ManualSalaries ManualSalaries { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblManualSalary == null)
            {
                return NotFound();
            }

            var manualsalaries = await _context.TblManualSalary.FirstOrDefaultAsync(m => m.id == id);

            if (manualsalaries == null)
            {
                return NotFound();
            }
            else 
            {
                ManualSalaries = manualsalaries;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblManualSalary == null)
            {
                return NotFound();
            }
            var manualsalaries = await _context.TblManualSalary.FindAsync(id);

            if (manualsalaries != null)
            {
                ManualSalaries = manualsalaries;
                _context.TblManualSalary.Remove(ManualSalaries);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
