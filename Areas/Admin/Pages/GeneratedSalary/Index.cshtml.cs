using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.GeneratedSalary
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;
        public List<SelectListItem> SiteName { get; set; }
        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ManualSalaries> ManualSalaries { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int month { get; set; }
        [BindProperty(SupportsGet = true)]
        public int year { get; set; }
        [BindProperty(SupportsGet = true)]
        public int sitename { get; set; }
        public async Task OnGetAsync()
        {
            SiteName = await _context.TblSite.Select(s => new SelectListItem { Text = s.SiteName, Value = s.Siteid.ToString() }).ToListAsync();
            if (_context.TblManualSalary != null)
            {
                ManualSalaries = await _context.TblManualSalary.Take(5).ToListAsync();
            }
        }

        public async Task<IActionResult> OnGetSearch()
        {
            ManualSalaries = await _context.TblManualSalary.Where(e => e.Month == month && e.Year == year && e.Siteid == sitename).ToListAsync();
            SiteName = await _context.TblSite.Select(s => new SelectListItem { Text = s.SiteName, Value = s.Siteid.ToString() }).ToListAsync();

            return Page();
        }
    }
}
