using CompanyData.Models;

namespace CompanyData.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Position { get; set; }
        public int? CompanyId { get; set; }
    }
}
