using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Input
{
    public sealed record CreateNewGroupDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string GroupName { get; init; }
        public required string Description { get; init; }
        public required int MemberLimit { get; init; }
        public required int PostId { get; init; }
        public required string UserId { get; init; }
    }
}
