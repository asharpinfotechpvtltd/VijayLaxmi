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
    public class searchemployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public searchemployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpSearchEmployeeList>>> GetTblEmployees(string EmployeeName)
        {
            var EmployeeNameNames = new SqlParameter("@EmployeeName", EmployeeName);
            var result = await _context.SpSearchEmployeeList.FromSqlRaw("SpSearchEmployeeList @EmployeeName", EmployeeNameNames).ToListAsync();
            return result;
        }

    }
}
