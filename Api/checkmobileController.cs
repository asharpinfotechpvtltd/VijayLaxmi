using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class checkmobileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public checkmobileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<string> Getcheckmobile(string contactno)
        {
            var esi = await _context.TblEmployees.SingleOrDefaultAsync(e => e.Contactno == contactno);
            if (esi != null)
            {

                return "AR";

            }
            else
            {
                return "ok";
            }
        }
    }
}

