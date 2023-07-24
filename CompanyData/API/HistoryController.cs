using AutoMapper;
using CompanyData.Data;
using CompanyData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HistoryController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetAll()
        {
            if (_context.Histories == null)
            {
                return NotFound();
            }
            return await _context.Histories.Include(h => h.Company).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> Get(int id)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }
            var history = await _context.Histories.Include(h => h.Company).SingleOrDefaultAsync(_ => _.Id == id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, History history)
        {
            if (id != history.Id)
            {
                return BadRequest();
            }

            _context.Entry(history).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<History>> Post(History history)
        {
            if (_context.Histories == null)
            {
                return Problem("Entity set 'DataContext.Histories'  is null.");
            }
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = history.Id }, history);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Histories == null)
            {
                return NotFound();
            }
            var history = await _context.Histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
