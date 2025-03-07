using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Output
{
    
    public sealed record PostDTO
    {
        public int id { get; init; }
        public required string title { get; init; }
        public required string content { get; init; }
        public DateTime createdDate { get; init; }
        public bool isActive { get; init; }
        public string? userId { get; init; }
        public int? categoryId { get; init; }
        public Guid? groupid { get; init; }
        public UserDTO creator { get; init; }
        public GroupDTO group { get; init; }
    }
}
