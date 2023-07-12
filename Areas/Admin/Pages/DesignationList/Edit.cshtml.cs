using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.DesignationList
{
    public class EditModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public EditModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Designation Designation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                var designation = await _context.TblDesignation.FirstOrDefaultAsync(m => m.Id == id);
                if (designation == null)
                {
                    return NotFound();
                }
                Designation = designation;
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Designation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(Designation.Id))
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

        private bool SiteExists(long id)
        {
            return _context.TblDesignation.Any(e => e.Id == id);
        }
    }
}
