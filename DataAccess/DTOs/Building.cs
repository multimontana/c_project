using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Build { get; set; }
        public string Case { get; set; }
        public string WiringDiagram { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public int? VisioId { get; set; }
        public string ExternalId { get; set; }
        public Guid? UnitsId { get; set; }
        public Guid? OrganizationsId { get; set; }
        public Guid? ImobjId { get; set; }
        public string House { get; set; }
        public Guid? PeripheralDatabaseId { get; set; }
        public int? ComplementaryId { get; set; }
        public string TimeZoneId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual TimeZone TimeZone { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
    }
}