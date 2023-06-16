using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Pages
{
    public class adminloginModel : PageModel
    {
        ApplicationDbContext _context;
        public adminloginModel(ApplicationDbContext context)
        {
            _context = context;

        }

        [BindProperty]
        public Admin Admin { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
             Admin = await _context.TblAdmin.FirstOrDefaultAsync(uname => uname.Username == Admin.Username && uname.Password == Admin.Password);
            if (Admin != null)
            {              
                    HttpContext.Session.SetString("AdminLogin", Admin.Username.ToString());
                    return RedirectToPage("/Index", new { area = "Admin" });
               
            }
            else
            {
                return Redirect("Adminlogin");
            }

        }
    }
}
