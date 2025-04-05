﻿using System;
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
    public class TrainingApiController : ControllerBase
    {
        private readonly SportDbContext _context;

        public TrainingApiController(SportDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Training>>> GetTrainings()
        {
            return await _context.Trainings.ToListAsync();
        }

        // GET: api/TrainingApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Training>> GetTraining(int id)
        {
            var training = await _context.Trainings.FindAsync(id);

            if (training == null)
            {
                return NotFound();
            }

            return training;
        }

        // PUT: api/TrainingApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraining(int id, Training training)
        {
            if (id != training.Id)
            {
                return BadRequest();
            }

            _context.Entry(training).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(id))
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

        // POST: api/TrainingApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Training>> PostTraining(Training training)
        {
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraining", new { id = training.Id }, training);
        }

        // DELETE: api/TrainingApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingExists(int id)
        {
            return _context.Trainings.Any(e => e.Id == id);
        }
    }
}
