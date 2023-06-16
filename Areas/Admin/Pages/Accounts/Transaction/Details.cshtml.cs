using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Accounts.Transaction
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public Ledger Ledger { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblLedger == null)
            {
                return NotFound();
            }

            var ledger = await _context.TblLedger.FirstOrDefaultAsync(m => m.Id == id);
            if (ledger == null)
            {
                return NotFound();
            }
            else 
            {
                Ledger = ledger;
            }
            return Page();
        }
    }
}
