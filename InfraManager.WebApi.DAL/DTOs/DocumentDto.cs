namespace InfraManager.WebApi.DAL.DTOs
{
    using System;

    public class DocumentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Size { get; set; }

        public string Extension { get; set; }

        public byte[] Data { get; set; }
    }

}
