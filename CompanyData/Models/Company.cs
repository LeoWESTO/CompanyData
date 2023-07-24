namespace CompanyData.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Phone { get; set; }
        public List<Employee> Employees { get; set; } = new();
        public List<History> Histories { get; set; } = new();
    }
}
