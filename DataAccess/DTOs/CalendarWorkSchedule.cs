using System;
using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public partial class CalendarWorkSchedule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Year { get; set; }
        public string ShiftTemplate { get; set; }
        public byte ShiftTemplateLeft { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
