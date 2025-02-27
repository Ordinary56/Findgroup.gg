namespace Findgroup_Backend.Models
{
    public sealed record Activity
    {
        public required string ProcessName { get; set; }
        public required string ActivityName { get; set; }
    }
}
