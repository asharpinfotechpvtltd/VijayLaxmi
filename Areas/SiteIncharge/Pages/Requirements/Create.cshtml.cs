using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Requirements
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
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
            int site = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            GetUserDate date = new GetUserDate();
            Requirement.SiteId = site;
            Requirement.AddedDate = date.ReturnDate();
            _context.TblRequirement.Add(Requirement);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
