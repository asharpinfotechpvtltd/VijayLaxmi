using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Attendance
{
    public class EditOutTimeModel : PageModel
    {
        ApplicationDbContext _context;
        public EditOutTimeModel(ApplicationDbContext context)
        {
                _context = context;
        }
        [BindProperty]
        public EmpAttendence Attendance { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                Attendance = await _context.TblAttendence.SingleOrDefaultAsync(e => e.Id == id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(int id, string Outtime,double Worktime)
        {
            var attendence=await _context.TblAttendence.FindAsync(id);
            if(attendence!=null)
            {
                attendence.Outtime=Outtime;
                attendence.WorkTime = Worktime;
            }
            await _context.SaveChangesAsync();

            return Redirect("../Index");
        }
    }
}
