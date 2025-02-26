using AutoMapper;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.InputDTOs;
using Findgroup_Backend.Models.DTOs.OutputDTOs;

namespace Findgroup_Backend.Profiles
{
    public sealed class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>();
            CreateMap<Post, RegisterPostDTO>();
        }
    }
}
