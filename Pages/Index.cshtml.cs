using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VijayLaxmi.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using Monash.Models;

namespace NieAdvisory.Pages
{

    public class IndexModel : PageModel
    {
        ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public SitesIncharge Employee { get; set; }

        public void OnGet()
        {
            // Sms.SendSMS("9599929953", "20", "test");
        }
        public async Task<IActionResult> OnPost()
        {


            var emp = await _context.TblSiteIncharge.SingleOrDefaultAsync(e => e.EmployeeId == Employee.EmployeeId && e.Password == Employee.Password && e.IsloginEnabled == true);
            if (emp != null)
            {
                HttpContext.Session.SetString("SiteIncharge", emp.EmployeeId.ToString());
                HttpContext.Session.SetString("siteid", emp.SiteId.ToString());
                return RedirectToPage("/Index", new { area = "SiteIncharge" });
            }
            return Page();

        }
    }
}