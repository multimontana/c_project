namespace InfraManager.WebApi.DAL.DTOs
{
    using System;

    /// <summary>
    /// The Call.
    /// </summary>
    public class Call
    {
        public Guid Id { get; set; }

        public Guid? OwnerId { get; set; }

        public string OwnerFullName { get; set; }

        public string Htmldescription { get; set; }

        public string Htmlsolution { get; set; }

        public Guid? AccomplisherId { get; set; }

        public byte ReceiptType { get; set; }

        public string AccomplisherFullName { get; set; }

        public Guid? ExecutorId { get; set; }

        public string ExecutorFullName { get; set; }

        public Guid? QueueId { get; set; }

        public Guid? ServiceId { get; set; }

        public string ServiceItemFullName { get; set; }

        public int Number { get; set; }

        public string ClientFullName { get; set; }

        public DateTime? UtcDateRegistered { get; set; }

        public DateTime? UtcDateOpened { get; set; }

        public DateTime? UtcDateClosed { get; set; }    

        public string CallSummaryName { get; set; }

        public string EntityStateName { get; set; }

        public Guid PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        public Guid CallTypeId { get; set; }

        public virtual CallType CallType { get; set; }
    }
}