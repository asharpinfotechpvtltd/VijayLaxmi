using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VijayLaxmi.Models;

namespace Monash.Areas.Admin.Pages.Bill
{
    public class ProductPrintBillModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductPrintBillModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public TblOrderid TblOrderId { get; set; }
        public List<BillDescription> Bills { get; set; }
        public Site SiteName { get; set; }
       

        public async Task<IActionResult> OnGetAsync(string Orderid)
        {
            
            TblOrderId = await _context.TblOrderid.SingleOrDefaultAsync(id => id.Orderid == Orderid);            
            Bills = await _context.TblBillDescription.Where(e=>e.Orderid==Orderid).ToListAsync();
            SiteName = await _context.TblSite.SingleOrDefaultAsync(e => e.Siteid == TblOrderId.Siteid);

            return Page();

        }



    }
}