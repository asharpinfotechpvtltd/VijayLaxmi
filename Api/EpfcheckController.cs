using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpfcheckController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EpfcheckController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<string> GetEpfcheck(string Epf)
        {
            var Epfno = await _context.TblEmployees.SingleOrDefaultAsync(e => e.EPF == Epf);
            if (Epfno != null)
            {
                if (Epfno.EPF != null)
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

    
