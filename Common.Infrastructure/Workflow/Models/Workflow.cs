using Common.Infrastructure.Enums;
using System;

namespace Common.Infrastructure.Models
{
    public class Workflow
    {
        public string WorkflowId { get; set; }
        public string WorkflowKey { get; set; }
        public string WorkflowInstanceName { get; set; }
        public string WorkflowName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ResumedDate { get; set; }
        public DateTime? SuspendDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public WorkflowStatus State { get; set; }
        public string Initiator { get; set; }
    }
}
