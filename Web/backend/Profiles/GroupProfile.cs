using AutoMapper;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Profiles
{
    public sealed class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
            CreateMap<GroupDTO, Group>();
            CreateMap<CreateGroupDTO, Group>();
            CreateMap<Group, CreateGroupDTO>();
        }
    }
}
