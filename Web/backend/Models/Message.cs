namespace Findgroup_Backend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Content { get; set; } = "";
    }
}
