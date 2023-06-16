using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Accounts.CashHead
{
    public class DeleteModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DeleteModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CashHeads CashHeads { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblCashHead == null)
            {
                return NotFound();
            }

            var cashheads = await _context.TblCashHead.FirstOrDefaultAsync(m => m.Id == id);

            if (cashheads == null)
            {
                return NotFound();
            }
            else 
            {
                CashHeads = cashheads;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblCashHead == null)
            {
                return NotFound();
            }
            var cashheads = await _context.TblCashHead.FindAsync(id);

            if (cashheads != null)
            {
                CashHeads = cashheads;
                _context.TblCashHead.Remove(CashHeads);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
