using AutoMapper;
using Repository.Data.Models;
using Service.DTOs;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MachineDTO, Machine>().ReverseMap();
            CreateMap<MachineProductionDTO, MachineProduction>().ReverseMap();
        }
    }
}
