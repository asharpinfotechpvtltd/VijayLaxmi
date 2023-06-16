using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class sitedetailController : ControllerBase
    {
        ApplicationDbContext _context;
        public sitedetailController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Getsitedetail(int id)
        {
            var sitedetail = await _context.TblSite.SingleOrDefaultAsync(e => e.Siteid == id);
            return Ok(sitedetail);
        }
    }
}
