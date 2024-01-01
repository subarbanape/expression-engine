using Common.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Common.Infrastructure.Interfaces
{
    public interface IWorkflowManager
    {
        Response<Workflow> GetWorkflow(string workflowId);
        Response<WorkflowData<T>> GetWorkflowData<T>(string workflowKey);
        IWorkflowEvent GetEvent<T>(T eventData);
        Response<string> GetUserFullName(string requestedByUserName);
        Response<string> GetReportViewLink(string workflowId);
        Response<string> GetFlowChartLink(string workflowId);
        Response<string> GetProjectArtifactsLink(string projectId);
        Response<WorkflowTask> GetTask(string workflowTaskId);
        Response<DateTime?> GetWorkflowResumedDate(string workflowId);
        Response<DateTime?> GetWorkflowCancelDate(string workflowId);
        Response<DateTime?> GetWorkflowSuspendDate(string workflowId);
        Response<string> CreateRequest(string processDefId, string initiator, 
            string procInstId, string workObjectId,
            NameValuePair[] attributes, string procInstName);
    }
}