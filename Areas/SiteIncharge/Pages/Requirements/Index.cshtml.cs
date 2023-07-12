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
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Requirement> Requirement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                if (_context.TblRequirement != null)
                {
                    int site = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                    Requirement = await _context.TblRequirement.Where(e => e.SiteId == site).OrderByDescending(e => e.Id).ToListAsync();
                }
                return Page();
            }
        }
    }
}
