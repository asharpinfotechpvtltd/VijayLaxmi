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

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            TotalSite = _context.TblSite.Count();
            TotalStaff = _context.TblEmployees.Count();
            PendingEmployeeList = await _context.TblEmployees.Where(e => e.IsVerified == false && e.Reason==null).Take(5).ToListAsync();
            RejectedEmployeeList = await _context.TblEmployees.Where(e => e.IsVerified == false && e.Reason != null).ToListAsync();

            return Page();

        }

        public IActionResult OnGetLogout()
        {

            HttpContext.Session.Remove("Login");
            return Redirect("./Index");

        }

    }
}
