using AutoMapper;
using PickupAnnouncer.Models.DAO;
using PickupAnnouncer.Models.DAO.Config;
using PickupAnnouncer.Models.DTO;
using PickupAnnouncer.Models.Requests;
using System.Linq;

namespace PickupAnnouncer.Mappings
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<StudentDAO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<GradeLevelRequest, GradeLevel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
