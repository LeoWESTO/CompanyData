using AutoMapper;
using CompanyData.Data;
using CompanyData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NoteController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAll()
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            return await _context.Notes.Include(n => n.Employee).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            var note = await _context.Notes.Include(n => n.Employee).SingleOrDefaultAsync(_ => _.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note note)
        {
            if (_context.Notes == null)
            {
                return Problem("Entity set 'DataContext.Notes'  is null.");
            }
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
