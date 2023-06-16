using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.HR.Employees
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
        public IActionResult OnGet(Int64 Empid,Int64 aadharno)
        {
            EmployeeId = Empid;
            AadharNo = aadharno;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await _context.AddAsync(Salary);
            await _context.SaveChangesAsync();
            return Redirect("EmployeeDocuments?Empid=" + Salary.EmployeeId + "&aadharno=" + Salary.AadharNo);

        }
    }
}
