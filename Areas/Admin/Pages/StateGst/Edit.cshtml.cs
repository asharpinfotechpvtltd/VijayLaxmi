using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.StateGst
{
    public class EditModel : PageModel
    {
        ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;

        }
        [BindProperty]
        public StateDetail StateDetail { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            StateDetail=await _context.TblStateDetail.FindAsync(id);
            return Page();

        }
    }
}
