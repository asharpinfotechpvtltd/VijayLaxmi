using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Sites
{
    public class UploadContractModel : PageModel
    {
        ApplicationDbContext _context;
        IWebHostEnvironment Environmet;
        public UploadContractModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }
        [BindProperty]
        public SiteWiseContract Contract { get; set; }
        public List<SiteWiseContract> SiteWiseContractList { get; set; }
        public string ContractNames { get; set; }
        public int Site { get; set; }
        public async Task<IActionResult> OnGet(int Siteid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                Site = Siteid;
                SiteWiseContractList = await _context.TblSiteWiseContract.Where(id => id.SiteId == Siteid).OrderByDescending(id => id).ToListAsync();
                return Page();
            }
        }
        public async Task<IActionResult> OnPost(int Siteid, IFormFile ContractName)
        {
            Upload u = new Upload(Environmet);
            GetUserDate date = new GetUserDate();
            ContractNames = u.UploadImage(ContractName, "Agreement");
            SiteWiseContract contract = new SiteWiseContract
            {
                SiteId = Siteid,
                ContractName = ContractNames,
                EndDate = Contract.EndDate,
                StartDate = Contract.StartDate,
                UploadedDate = date.ReturnDate()
            };
            await _context.TblSiteWiseContract.AddAsync(contract);
            await _context.SaveChangesAsync();
            SiteWiseContractList = await _context.TblSiteWiseContract.Where(id => id.SiteId == Siteid).OrderByDescending(id => id).ToListAsync();
            return Page();
        }
    }
}
