using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Findgroup_Backend.Models
{
    public record Group
    {
        [Key]
        public Guid Id { get; set; }
        public required string GroupName { get; set; }
        public required string Description { get; set; }
        public required int MemberLimit { get; set; } = 1;
        public int? PostId { get; set; }
        [JsonIgnore]
        public IList<User> Users { get; set; } = [];
        public IList<Message> Messages { get; set; } = [];
        public User? Creator => Users.FirstOrDefault();
        [JsonIgnore]
        public virtual Post? Post { get; set; }

    }
}
