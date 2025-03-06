using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Input
{
    public sealed record RegisterNewPostDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Title { get; init; }
        public required string Content { get; init; }
        public required string UserId { get; init; }
        public int CategoryId { get; init; }

    }
}
