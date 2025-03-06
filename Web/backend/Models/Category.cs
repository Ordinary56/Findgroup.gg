using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Findgroup_Backend.Models;
public class Category
{

    [Key]
    public int Id { get; set; }
    public required string CategoryName { get; set; }
    [JsonIgnore]
    public IList<Post> Posts { get; set; } = [];
}
