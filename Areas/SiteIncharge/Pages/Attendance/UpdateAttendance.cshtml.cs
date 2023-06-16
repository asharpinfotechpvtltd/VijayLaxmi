using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Attendance
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
        public List<SelectListItem> EmployeeList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            int SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            EmployeeList = await _context.TblEmployees.Where(e=>e.Site== SiteId).Select(s => new SelectListItem
            {
                Text = s.Name+"-"+s.AAdharno,
                Value = s.Id.ToString()
            }).ToListAsync();
            return Page();
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
                    WorkTime = Worktime
                };
                await _context.TblAttendence.AddAsync(attendance);
            }
            await _context.SaveChangesAsync();
            int SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));

            EmployeeList = await _context.TblEmployees.Where(e => e.Site == SiteId).Select(s => new SelectListItem
            {
                Text = s.Name + "-" + s.AAdharno,
                Value = s.Id.ToString()
            }).ToListAsync();
            return Page();
        }
    }
}
