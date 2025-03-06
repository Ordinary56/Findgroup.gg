using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Output
{
    
    public sealed record PostDTO
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
        public DateTime CreatedDate { get; init; }
        public bool IsActive { get; init; }
        public string? UserId { get; init; }
        public int? CategoryId { get; init; }
        public Guid? Groupid { get; init; }
        public UserDTO Creator { get; init; }
        public GroupDTO Group { get; init; }
    }
}
