using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Models;

namespace VijayLaxmi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManualSalariesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManualSalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

     

       

        // PUT: api/ManualSalaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManualSalaries(int id, ManualSalaries manualSalaries)
        {
            if (id != manualSalaries.id)
            {
                return BadRequest();
            }

            _context.Entry(manualSalaries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManualSalariesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/ManualSalaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet]
        public async Task<ActionResult<ManualSalaries>> PostManualSalaries(Int64 Empcode,string AAdharno,double TotalWorkingHours,double TotalAttendance,double MonthlySalary,double ManualSalary,int Month,int Year,int Siteid)
        {
            ManualSalaries manualSalaries = new ManualSalaries
            {
                AAdharno = AAdharno,
                TotalWorkingHours = TotalWorkingHours,
                TotalAttendance = TotalAttendance,
                Empcode = Empcode,
                ManualSalary = ManualSalary,
                MonthlySalary = MonthlySalary,
                Month = Month,
                Year = Year,
                Siteid = Siteid
            };
            
            await _context.TblManualSalary.AddAsync(manualSalaries);
            await _context.SaveChangesAsync();
         

            return CreatedAtAction("GetManualSalaries", new { id = manualSalaries.id }, manualSalaries);
        }

      

        private bool ManualSalariesExists(int id)
        {
            return _context.TblManualSalary.Any(e => e.id == id);
        }
    }
}
