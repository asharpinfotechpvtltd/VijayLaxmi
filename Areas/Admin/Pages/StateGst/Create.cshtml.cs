using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.StateGst
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public CreateModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> SiteList { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StateDetail StateDetail { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            StateDetail.StateName.ToUpper();
            await _context.TblStateDetail.AddAsync(StateDetail);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
