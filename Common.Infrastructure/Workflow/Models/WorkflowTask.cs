using Common.Infrastructure.Enums;
using System;

namespace Common.Infrastructure.Models
{
    public class WorkflowTask
    {
        public int RequestId { get; set; }
        public string WorkflowTaskId { get; set; }
        public string WorkflowTaskGroupId { get; set; }
        public int Id { get; set; }
        public string WorkflowId { get; set; }
        public string TaskName { get; set; }
        public string TaskDisplayName { get; set; }
        public WorkflowTaskStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime AssignedDate { get; set; }
        public string TaskAssigneeUserName { get; set; }
        public string TaskAssigneeUserFullName { get; set; }
        public string TaskOriginalUserName { get; set; }
        public string TaskOriginalUserFullName { get; set; }
    }
}
