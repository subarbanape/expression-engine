using Common.Infrastructure.Interfaces;
using System;

namespace Common.Infrastructure.Models
{
    public class RequestAndTaskLogActionParams : IActionParams
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

        public override string ToString()
        {
            return $"WorkflowId: {WorkflowId}, TaskName: {TaskName}, RequestId: {RequestId}, User: {User}, " +
                $"Date: {Date}, TaskAssignedDate: {TaskAssignedDate}, TaskCancelledDate: {TaskCancelledDate}, " +
                $"TaskCompletedDate: {TaskCompletedDate}, FromUser: {FromUser}, Initiator: {Initiator}, " +
                $"OriginalUser: {OriginalUser}";
        }
    }
}
