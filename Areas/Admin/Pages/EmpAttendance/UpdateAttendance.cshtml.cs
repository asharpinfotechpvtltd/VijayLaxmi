using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.EmpAttendance
{
    public class UpdateAttendanceModel : PageModel
    {
        ApplicationDbContext _context;
        [BindProperty]
        public EmpAttendence EmpAttendence { get; set; }
        public UpdateAttendanceModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> SiteList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                SiteList = await _context.TblSite.Select(s => new SelectListItem
                {
                    Text = s.SiteName,
                    Value = s.Siteid.ToString()
                }).ToListAsync();
                return Page();
            }
        }
        public async Task<IActionResult> OnPost(string date, string Attendance, int SiteName, string intime, string Outtime, string Worktime)
        {
            string[] dates = date.Split('-').ToString().Split('/');
            int Date = Convert.ToInt32(dates[2]);
            int Month = Convert.ToInt32(dates[1]);
            int Year = Convert.ToInt32(dates[0]);

            var Checkattendance = await _context.TblAttendence.SingleOrDefaultAsync(e => e.Date == Date && e.Month == Month && e.Year == Year && e.EmpCode == EmpAttendence.EmpCode);
            if (Checkattendance != null)
            {
                Checkattendance.Date = Convert.ToInt32(dates[2]);
                Checkattendance.Month = Convert.ToInt32(dates[1]);
                Checkattendance.Year = Convert.ToInt32(dates[0]);
                Checkattendance.Attendence = Attendance;
                Checkattendance.SiteId = SiteName;
                Checkattendance.Marked = 1;
                Checkattendance.InTime = intime;
                Checkattendance.Outtime = Outtime;
            }
            else
            {

                EmpAttendence attendance = new EmpAttendence
                {
                    Attendence = Attendance,
                    Date = Convert.ToInt32(dates[2]),
                    Month = Convert.ToInt32(dates[1]),
                    Year = Convert.ToInt32(dates[0]),
                    EmpCode = EmpAttendence.EmpCode,
                    InTime = intime,
                    Outtime = Outtime,
                    SiteId = SiteName,
                    Marked = 1,
                    //WorkTime=Worktime
                };
                await _context.TblAttendence.AddAsync(attendance);
            }
            await _context.SaveChangesAsync();

            SiteList = await _context.TblSite.Select(s => new SelectListItem
            {
                Text = s.SiteName,
                Value = s.Siteid.ToString()
            }).ToListAsync();
            return Page();
        }
    }
}
