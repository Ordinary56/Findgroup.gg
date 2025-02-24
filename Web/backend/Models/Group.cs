using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public sealed record Group
    {
        [Key]
        public Guid Id { get; set; }
        public required string GroupName { get; set; }
        public required string Description { get; set; }
        public required int MemberLimit { get; set; } = 1;
        public IList<User> Users { get; set; } = [];
        public IList<Message> Messages { get; set; } = [];
        public User? Creator => Users.FirstOrDefault();
        public Post? Post { get; set; }

    }
}
