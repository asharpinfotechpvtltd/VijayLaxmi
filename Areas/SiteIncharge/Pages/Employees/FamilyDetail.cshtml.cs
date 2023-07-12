using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class FamilyDetailModel : PageModel
    {
        ApplicationDbContext _context;

        public FamilyDetailModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public Int64 Employeeid { get; set; }
        public IActionResult OnGet(Int64 Aadhar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SiteIncharge")))
            {
                return Redirect("~/Index");
            }
            else
            {
                Employeeid = Aadhar;
                return Page();
            }
        }

        public async Task<IActionResult> OnPost(Int64 EmployeeId)
        {
            string Familydetail = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Familydetail);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                {
                    string relation = Convert.ToString(dt.Rows[i][0].ToString());
                    string name = Convert.ToString(dt.Rows[i][1]);
                    string dob = Convert.ToString(dt.Rows[i][2]);
                    Int64 aadharno = Convert.ToInt64(dt.Rows[i][3]);
                    string alternate = Convert.ToString(dt.Rows[i][4]);
                    FamilyDetail detail = new FamilyDetail()
                    {
                        EmployeeAAdhar = EmployeeId,
                        Relation = relation,
                        Name = name,
                        DateofBirth = dob,
                        AAdhar = aadharno,
                        Alternateno = alternate
                    };
                    await _context.TblFamilyDetail.AddAsync(detail);
                    await _context.SaveChangesAsync();
                }
            }
            return Redirect("AddBankDetail?Aadhar=" + EmployeeId);
        }
    }
}
