using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.HR.Employees
{
    public class AddBankDetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public BankDetail BankDetail { get; set; }
        [BindProperty]
        public IFormFile Filename { get; set; }
        public string BankDocumentFilename { get; set; }
        IWebHostEnvironment Environmet;
        public AddBankDetailModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }
        public string EmpCode { get;set; }
        public Int64 EmployeeCode { get;set; }
        public IActionResult OnGet(Int64 Aadhar)
        {
           

            EmpCode = HttpContext.Session.GetString("Login");
            EmployeeCode = Aadhar;
            return Page();
            //if (string.IsNullOrEmpty(EmpCode))
            //{
            //    return Redirect("~/Index");
            //}
            //else
            //{

            //    
            //}
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Upload u = new Upload(Environmet);
            BankDocumentFilename = u.UploadImage(Filename, "BankDocument");          
            BankDetail.Filename = BankDocumentFilename;
            await  _context.TblBankDetail.AddAsync(BankDetail);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}