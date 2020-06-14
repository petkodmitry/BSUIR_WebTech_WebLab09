using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitariesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MilitariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Militaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Military>>> GetMilitaries(int? group)
        {
            return await _context.Militaries.Where(d => !group.HasValue || d.MilitaryGroupId.Equals(group.Value))?.ToListAsync();
        }

        // GET: api/Militaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Military>> GetMilitary(int id)
        {
            var military = await _context.Militaries.FindAsync(id);

            if (military == null)
            {
                return NotFound();
            }

            return military;
        }

        // PUT: api/Militaries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilitary(int id, Military military)
        {
            if (id != military.MilitaryId)
            {
                return BadRequest();
            }

            _context.Entry(military).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilitaryExists(id))
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

        // POST: api/Militaries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Military>> PostMilitary(Military military)
        {
            _context.Militaries.Add(military);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMilitary", new { id = military.MilitaryId }, military);
        }

        // DELETE: api/Militaries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Military>> DeleteMilitary(int id)
        {
            var military = await _context.Militaries.FindAsync(id);
            if (military == null)
            {
                return NotFound();
            }

            _context.Militaries.Remove(military);
            await _context.SaveChangesAsync();

            return military;
        }

        private bool MilitaryExists(int id)
        {
            return _context.Militaries.Any(e => e.MilitaryId == id);
        }
    }
}
