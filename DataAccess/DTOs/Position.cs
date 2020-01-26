using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ComplementaryId { get; set; }
        public Guid? ImobjId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
