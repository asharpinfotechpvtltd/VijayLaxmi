using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.HR.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
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
                else
                {
                    Employee = employee;
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.TblEmployees == null)
            {
                return NotFound();
            }
            var employee = await _context.TblEmployees.FindAsync(id);

            if (employee != null)
            {
                Employee = employee;
                _context.TblEmployees.Remove(Employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
