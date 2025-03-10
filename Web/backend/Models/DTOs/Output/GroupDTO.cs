namespace Findgroup_Backend.Models.DTOs.Output
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public required string GroupName { get; init; }
        public required string Description { get; init; }
        public required int MemberLimit { get; init; }
        public List<UserDTO> Users { get; set; } = [];
        public UserDTO? Creator => Users.FirstOrDefault();
        public List<Message> Messages { get; init; }
    }
}
