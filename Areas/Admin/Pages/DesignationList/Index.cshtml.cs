using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.DesignationList
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;
        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Designation> DesignationList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (_context.TblEmployees != null)
                {
                    DesignationList = await _context.TblDesignation.ToListAsync();
                }
                return Page();
            }
        }
        
    }
}
