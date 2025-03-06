using AutoMapper;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Profiles
{
    public sealed class PostProfile : Profile
    {
        public PostProfile()
        {
            // HACK: no need to explicitly set values since the profile can do it
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group));
            CreateMap<Post, CreatePostDTO>();
            CreateMap<CreatePostDTO, Post>();

        }
    }
}
