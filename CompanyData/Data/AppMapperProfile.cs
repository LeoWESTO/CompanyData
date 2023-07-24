using AutoMapper;
using CompanyData.Models;
using CompanyData.ViewModels;

namespace CompanyData.Data
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<CompanyViewModel, Company>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<HistoryViewModel, History>();
            CreateMap<NoteViewModel, Note>();
        }
    }
}
