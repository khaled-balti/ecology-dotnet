using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecology_dotnet.Data;
using ecology_dotnet.Models;

namespace ecology_dotnet.Controllers
{
    [ApiController]
    [Route("api/complains")]
    public class ComplainsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComplainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/complains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complain>>> GetComplains()
        {
            return await _context.Complains
                .Include(c => c.user)
                .ToListAsync();
        }

        // GET: api/complains/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Complain>> GetComplain(int id)
        {
            var complain = await _context.Complains
                .Include(c => c.user)
                .FirstOrDefaultAsync(c => c.id == id);

            if (complain == null)
                return NotFound();

            return complain;
        }

        // POST: api/complains
        [HttpPost]
        public async Task<ActionResult<Complain>> PostComplain(Complain complain)
        {
            var userExists = await _context.Users.AnyAsync(u => u.id == complain.userId);
            if (!userExists)
                return BadRequest($"User with id {complain.userId} does not exist.");

            _context.Complains.Add(complain);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplain), new { id = complain.id }, complain);
        }

        // PUT: api/complains/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutComplain(int id, Complain complain)
        {
            if (id != complain.id)
                return BadRequest();

            _context.Entry(complain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Complains.Any(e => e.id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/complains/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComplain(int id)
        {
            var complain = await _context.Complains.FindAsync(id);
            if (complain == null)
                return NotFound();

            _context.Complains.Remove(complain);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
