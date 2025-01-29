using AutoMapper;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;

namespace Findgroup_Backend.Profiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
