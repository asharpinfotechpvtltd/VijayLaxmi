using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.Requirements
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SPRequirement> SPRequirement { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (_context.TblRequirement != null)
                {
                    SPRequirement = await _context.SPRequirement.FromSqlRaw("SPRequirement").ToListAsync();

                }
            }
            return Page();
        }
    }
}
