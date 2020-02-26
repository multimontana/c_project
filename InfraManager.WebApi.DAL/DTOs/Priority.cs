namespace InfraManager.WebApi.DAL.DTOs
{
    using System;
    using System.Collections.Generic;

    using InfraManager.WebApi.DAL.DTOs.Calls;

    public class Priority
    {
        public Guid Id { get; set; }

        public int Color { get; set; }

        public virtual ICollection<Call> Call { get; set; }
    }
}