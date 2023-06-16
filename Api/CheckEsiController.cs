using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckEsiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckEsiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<string> GetCheckEsi(long EsiNo)
        {
            var esi= await _context.TblEmployees.SingleOrDefaultAsync(e => e.EsiNo == EsiNo);
            if (esi != null)
            {
                if (esi.EsiNo != 0)
                {
                    return "AR";
                }
                else
                {
                    return "ok";
                }
            }
            else
            {
                return "ok";
            }
        }
    }
}
