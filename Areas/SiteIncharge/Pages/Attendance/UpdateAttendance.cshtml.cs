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
        public string Todaysdate { get; set; }
        public string Lastdate { get; set; }
        public string LastToLastdate { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                int SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                Todaysdate = DateTime.Now.ToString("dd/MM/yyyy");
                Lastdate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                LastToLastdate = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                EmployeeList = await _context.TblEmployees.Where(e => e.Site == SiteId).Select(s => new SelectListItem
                {
                    Text = s.Name + "-" + s.AAdharno,
                    Value = s.Id.ToString()
                }).ToListAsync();
                return Page();
            }
        }
        public async Task<IActionResult> OnPost(string date, string Attendance, string intime, string Outtime, double Worktime)
        {
            try
            {
                int SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                GetUserDate Userdate = new GetUserDate();
                string[] dates = date.Split('/');
                int Date = Convert.ToInt32(dates[2]);
                int Month = Convert.ToInt32(dates[1]);
                int Year = Convert.ToInt32(dates[0]);

                var Checkattendance = await _context.TblAttendence.SingleOrDefaultAsync(e => e.Date == Date && e.Month == Month && e.Year == Year && e.EmpCode == EmpAttendence.EmpCode);
                if (Checkattendance != null)
                {
                    Checkattendance.Date = Convert.ToInt32(dates[0]);
                    Checkattendance.Month = Convert.ToInt32(dates[1]);
                    Checkattendance.Year = Convert.ToInt32(dates[2]);
                    Checkattendance.Attendence = Attendance;
                    Checkattendance.SiteId = SiteId;
                    Checkattendance.Marked = 1;
                    Checkattendance.InTime = intime;
                    Checkattendance.Outtime = Outtime;
                }
                else
                {

                    EmpAttendence attendance = new EmpAttendence
                    {
                        Attendence = Attendance,
                        Date = Convert.ToInt32(dates[0]),
                        Month = Convert.ToInt32(dates[1]),
                        Year = Convert.ToInt32(dates[2]),
                        EmpCode = EmpAttendence.EmpCode,
                        InTime = intime,
                        Outtime = Outtime,
                        SiteId = SiteId,
                        Marked = 1,
                        WorkTime = Worktime,
                        FetchedDate = Userdate.ReturnDate().ToShortDateString(),

                    };
                    await _context.TblAttendence.AddAsync(attendance);
                }
                await _context.SaveChangesAsync();

                Todaysdate = DateTime.Now.ToString("dd/MM/yyyy");
                Lastdate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                LastToLastdate = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                EmployeeList = await _context.TblEmployees.Where(e => e.Site == SiteId).Select(s => new SelectListItem
                {
                    Text = s.Name + "-" + s.AAdharno,
                    Value = s.Id.ToString()
                }).ToListAsync();
            }
            catch (Exception)
            {
                return Redirect("../Index");
            }
            return Page();
        }
    }
}
