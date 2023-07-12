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

namespace VijayLaxmi.Areas.SiteIncharge.Pages.LoanAdvance
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
        public string SignedDocumentName { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                int site = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
                EmployeeList = await _context.TblEmployees.Where(e => e.Site == site).Select(a => new SelectListItem
                {
                    Value = a.AAdharno.ToString(),
                    Text = a.Name + "-" + a.AAdharno
                }
                ).ToListAsync();
                return Page();
            }
        }

        [BindProperty]
        public Loan_Advance Loan_Advance { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Upload u = new Upload(Environment);
            SignedDocumentName = u.UploadImage(SignedDocument, "LoanadvanceDocument");
            Loan_Advance.SignedDocument = SignedDocumentName;
            _context.TblLoanAdvance.Add(Loan_Advance);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
