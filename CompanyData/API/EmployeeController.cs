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
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            return await _context.Employees
                .Include(_ => _.Company)
                .Include(_ => _.Notes)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employeeById = await _context.Employees
                .Include(_ => _.Company)
                .Include(_ => _.Notes)
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();

            if (employeeById == null) return NotFound();

            return Ok(employeeById);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeViewModel employeeViewModel)
        {
            var updateEmployee = _mapper.Map<Employee>(employeeViewModel);
            _context.Employees.Update(updateEmployee);
            await _context.SaveChangesAsync();

            return Ok(updateEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeViewModel employeeViewModel)
        {
            var newEmployee = _mapper.Map<Employee>(employeeViewModel);
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            return Created($"/{newEmployee.Id}", newEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeToDelete = await _context.Employees
                .Include(_ => _.Company)
                .Include(_ => _.Notes)
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();

            if (employeeToDelete == null) return NotFound();

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
