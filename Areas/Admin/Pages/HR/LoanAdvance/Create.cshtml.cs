using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.HR.LoanAdvance
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        IWebHostEnvironment Environment;
        public CreateModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environment = Env;
        }
        public IFormFile SignedDocument { get; set; }
        public IFormFile ApplicationDocument { get; set; }
        public string SignedDocumentName { get; set; }
        public string ApplicationDocumentName { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            EmployeeList = await _context.TblEmployees.Select(a => new SelectListItem
            {
                Value = a.AAdharno.ToString(),
                Text = a.Name + "-" + a.AAdharno
            }
            ).ToListAsync();
            return Page();
        }

        [BindProperty]
        public Loan_Advance Loan_Advance { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {            
          
            Upload u = new Upload(Environment);
            SignedDocumentName = u.UploadImage(SignedDocument, "LoanadvanceDocument");
            ApplicationDocumentName = u.UploadImage(ApplicationDocument, "LoanadvanceDocument");
            Loan_Advance.SignedDocument = SignedDocumentName;
            Loan_Advance.ApplicationDocument = ApplicationDocumentName;
            Loan_Advance.Status = true;
            _context.TblLoanAdvance.Add(Loan_Advance);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
