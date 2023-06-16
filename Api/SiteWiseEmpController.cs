using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteWiseEmpController : ControllerBase
    {
        ApplicationDbContext _context;
        public SiteWiseEmpController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpSiteWiseEmployeeList>>> GetTblEmployees(int SiteName)
        {
            var SiteId = new SqlParameter("@SiteId", SiteName);
            var result = await _context.SpSiteWiseEmployeeList.FromSqlRaw("SpSiteWiseEmployeeList @SiteId", SiteId).ToListAsync();
            return result;
        }
    }
}
