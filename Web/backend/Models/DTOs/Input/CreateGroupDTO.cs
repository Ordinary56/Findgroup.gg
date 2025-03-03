using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record CreateGroupDTO
    {
        public required string GroupName { get; init; }
        public required string Description { get; init; }
        public required int MemberLimit { get; init; }
        public required string UserId { get; init; }
        public required int PostId { get; init; }
    }
}
