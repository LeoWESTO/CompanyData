namespace CompanyData.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? StoreCity { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
