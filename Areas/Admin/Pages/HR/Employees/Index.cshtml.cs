using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.HR.Employees
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

        public PaginatedList<SPEmployeeList> SPEmployeeList { get; set; } = default!;
        public List<SelectListItem> SiteList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {

            IQueryable<SPEmployeeList> EmployeeList = _context.SPEmployeeList.FromSqlInterpolated<SPEmployeeList>($"SELECT [Id]\r\n\t\t\t\t  ,TE.[Name]\r\n\t\t\t\t  ,TE.[FathersName]\r\n\t\t\t\t  ,TE.[AAdharno]\r\n\t\t\t\t  ,TE.[Contactno]\r\n\t\t\t\t  ,TE.[Email]\r\n\t\t\t\t  ,TE.[Employeetype]\r\n\t\t\t\t  ,TS.[SiteName]\r\n\t\t\t\t  ,TE.[AddedDate]     \r\n\t\t\t\t  ,te.IsVerified\r\n\t\t\t  FROM TblEmployees TE\r\n\t\t\t  inner join TblSite TS ON TS.Siteid=TE.Site").AsQueryable();
            var pageSize = 50;
            SPEmployeeList = await PaginatedList<SPEmployeeList>.CreateAsync(
                EmployeeList.AsNoTracking(), pageIndex ?? 1, pageSize);

            SiteList = await _context.TblSite.Select(d => new SelectListItem { Text = d.SiteName, Value = d.Siteid.ToString() }).ToListAsync();
            return Page();
        }

    }
}
