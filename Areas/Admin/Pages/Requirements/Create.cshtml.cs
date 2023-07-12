using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Requirements
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public CreateModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                return Page();
            }
        }

        [BindProperty]
        public Requirement Requirement { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblRequirement.Add(Requirement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
