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
    public class MemberApiController : ControllerBase
    {
        private readonly SportDbContext _context;

        public MemberApiController(SportDbContext context)
        {
            _context = context;
        }

        // GET: api/MemberApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberShip>>> GetMemberShips()
        {
            return await _context.MemberShips.ToListAsync();
        }

        // GET: api/MemberApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberShip>> GetMemberShip(int id)
        {
            var memberShip = await _context.MemberShips.FindAsync(id);

            if (memberShip == null)
            {
                return NotFound();
            }

            return memberShip;
        }

        // PUT: api/MemberApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberShip(int id, MemberShip memberShip)
        {
            if (id != memberShip.Id)
            {
                return BadRequest();
            }

            _context.Entry(memberShip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberShipExists(id))
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

        // POST: api/MemberApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemberShip>> PostMemberShip(MemberShip memberShip)
        {
            _context.MemberShips.Add(memberShip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberShip", new { id = memberShip.Id }, memberShip);
        }

        // DELETE: api/MemberApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberShip(int id)
        {
            var memberShip = await _context.MemberShips.FindAsync(id);
            if (memberShip == null)
            {
                return NotFound();
            }

            _context.MemberShips.Remove(memberShip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberShipExists(int id)
        {
            return _context.MemberShips.Any(e => e.Id == id);
        }
    }
}
