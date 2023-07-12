using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.DeviceList
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                if (id == null || _context.TblDevice == null)
                {
                    return NotFound();
                }

                var device = await _context.TblDevice.FirstOrDefaultAsync(m => m.Id == id);

                if (device == null)
                {
                    return NotFound();
                }
                else
                {
                    Device = device;
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblDevice == null)
            {
                return NotFound();
            }
            var device = await _context.TblDevice.FindAsync(id);

            if (device != null)
            {
                Device = device;
                _context.TblDevice.Remove(Device);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
