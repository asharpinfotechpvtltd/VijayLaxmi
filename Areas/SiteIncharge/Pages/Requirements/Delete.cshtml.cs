using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Requirements
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Requirement Requirement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblRequirement == null)
            {
                return NotFound();
            }

            var requirement = await _context.TblRequirement.FirstOrDefaultAsync(m => m.Id == id);

            if (requirement == null)
            {
                return NotFound();
            }
            else 
            {
                Requirement = requirement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblRequirement == null)
            {
                return NotFound();
            }
            var requirement = await _context.TblRequirement.FindAsync(id);

            if (requirement != null)
            {
                Requirement = requirement;
                _context.TblRequirement.Remove(Requirement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
