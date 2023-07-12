using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VijayLaxmi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using VijayLaxmi.StoredProcedure;

namespace NieAdvisory.Areas.SiteIncharge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;


        public int TotalSite { get; set; }
        public int TotalStaff { get; set; }
        public IList<Employee> PendingEmployeeList { get; set; }
        public IList<Employee> RejectedEmployeeList { get; set; }
        public IList<SPMissingOutTime> MissingOutTime { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
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
                TotalSite = _context.TblSite.Count();
                TotalStaff = _context.TblEmployees.Count();
                PendingEmployeeList = await _context.TblEmployees.Where(e => e.IsVerified == false && e.Reason == null && e.Site == SiteId).Take(5).ToListAsync();
                RejectedEmployeeList = await _context.TblEmployees.Where(e => e.IsVerified == false && e.Reason != null && e.Site == SiteId).ToListAsync();
                MissingOutTime = await _context.SPMissingOutTime.FromSqlRaw("SPMissingOutTime @Siteid", Site_id).ToListAsync();
                return Page();
            }

        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("SiteIncharge");
            return Redirect("./Index");
        }

    }
}
