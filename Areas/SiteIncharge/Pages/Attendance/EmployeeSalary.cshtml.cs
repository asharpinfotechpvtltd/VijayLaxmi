using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Attendance
{
    public class EmployeeSalaryModel : PageModel
    {
        ApplicationDbContext Context;
        public List<SPSalary> SPSalary { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }
        [BindProperty]
        public int siteid { get; set; }
        public EmployeeSalaryModel(ApplicationDbContext context)
        {
            Context = context;
        }
        public  void OnGet()
        {
             siteid = Convert.ToInt32(HttpContext.Session.GetString("siteid"));

        }
        public async Task<IActionResult> OnGetSearch()
        {
            siteid = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            var sid = new SqlParameter("@SiteId", siteid);
            var SelectedMonth = new SqlParameter("@Month", Month);
            var SelectedYear = new SqlParameter("@Year", Year);
            
            SPSalary = await Context.SPSalary.FromSqlRaw("SPSalary @SiteId,@Month,@Year", sid, SelectedMonth, SelectedYear).ToListAsync();
            return Page();
        }
    }
}
