using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.Admin.Pages.Accounts.Transaction
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SpBankTransaction> SpBankTransaction { get;set; } = default!;

        public async Task OnGetAsync()
        {

            SpBankTransaction = await _context.SpBankTransaction.FromSqlRaw("SpBankTransaction").ToListAsync();
            
        }
    }
}
