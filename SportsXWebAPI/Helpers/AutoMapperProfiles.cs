using AutoMapper;
using SportsXDomain;
using SportsXWebAPI.Dto;

namespace SportsXWebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente,ClienteDto>();
            CreateMap<ClienteTelefone,ClienteTelefoneDto>().ReverseMap();
        }
    }
}