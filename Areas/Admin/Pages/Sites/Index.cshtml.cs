using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Sites
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Site> Site { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (_context.TblSite != null)
                {
                    Site = await _context.TblSite.ToListAsync();
                }
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
