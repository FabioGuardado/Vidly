using AutoMapper;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CustomerDto, Customer>();
            CreateMap<Movie, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>();
        }
    }
}
