using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Incharge
{
    public class EditModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public EditModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SitesIncharge SitesIncharge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (id == null || _context.TblSiteIncharge == null)
                {
                    return NotFound();
                }

                var sitesincharge = await _context.TblSiteIncharge.FirstOrDefaultAsync(m => m.id == id);
                if (sitesincharge == null)
                {
                    return NotFound();
                }
                SitesIncharge = sitesincharge;
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SitesIncharge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SitesInchargeExists(SitesIncharge.id))
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

        private bool SitesInchargeExists(int id)
        {
            return _context.TblSiteIncharge.Any(e => e.id == id);
        }
    }
}
