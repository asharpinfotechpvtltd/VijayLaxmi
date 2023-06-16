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
    public class DetailsModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public DetailsModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
