using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class EmployeeDocumentsModel : PageModel
    {
        ApplicationDbContext _context;
        IWebHostEnvironment Environment;
        public EmployeeDocumentsModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environment = Env;
        }
        [BindProperty]
        public Document document { get; set; }
        [BindProperty]
        public IFormFile DocumentFrontFileName { get; set; }
        [BindProperty]
        public IFormFile DocumentBackFileName { get; set; }
        [BindProperty]
        public IFormFile PancardFileName { get; set; } = null;
        [BindProperty]
        public IFormFile TicFileName { get; set; } = null;
        [BindProperty]
        public IFormFile Signeddocument { get; set; } = null;
        public string FrontFileName { get; set; }
        public string BackFileName { get; set; }
        public string PanFileName { get; set; }
        public string TiFileName { get; set; }
        public string SigneddocumentName { get; set; }

        public Int64 Aadharno { get; set; }

        public IActionResult OnGet(Int64 aadharno)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                Aadharno = aadharno;
                return Page();
            }
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                Upload u = new Upload(Environment);
                FrontFileName = u.UploadImage(DocumentFrontFileName, "UserDocument");
                BackFileName = u.UploadImage(DocumentBackFileName, "UserDocument");
                SigneddocumentName = u.UploadImage(Signeddocument, "UserDocument");
                if (TiFileName != null)
                {
                    TiFileName = u.UploadImage(TicFileName, "Tic");
                }
                if (PancardFileName != null)
                {
                    PanFileName = u.UploadImage(PancardFileName, "Userpan");
                }
                Document documents = new Document()
                {
                    AadharNo = document.AadharNo,
                    DocumentBackFileName = BackFileName,
                    DocumentFrontFileName = FrontFileName,
                    PancardFileName = PanFileName,
                    TicFileName = TiFileName,
                    SignedDocumentName = SigneddocumentName
                };
                await _context.TblDocuments.AddAsync(documents);
                await _context.SaveChangesAsync();
                return Redirect("FamilyDetail?Aadhar=" + document.AadharNo);
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
