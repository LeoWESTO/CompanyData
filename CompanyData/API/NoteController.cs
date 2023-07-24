using AutoMapper;
using CompanyData.Data;
using CompanyData.Models;
using CompanyData.ViewModels;
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
            return await _context.Notes
                .Include(_ => _.Employee)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            var noteById = await _context.Notes
                .Include(_ => _.Employee)
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();

            if (noteById == null) return NotFound();

            return Ok(noteById);
        }

        [HttpPut]
        public async Task<IActionResult> Put(NoteViewModel noteViewModel)
        {
            var updateNote = _mapper.Map<Note>(noteViewModel);
            _context.Notes.Update(updateNote);
            await _context.SaveChangesAsync();

            return Ok(updateNote);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NoteViewModel noteViewModel)
        {
            var newNote = _mapper.Map<Note>(noteViewModel);
            _context.Notes.Add(newNote);
            await _context.SaveChangesAsync();

            return Created($"/{newNote.Id}", newNote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var noteToDelete = await _context.Notes
                .Include(_ => _.Employee)
                .FirstOrDefaultAsync();

            if (noteToDelete == null) return NotFound();

            _context.Notes.Remove(noteToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
