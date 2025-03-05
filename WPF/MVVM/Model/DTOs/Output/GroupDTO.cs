using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Output
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public required string GroupName { get; init; }
        public required string Description { get; init; }
        public List<UserDTO> Users { get; set; }
        public UserDTO? Creator => Users.FirstOrDefault();
    }
}
