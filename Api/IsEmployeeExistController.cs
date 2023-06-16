using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsEmployeeExistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IsEmployeeExistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetIsEmployeeExist(Int64 AAdharno)
        {
            var emp = await _context.TblEmployees.SingleOrDefaultAsync(e => e.AAdharno == AAdharno);
            if (emp != null)
            {

                return Ok(emp);

            }
            return Ok();
        }
    }
}
