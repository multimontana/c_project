#nullable enable
using System;

namespace DataAccess.DTOs
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? Lastname { get; set; }
        public string? Initials { get; set; }
        public int? RoomId { get; set; }
        public int? PositionId { get; set; }
        public Guid? SubdivisionId { get; set; }
        public string? Phone { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? Phone4 { get; set; }
        public string? Fax { get; set; }
        public string? Pager { get; set; }
        public string? Email { get; set; }
        public byte[]? Photo { get; set; }
        public string? Notes { get; set; }
        public int? VisioId { get; set; }
        public string? LoginName { get; set; }
        public string ExternalId { get; set; }
        public int? WorkspaceId { get; set; }
        public bool? Admin { get; set; }
        public bool? SupportOperator { get; set; }
        public bool? NetworkAdmin { get; set; }
        public bool? SupportEngineer { get; set; }
        public bool? SupportAdmin { get; set; }
        public bool? Removed { get; set; }
        public string? Sid { get; set; }
        public Guid? ImobjId { get; set; }
        public byte[] RowVersion { get; set; }
        public bool SdwebAccessIsGranted { get; set; }
        public byte[] SdwebPassword { get; set; }
        public Guid? PeripheralDatabaseId { get; set; }
        public int? ComplementaryId { get; set; }
        public Guid? ComplementaryGuidId { get; set; }
        public string? Number { get; set; }
        public string? TimeZoneId { get; set; }
        public Guid? CalendarWorkScheduleId { get; set; }
        public bool? IsLockedForOsi { get; set; }

        public virtual CalendarWorkSchedule? CalendarWorkSchedule { get; set; }
        public virtual TimeZone? TimeZone { get; set; }
        public virtual Position? PositionNavigation { get; set; }

        public virtual Workplace Workspace { get; set; }
    }
}