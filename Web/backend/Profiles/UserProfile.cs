﻿using AutoMapper;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Profiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<RegisterUserDTO, User>();
        }
    }
}
