using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.DeviceList
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public CreateModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> SiteList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                SiteList = await _context.TblSite.Select(s => new SelectListItem { Text = s.SiteName, Value = s.Siteid.ToString() }).ToListAsync();
                return Page();
            }
        }

        [BindProperty]
        public Device Device { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblDevice.Add(Device);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
