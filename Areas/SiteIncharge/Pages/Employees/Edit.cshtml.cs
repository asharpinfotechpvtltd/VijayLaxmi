using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public EditModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                if (id == null || _context.TblEmployees == null)
                {
                    return NotFound();
                }

                var employee = await _context.TblEmployees.FirstOrDefaultAsync(m => m.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }
                Employee = employee;
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

            _context.Attach(Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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

        private bool EmployeeExists(long id)
        {
            return _context.TblEmployees.Any(e => e.Id == id);
        }
    }
}
