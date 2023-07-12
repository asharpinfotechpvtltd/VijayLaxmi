using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Attendance
{
    public class MissingOuttimeListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MissingOuttimeListModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<SPMissingOutTime> MissingOutTime { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                int SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                var Site_id = new SqlParameter("@Siteid", SiteId);
                MissingOutTime = await _context.SPMissingOutTime.FromSqlRaw("SPMissingOutTime @Siteid", Site_id).ToListAsync();

                return Page();
            }
        }
    }
}
