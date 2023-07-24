using CompanyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<History> Histories { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
    }
}
