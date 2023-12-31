﻿using CompanyData.Models;

namespace CompanyData.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Phone { get; set; }
    }
}
