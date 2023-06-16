using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.LoanAdvance
{
    public class IndexModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        public IndexModel(VijayLaxmi.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPLoanadvanceList> Loan_Advance { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TblLoanAdvance != null)
            {
                Loan_Advance = await _context.SPLoanadvanceList.FromSqlRaw("SPLoanadvanceList").ToListAsync();
            }
        }
    }
}
