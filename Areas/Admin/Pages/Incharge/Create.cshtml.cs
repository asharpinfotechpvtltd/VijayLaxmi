using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Incharge
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public CreateModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> SiteList { get; set; }


        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                EmployeeList = await _context.TblEmployees.Select(e => new SelectListItem { Text = e.Name + "-" + e.AAdharno, Value = e.Id.ToString() }).ToListAsync();
                SiteList = await _context.TblSite.Select(e => new SelectListItem { Text = e.SiteName, Value = e.Siteid.ToString() }).ToListAsync();
                return Page();
            }
        }

        [BindProperty]
        public SitesIncharge SitesIncharge { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            GetUserDate date = new GetUserDate();
            SitesIncharge.Date = date.ReturnDate();
            _context.TblSiteIncharge.Add(SitesIncharge);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
