using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.DesignationList
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;


        public CreateModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public Designation Designation { get; set; }    
        public async Task<IActionResult> OnPostAsync()
        {
            await _context.TblDesignation.AddAsync(Designation);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}