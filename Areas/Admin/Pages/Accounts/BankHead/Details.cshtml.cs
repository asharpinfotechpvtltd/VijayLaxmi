using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Accounts.BankHead
{
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

      public BankHeads BankHeads { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblBankHead == null)
            {
                return NotFound();
            }

            var bankheads = await _context.TblBankHead.FirstOrDefaultAsync(m => m.Id == id);
            if (bankheads == null)
            {
                return NotFound();
            }
            else 
            {
                BankHeads = bankheads;
            }
            return Page();
        }
    }
}
