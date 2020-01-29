#nullable enable
using System;

namespace DataAccess.DTOs
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LoginName { get; set; }
        public byte[]? SdwebPassword { get; set; }
    }
}