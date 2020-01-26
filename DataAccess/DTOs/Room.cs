using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Drawing { get; set; }
        public string Note { get; set; }
        public int? TypeId { get; set; }
        public int? FloorId {get; set; }
        public string Size { get; set; }
        public string Location {get; set; }
        public string ServicedZone {get; set; }
        public string Key { get; set; }
        public int? VisioId { get; set; }
        public string ExternalId { get; set; }
        public Guid? SubdivisionId {get; set; }
        public Guid? ImobjId { get; set; }
        public Guid? PeripheralDatabaseId { get; set; }
        public int? ComplementaryId { get; set; }

        public virtual Floor FloorNavigationId { get; set; }
        public virtual ICollection<Workplace> Workplaces { get; set; }
    }
}
