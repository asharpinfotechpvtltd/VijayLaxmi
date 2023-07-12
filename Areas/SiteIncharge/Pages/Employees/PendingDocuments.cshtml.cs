using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class PendingDocumentsModel : PageModel
    {
        ApplicationDbContext _context;
        public PendingDocumentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SpPendingDocument> SpPendingDocument { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                int site_id = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                var siteid = new SqlParameter("@siteid", site_id);
                SpPendingDocument = await _context.SpPendingDocument.FromSqlRaw<SpPendingDocument>("SpPendingDocument @siteid", siteid).ToListAsync();
                return Page();
            }
        }
    }
}
