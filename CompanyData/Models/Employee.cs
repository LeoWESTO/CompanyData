namespace CompanyData.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public DateTime? BirthDate {  get; set; }
        public string? Position { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<Note> Notes { get; set; } = new();
    }
}
