using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.HR.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;
        IWebHostEnvironment Environmet;

        public DetailsModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public List<FamilyDetail> FamilyDetails { get; set; }
        [BindProperty]
        public BankDetail BankDetails { get; set; }
        public List<SelectListItem> SiteName { get; set; }
        public List<SelectListItem> Designation { get; set; }
        public Salary Salary { get; set; }
        public string desg { get; set; }
        [BindProperty]
        public Document Document { get; set; }

        [BindProperty]
        public List<Loan_Advance> loan_Advances { get; set; }

        public string DocumentFrontFile { get; set; } = null;
        public string DocumentBackFile { get; set; } = null;
        public string PancardName { get; set; } = null;
        public string TicFile { get; set; } = null;
        public string SignedDocument { get; set; } = null;
        public string? PassbookFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Int64 Aadharno)
        {



            Employee = await _context.TblEmployees.FirstOrDefaultAsync(e => e.AAdharno == Aadharno);
            Salary = await _context.TblSalary.FirstOrDefaultAsync(e => e.AadharNo == Aadharno);
            FamilyDetails = await _context.TblFamilyDetail.Where(e => e.EmployeeAAdhar == Convert.ToInt64(Employee.AAdharno)).ToListAsync();
            BankDetails = await _context.TblBankDetail.SingleOrDefaultAsync(e => e.AAdharNo == Employee.AAdharno);
            Document = await _context.TblDocuments.SingleOrDefaultAsync(e => e.AadharNo == Employee.AAdharno);
            loan_Advances = await _context.TblLoanAdvance.Where(e => e.AAdharNo == Convert.ToInt64(Employee.AAdharno) && e.Status == true).ToListAsync();
            if (BankDetails != null)
            {
                PassbookFile = BankDetails.Filename;
            }
            if (Document != null)
            {
                DocumentFrontFile = Document.DocumentFrontFileName;
            }
            if(Document!=null)
            {
                DocumentBackFile = Document.DocumentBackFileName;
            }
            if(Document!=null)
            {
                PancardName = Document.PancardFileName;
            }
            if(Document!=null)
            {
                TicFile=Document.TicFileName;
            } 
            if(Document!=null)
            {
                SignedDocument = Document.SignedDocumentName;
            }
            SiteName = await _context.TblSite.Select(s => new SelectListItem
            {
                Text = s.SiteName,
                Value = s.Siteid.ToString()
            }).ToListAsync();

            Designation = await _context.TblDesignation.Select(s => new SelectListItem
            {
                Text = s.DesignationName,
                Value = s.Id.ToString()
            }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostSalary(double CTC,double INHAND, double PERHOURRATE,double WORKINGDAYS,double WORKINGHOURS,Int64 EmployeeId,Int64 AadharNo)
        {
            var issalaryexist = await _context.TblSalary.SingleOrDefaultAsync(e => e.EmployeeId == EmployeeId);
            if(issalaryexist!=null)
            {
                issalaryexist.WORKINGDAYS = WORKINGDAYS;
                issalaryexist.CTC = CTC;
                issalaryexist.WORKINGHOURS = WORKINGHOURS;
                issalaryexist.INHAND = INHAND;
                issalaryexist.PERHOURRATE = PERHOURRATE;                    
            }
            else
            {
                Salary s = new Salary()
                {
                    WORKINGDAYS = WORKINGDAYS,
                    CTC = CTC,
                    INHAND = INHAND,
                    PERHOURRATE = PERHOURRATE,
                    WORKINGHOURS = WORKINGHOURS,
                    EmployeeId = EmployeeId,
                    AadharNo = AadharNo
                };
                await _context.TblSalary.AddAsync(s);                
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("Details", new { Aadharno = AadharNo });


        }
        public async Task<IActionResult> OnPostBankUpdate(Int64 empid, string Bankname, string Accountholdername, string AccountNumber, string IFSCCODE, string BranchName, IFormFile bankdetailfilename)
        {
             Upload u = new Upload(Environmet);
            string bankfilename;
            var bankdetail = await _context.TblBankDetail.SingleOrDefaultAsync(e => e.AAdharNo == empid);
            if (bankdetail != null)
            {
                bankdetail.BranchName = BranchName;
                bankdetail.AccountNumber = AccountNumber;
                bankdetail.IFSCCODE = IFSCCODE;
                bankdetail.Bankname = Bankname;
                bankdetail.Accountholdername = Accountholdername;
                if (bankdetailfilename != null)
                {
                    bankfilename = u.UploadImage(bankdetailfilename, "BankDocument");
                    bankdetail.Filename = bankfilename;
                }
                else
                {
                    bankdetail.Filename = BankDetails.Filename;
                }


            }
            else
            {
                bankfilename = u.UploadImage(bankdetailfilename, "BankDocument");
                BankDetails.AAdharNo = (empid);
                BankDetails.BranchName = BranchName;
                BankDetails.AccountNumber = AccountNumber;
                BankDetails.IFSCCODE = IFSCCODE;
                BankDetails.Bankname = Bankname;
                BankDetails.Accountholdername = Accountholdername;
                BankDetails.Filename = bankfilename;
                await _context.TblBankDetail.AddAsync(BankDetails);

            }
            await _context.SaveChangesAsync();
            PassbookFile = BankDetails.Filename;
            Employee = await _context.TblEmployees.FirstOrDefaultAsync(e => e.AAdharno == empid);
            FamilyDetails = await _context.TblFamilyDetail.Where(e => e.EmployeeAAdhar == Convert.ToInt64(Employee.AAdharno)).ToListAsync();
            BankDetails = await _context.TblBankDetail.SingleOrDefaultAsync(e => e.AAdharNo == Employee.AAdharno);
            Document = await _context.TblDocuments.SingleOrDefaultAsync(e => e.AadharNo == Employee.AAdharno);
            SiteName = await _context.TblSite.Select(s => new SelectListItem
            {
                Text = s.SiteName,
                Value = s.Siteid.ToString()
            }).ToListAsync();

            Designation = await _context.TblDesignation.Select(s => new SelectListItem
            {
                Text = s.DesignationName,
                Value = s.Id.ToString()
            }).ToListAsync();
            var designation = Designation.SingleOrDefault(e => e.Value == Employee.Designation.ToString());
            desg = designation.Text;
            return Page();
        }
        [BindProperty]
        public IFormFile DocumentFrontFileName { get; set; }
        [BindProperty]
        public IFormFile DocumentBackFileName { get; set; }
        [BindProperty]
        public IFormFile PancardFileName { get; set; }
        [BindProperty]
        public IFormFile TicFileName { get; set; } = null;
        [BindProperty]
        public IFormFile Signeddocument { get; set; } = null;
        public string FrontFileName { get; set; }
        public string BackFileName { get; set; }
        public string PanFileName { get; set; }
        public string TiFileName { get; set; }
        public string SigneddocumentName { get; set; }

        public async Task<IActionResult> OnPostDocumentUpdate(Int64 AAdharno)
        {
            var document = await _context.TblDocuments.SingleOrDefaultAsync(e => e.AadharNo == AAdharno);
            Upload u = new Upload(Environmet);
            if (document != null)
            {
               
                if (DocumentFrontFileName != null)
                {
                    FrontFileName = u.UploadImage(DocumentFrontFileName, "UserDocument");
                    document.DocumentFrontFileName = FrontFileName;
                }
                else
                {
                    document.DocumentFrontFileName = Document.DocumentFrontFileName;
                }
                if (DocumentBackFileName != null)
                {
                    BackFileName = u.UploadImage(DocumentBackFileName, "UserDocument");
                    document.DocumentBackFileName = BackFileName;
                }
                else
                {
                    document.DocumentBackFileName = Document.DocumentBackFileName;
                }
                if (PancardFileName != null)
                {
                    PanFileName = u.UploadImage(PancardFileName, "Userpan");
                    document.PancardFileName = PanFileName;
                }
                else
                {
                    document.PancardFileName = Document.PancardFileName;
                }
                if (Signeddocument != null)
                {
                    SigneddocumentName = u.UploadImage(Signeddocument, "UserDocument");
                    document.SignedDocumentName = SigneddocumentName;
                }
                else
                {
                    document.SignedDocumentName = Document.SignedDocumentName;
                }
                if (TicFileName != null)
                {
                    TiFileName = u.UploadImage(TicFileName, "Tic");
                    document.TicFileName = TiFileName;
                }
                else
                {
                    document.TicFileName = Document.TicFileName;
                }              
                

            }
            else
            {
                if (DocumentFrontFileName != null)
                {
                    FrontFileName = u.UploadImage(DocumentFrontFileName, "UserDocument");
                   
                }
                else
                {
                    FrontFileName = Document.DocumentFrontFileName;
                }
                if (DocumentBackFileName != null)
                {
                    BackFileName = u.UploadImage(DocumentBackFileName, "UserDocument");
                   
                }
                else
                {
                    BackFileName = "NA";
                }
                if (PancardFileName != null)
                {
                    PanFileName = u.UploadImage(PancardFileName, "Userpan");
                   
                }
                else
                {
                    PanFileName = "NA";
                }
                if (Signeddocument != null)
                {
                    SigneddocumentName = u.UploadImage(Signeddocument, "UserDocument");
                   
                }
                else
                {
                    SigneddocumentName = "NA";
                }
                if (TicFileName != null)
                {
                    TiFileName = u.UploadImage(TicFileName, "Tic");
                   
                }
                else
                {
                    TiFileName = "NA";
                }
                Document documents = new Document()
                {
                    AadharNo = AAdharno,
                    DocumentBackFileName = BackFileName,
                    DocumentFrontFileName = FrontFileName,
                    PancardFileName = PanFileName,
                    TicFileName = TiFileName,
                    SignedDocumentName = SigneddocumentName
                };
                await _context.TblDocuments.AddAsync(documents);

            }
            await _context.SaveChangesAsync();
            Document = await _context.TblDocuments.SingleOrDefaultAsync(e => e.AadharNo == AAdharno);
            Employee = await _context.TblEmployees.FirstOrDefaultAsync(e => e.AAdharno == AAdharno);
            FamilyDetails = await _context.TblFamilyDetail.Where(e => e.EmployeeAAdhar == Convert.ToInt64(Employee.AAdharno)).ToListAsync();
            BankDetails = await _context.TblBankDetail.SingleOrDefaultAsync(e => e.AAdharNo == Employee.AAdharno);
            SiteName = await _context.TblSite.Select(s => new SelectListItem
            {
                Text = s.SiteName,
                Value = s.Siteid.ToString()
            }).ToListAsync();

            Designation = await _context.TblDesignation.Select(s => new SelectListItem
            {
                Text = s.DesignationName,
                Value = s.Id.ToString()
            }).ToListAsync();
            var designation = Designation.SingleOrDefault(e => e.Value == Employee.Designation.ToString());
            desg = designation.Text;
            
            return RedirectToPage("Details", new { Aadharno = AAdharno });

        }
        public string UserimgName { get; set; }
        public async Task<IActionResult> OnPostEmpUpdate(Int64 employeeeid, IFormFile userimg, string savedimgname)
        {
            Upload u = new Upload(Environmet);
            if (userimg != null)
            {
                UserimgName = u.UploadImage(userimg, "UserImg");
                Employee.Image = UserimgName.ToString().Replace(" ", "_");
            }
            else
            {
                Employee.Image = savedimgname;
            }
            var empdetail = await _context.TblEmployees.FirstOrDefaultAsync(e => e.Id == employeeeid);
            Employee e = new Employee();
            if (empdetail != null)
            {
                empdetail.Name = Employee.Name;
                empdetail.IsVerified = true;
                empdetail.Salary = Employee.Salary;
                empdetail.Pancard = Employee.Pancard;
                empdetail.Contactno = Employee.Contactno;
                empdetail.State = Employee.State;
                empdetail.Addresslocal = Employee.Addresslocal;
                empdetail.AddressPermanent = Employee.AddressPermanent;
                empdetail.AAdharno = Employee.AAdharno;
                empdetail.City = Employee.City;
                empdetail.Email = Employee.Email;
                empdetail.FathersName = Employee.FathersName;
                empdetail.IsVerified = Employee.IsVerified;
                empdetail.Reason = Employee.Reason;
                empdetail.Image = Employee.Image;
                empdetail.UANNo = Employee.UANNo;
                empdetail.EPF = Employee.EPF;
                empdetail.EsiNo = Employee.EsiNo;
                empdetail.Site = Employee.Site;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Details", new { Aadharno = Employee.AAdharno });

        }
        public async Task<IActionResult> OnPostFamilyUpdate(Int64 familyempid)
        {
            string Familydetail = Request.Form["jobworkdesc"];
            var Familydetai = await _context.TblFamilyDetail.Where(e => e.EmployeeAAdhar == familyempid).ToListAsync();
            if (Familydetai.Any())
            {
                _context.TblFamilyDetail.RemoveRange(Familydetai);
            }
            await _context.SaveChangesAsync();
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Familydetail);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    string relation = Convert.ToString(dt.Rows[i][0].ToString());
                    string name = Convert.ToString(dt.Rows[i][1]);
                    string dob = Convert.ToString(dt.Rows[i][2]);
                    Int64 aadharno = Convert.ToInt64(dt.Rows[i][3]);
                    FamilyDetail detail = new FamilyDetail()
                    {
                        EmployeeAAdhar = familyempid,
                        Relation = relation,
                        Name = name,
                        DateofBirth = dob,
                        AAdhar = aadharno
                    };
                    await _context.TblFamilyDetail.AddAsync(detail);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("Index");
        }
    }
}
