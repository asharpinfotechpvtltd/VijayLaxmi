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
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public Site Site { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.TblSite == null)
            {
                return NotFound();
            }

            var site = await _context.TblSite.FirstOrDefaultAsync(m => m.Siteid == id);
            if (site == null)
            {
                return NotFound();
            }
            else 
            {
                Site = site;
            }
            return Page();
        }
    }
}
