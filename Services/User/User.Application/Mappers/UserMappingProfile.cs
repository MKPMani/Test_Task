using AutoMapper;
using User.Application.Commands;
using User.Application.Responses;
using User.Core.Entities;

namespace User.Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Users, UserCreatedResponse>().ReverseMap();
            CreateMap<Users, CreateUserCommand>().ReverseMap();
        }
    }
}
    