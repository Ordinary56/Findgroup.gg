﻿namespace Findgroup_Backend.Models.DTOs.Output
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
