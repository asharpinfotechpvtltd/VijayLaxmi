using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class SalaryModel : PageModel
    {
        ApplicationDbContext _context;
        public SalaryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Salary Salary { get; set; }
        public Int64 EmployeeId { get; set; }
        public Int64 AadharNo { get; set; }
        public IActionResult OnGet(Int64 Empid, Int64 aadharno)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                EmployeeId = Empid;
                AadharNo = aadharno;
                return Page();
            }
        }
        public async Task<IActionResult> OnPost()
        {
            int Siteid = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            Salary.Siteid = Siteid;
            await _context.TblSalary.AddAsync(Salary);
            await _context.SaveChangesAsync();
            return Redirect("EmployeeDocuments?Empid=" + Salary.EmployeeId + "&aadharno=" + Salary.AadharNo);

        }
    }
}
