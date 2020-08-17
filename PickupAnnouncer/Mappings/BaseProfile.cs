using AutoMapper;
using PickupAnnouncer.Models.DAO;
using PickupAnnouncer.Models.DTO;
using System.Linq;

namespace PickupAnnouncer.Mappings
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<StudentDAO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
