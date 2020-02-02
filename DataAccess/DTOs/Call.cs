namespace DataAccess.DTOs
{
    using System;

    public partial class Call
    {
        public Guid Id { get; set; }

        public Guid? OwnerId { get; set; }

        public Guid? ExecutorId { get; set; }

        public Guid? QueueId { get; set; }

        public int Number { get; set; }

        public string ClientFullName { get; set; }

        public DateTime? UtcDateRegistered { get; set; }

        public DateTime? UtcDateOpened { get; set; }

        public string CallSummaryName { get; set; }

        public string EntityStateName { get; set; }


        public Guid PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        public Guid CallTypeId { get; set; }

        public virtual CallType CallType { get; set; }
    }
}