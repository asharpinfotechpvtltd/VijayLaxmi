using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.StateGst
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<StateDetail> Details { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                Details = await _context.TblStateDetail.ToListAsync();

                return Page();
            }
        }
    }
}
