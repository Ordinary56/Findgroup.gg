namespace Findgroup_Backend.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
        public string UserId { get; set; }
        public int TopicId { get; set; }
        public bool IsActive { get; set; }

    }
}
