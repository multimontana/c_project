using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Floor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FloorDrawing {get; set; }
        public string Note { get; set; }
        public string MethodNamesofRooms {get; set; }
        public int? BuildingId {get; set; }
        public int? VisioId { get; set; }
        public byte[] Vsdfile { get; set; }
        public string ExternalId { get; set; }
        public Guid? UnitID {get; set; }
        public Guid? ImobjId { get; set; }
        public Guid? PeripheralDatabaseId { get; set; }
        public int? ComplementaryId { get; set; }

        public virtual Building BuildingNavigationId { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
