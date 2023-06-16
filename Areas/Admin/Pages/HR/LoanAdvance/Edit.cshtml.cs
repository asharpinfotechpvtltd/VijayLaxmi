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
    public class EditModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;
        public IFormFile SignedDocument { get; set; }
        public IFormFile ApplicationDocument { get; set; }
        public string SignedDocumentName { get; set; }
        public string ApplicationDocumentName { get; set; }
        IWebHostEnvironment Environment;
        public EditModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environment = Env;
        }

        [BindProperty]
        public Loan_Advance Loan_Advance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblLoanAdvance == null)
            {
                return NotFound();
            }

            var loan_advance = await _context.TblLoanAdvance.FirstOrDefaultAsync(m => m.Id == id);
            if (loan_advance == null)
            {
                return NotFound();
            }
            Loan_Advance = loan_advance;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile SignedDocument, IFormFile ApplicationDocument,string SignedDocumentfilename, string ApplicationDocumentfilename)
        {
           
            Upload u = new Upload(Environment);
            if (SignedDocument != null)
            {
                SignedDocumentName = u.UploadImage(SignedDocument, "LoanadvanceDocument");
                Loan_Advance.SignedDocument = SignedDocumentName;
            }
            else
            {
                Loan_Advance.SignedDocument = SignedDocumentfilename;
            }
            if (ApplicationDocument != null)
            {
                ApplicationDocumentName = u.UploadImage(ApplicationDocument, "LoanadvanceDocument");
                Loan_Advance.ApplicationDocument = ApplicationDocumentName;
            }
            else
            {
                Loan_Advance.ApplicationDocument = ApplicationDocumentfilename;
            }
            _context.Attach(Loan_Advance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Loan_AdvanceExists(Loan_Advance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Loan_AdvanceExists(int id)
        {
            return _context.TblLoanAdvance.Any(e => e.Id == id);
        }
    }
}
