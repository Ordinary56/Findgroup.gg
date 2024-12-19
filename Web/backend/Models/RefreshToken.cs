namespace Findgroup_Backend.Models
{
    public sealed class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }   
        public Guid UserId { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
        public User User { get; set; }

    }
}
