using VijayLaxmi.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using VijayLaxmi.Classes;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.EmpAttendance
{
    public class ViewAttendanceModel : PageModel
    {

        ApplicationDbContext Context;
        public ViewAttendanceModel(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<string> Attendance { get; set; }
        public List<SelectListItem> Employee { get; set; }
        public List<SPAttendanceList> AttendanceList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Employeeid { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime Startdate { get; set; } = DateTime.Now;

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int SiteId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                Employee = await Context.TblEmployees.Where(e => e.Site == SiteId).Select(s => new SelectListItem
                {
                    Text = s.Name + "-" + s.AAdharno,
                    Value = s.Id.ToString()
                }).ToListAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnGetSearch(int Employeeid)
        {
            SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
           
            string start = Startdate.ToString("dd/MM/yyyy");
            string end = EndDate.ToString("dd/MM/yyyy");
          

            string[] StartDate = start.Split("/");
            int date = Convert.ToInt32(StartDate[0]);
            int StartMonth = Convert.ToInt32(StartDate[1]);
            int StartYear = Convert.ToInt32(StartDate[2]);

            string[] Enddates = end.Split("/");
            int Enddate = Convert.ToInt32(Enddates[0]);
            int EndMonth = Convert.ToInt32(Enddates[1]);
            int EndYear = Convert.ToInt32(Enddates[2]);
            var sid = new SqlParameter("@SiteId", SiteId);
            var stDate = new SqlParameter("@StartDate", date);
            var endDate = new SqlParameter("@Enddate", Enddate);
            var stMonth = new SqlParameter("@StartMonth", StartMonth);
            var endMonth = new SqlParameter("@EndMonth", EndMonth);
            var stYear = new SqlParameter("@StartYear", StartYear);
            var endYear = new SqlParameter("@EndYear", EndYear);
            var EmpCode = new SqlParameter("@EmpCode", Employeeid);

            AttendanceList = await Context.SPAttendanceList.FromSqlRaw("SPAttendanceList @SiteId,@StartDate,@EndDate,@StartMonth,@EndMonth,@StartYear,@EndYear,@EmpCode", sid, stDate, endDate, stMonth, endMonth, stYear, endYear, EmpCode).ToListAsync();
            return Page();
        }
    }
}





