using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.Incharge
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPSiteInCharge> SitesIncharge { get;set; } = default!;
        public IList<SelectListItem> SitesInchargeList { get;set; } = default!;
        public List<SelectListItem> SiteList { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (_context.TblSiteIncharge != null)
                {
                    SiteList = await _context.TblSite.Select(s => new SelectListItem { Text = s.SiteName, Value = s.Siteid.ToString() }).ToListAsync();
                    SitesInchargeList = await _context.SPSiteInCharge.FromSqlInterpolated($"SELECT TSI.Id,TE.Id AS EmployeeId,TE.Name,TSI.SiteId,TS.SiteName,TSI.IsloginEnabled FROM TblEmployees TE \r\n\t\t\t  INNER JOIN TblSiteIncharge TSI ON TE.Id=TSI.EmployeeId\r\n\t\t\t  INNER JOIN TBLSITE TS ON TSI.SiteId=TS.Siteid").Select(s => new SelectListItem { Text = s.Name, Value = s.EmployeeId.ToString() }).ToListAsync();
                    SitesIncharge = await _context.SPSiteInCharge.FromSqlRaw("SPSITEINCHARGE").ToListAsync();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnGetSearch(int Empname,int site)
        {
            if(Empname!=0)
            {

            }
            return Page();
        }
    }
}
