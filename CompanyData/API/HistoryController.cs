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
            return await _context.Histories
                .Include(_ => _.Company)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> Get(int id)
        {
            var historyById = await _context.Histories
                .Include(_ => _.Company)
                .FirstOrDefaultAsync();

            if (historyById == null) return NotFound();

            return Ok(historyById);
        }

        [HttpPut]
        public async Task<IActionResult> Put(HistoryViewModel historyViewModel)
        {
            var updateHistory = _mapper.Map<History>(historyViewModel);
            _context.Histories.Update(updateHistory);
            await _context.SaveChangesAsync();

            return Ok(updateHistory);
        }

        [HttpPost]
        public async Task<IActionResult> Post(HistoryViewModel historyViewModel)
        {
            var newHistory = _mapper.Map<History>(historyViewModel);
            _context.Histories.Add(newHistory);
            await _context.SaveChangesAsync();

            return Created($"/{newHistory.Id}", newHistory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var historyToDelete = await _context.Histories
                .Include(_ => _.Company)
                .FirstOrDefaultAsync();

            if (historyToDelete == null) return NotFound();

            _context.Histories.Remove(historyToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
