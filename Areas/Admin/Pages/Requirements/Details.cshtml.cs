using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Requirements
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Requirement Requirement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
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
        }
    }
}
