using CompanyData.Models;

namespace CompanyData.ViewModels
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? StoreCity { get; set; }
        public int? CompanyId { get; set; }
    }
}
