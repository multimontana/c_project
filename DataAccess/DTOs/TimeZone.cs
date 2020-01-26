using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public partial class TimeZone
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public short BaseUtcOffsetInMinutes { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<User> Users{ get; set; }
    }
}
