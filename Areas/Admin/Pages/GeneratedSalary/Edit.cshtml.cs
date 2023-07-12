using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.GeneratedSalary
{
    public class EditModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public EditModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ManualSalaries ManualSalaries { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblManualSalary == null)
            {
                return NotFound();
            }

            var manualsalaries =  await _context.TblManualSalary.FirstOrDefaultAsync(m => m.id == id);
            if (manualsalaries == null)
            {
                return NotFound();
            }
            ManualSalaries = manualsalaries;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ManualSalaries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManualSalariesExists(ManualSalaries.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ManualSalariesExists(int id)
        {
          return _context.TblManualSalary.Any(e => e.id == id);
        }
    }
}
