using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpsalaryController : ControllerBase
    {
        ApplicationDbContext _context;
        public EmpsalaryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<double> GetEmpsalary(Int64 id)
        {
            var emp = await _context.TblSalary.SingleOrDefaultAsync(e => e.AadharNo == (id));
            return emp.INHAND;
        }
    }
}
