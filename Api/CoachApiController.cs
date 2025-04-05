using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Sport_Club.Models;
using My_Sport_Club.Models.Domains;

namespace My_Sport_Club.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachApiController : ControllerBase
    {
        private readonly SportDbContext _context;

        public CoachApiController(SportDbContext context)
        {
            _context = context;
        }

        // GET: api/CoachApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoachs()
        {
            return await _context.Coachs.ToListAsync();
        }

        // GET: api/CoachApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coach>> GetCoach(int id)
        {
            var coach = await _context.Coachs.FindAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            return coach;
        }

        // PUT: api/CoachApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            if (id != coach.ID)
            {
                return BadRequest();
            }

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
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

        // POST: api/CoachApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coach>> PostCoach(Coach coach)
        {
            _context.Coachs.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoach", new { id = coach.ID }, coach);
        }

        // DELETE: api/CoachApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            var coach = await _context.Coachs.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            _context.Coachs.Remove(coach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachExists(int id)
        {
            return _context.Coachs.Any(e => e.ID == id);
        }
    }
}
