using AutoMapper;
using CompanyData.Data;
using CompanyData.Models;
using CompanyData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompanyController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            return await _context.Companies
                .Include(_ => _.Employees)
                .ThenInclude(_ => _.Notes)
                .Include(_ => _.Histories)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var companyById = await _context.Companies
                .Include(_ => _.Employees)
                .ThenInclude(_ => _.Notes)
                .Include(_ => _.Histories)
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();

            if (companyById == null) return NotFound();
                
            return Ok(companyById);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CompanyViewModel companyViewModel)
        {
            var updateCompany = _mapper.Map<Company>(companyViewModel);
            _context.Companies.Update(updateCompany);
            await _context.SaveChangesAsync();

            return Ok(updateCompany);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyViewModel companyViewModel)
        {
            var newCompany = _mapper.Map<Company>(companyViewModel);
            _context.Companies.Add(newCompany);
            await _context.SaveChangesAsync();

            return Created($"/{newCompany.Id}", newCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var companyToDelete = await _context.Companies
                .Include(_ => _.Employees)
                .ThenInclude(_ => _.Notes)
                .Include(_ => _.Histories)
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();

            if (companyToDelete == null) return NotFound();

            _context.Companies.Remove(companyToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
