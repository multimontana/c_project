namespace DataAccess.DTOs
{
    using System;
    using System.Collections.Generic;

    public partial class Priority
    {
        public Guid Id { get; set; }
        public int Color { get; set; }

        public virtual ICollection<Call> Call { get; set; }
    }
}