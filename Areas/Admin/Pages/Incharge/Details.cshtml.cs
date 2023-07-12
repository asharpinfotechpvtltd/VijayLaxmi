using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Incharge
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public SitesIncharge SitesIncharge { get; set; }

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
                else
                {
                    SitesIncharge = sitesincharge;
                }
                return Page();
            }
        }
    }
}
