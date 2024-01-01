using System;

namespace Common.Infrastructure.Interfaces
{
    public interface IActionParams
    {
        public string WorkflowId { get; set; }
        public string TaskName { get; set; }
        public int RequestId { get; set; }
        public string User { get; set; }
        public DateTime? Date { get; set; }
        public DateTime TaskAssignedDate { get; set; }
        public DateTime TaskCancelledDate { get; set; }
        public DateTime TaskCompletedDate { get; set; }
        public string FromUser { get; set; }
        public string Initiator { get; set; }
        public string OriginalUser { get; set; }
    }
}