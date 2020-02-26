using System;

namespace InfraManager.WebApi.DAL.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LoginName { get; set; }

        public string Email { get; set; }

        public byte[]? SdwebPassword { get; set; }
    }
}