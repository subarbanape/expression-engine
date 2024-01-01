using Common.Infrastructure.Enums;
using System;

namespace Common.Infrastructure.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string RequestedByUserName { get; set; }
        public string RequestedByUserFullName { get; set; }
        public DateTime RequestedDate { get; set; }
        public string ReportViewLink { get; set; }
        public string FlowChartLink { get; set; }
        public string ProjectArtifactsLink { get; set; }
        public WorkflowStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string WorkflowId { get; set; }
    }
}
