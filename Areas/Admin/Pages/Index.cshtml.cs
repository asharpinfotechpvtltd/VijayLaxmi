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

namespace NieAdvisory.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;      

        //public IList<TodaysReminder> Reminders { get; set; }

        public int TotalSite { get; set; }
        public int TotalStaff { get; set; }
        public IList<Employee> PendingEmployeeList { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            //DateTime timeUtc = System.DateTime.UtcNow;
            //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            //string date = cstTime.Date.ToString("dd-MM-yyyy");
            //string time = cstTime.ToString("HH:mm");
            //var TodaysDate = new SqlParameter("@DATE", date);
            //Reminders = await _context.SpTodaysReminder.FromSqlRaw("SpTodaysReminder @DATE", TodaysDate).ToListAsync();
            TotalSite = _context.TblSite.Count();
            TotalStaff = _context.TblEmployees.Count();
            PendingEmployeeList = await _context.TblEmployees.Where(e=>e.IsVerified==false).ToListAsync();

            return Page();

        }

        public IActionResult OnGetLogout()
        {

            HttpContext.Session.Remove("Login");
            return Redirect("./Index");

        }

    }
}
