using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Workplace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RoomId { get; set; }
        public string Note { get; set; }
        public string ExternalId { get; set; }
        public Guid? UnitID {get; set; }
        public Guid? ImobjId { get; set; }
        public Guid? PeripheralDatabaseId { get; set; }
        public int? ComplementaryId { get; set; }

        public virtual Room RoomNavigationId { get; set; }
        public virtual ICollection<User> Users { get; set; }
}
}
