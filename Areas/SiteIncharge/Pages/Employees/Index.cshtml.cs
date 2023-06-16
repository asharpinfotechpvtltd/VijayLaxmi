using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<SpSiteWiseEmployeeList> SPEmployeeList { get; set; } = default!;
        public List<SelectListItem> SiteList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            var SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));

            IQueryable<SpSiteWiseEmployeeList> EmployeeList = _context.SpSiteWiseEmployeeList.FromSqlInterpolated($"SELECT [Id]  ,TE.[Name]  ,TE.[FathersName]  ,TE.[AAdharno]  ,TE.[Contactno]  ,TE.[Email]  ,TE.[Employeetype]  ,TS.[SiteName]  ,TE.[AddedDate],te.IsVerified  FROM TblEmployees TE  inner join TblSite TS ON TS.Siteid=TE.Site where TE.Site=({SiteId})").AsQueryable();
            var pageSize = 20;
            SPEmployeeList = await PaginatedList<SpSiteWiseEmployeeList>.CreateAsync(
                EmployeeList.AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }

    }
}
