using Common.Infrastructure.Enums;
using Common.Infrastructure.Models;
using System;

namespace Common.Infrastructure.Interfaces
{
    public interface IDataManager
    {
        RequestLog GetRecentRequestLog(string requestId);
        Response<Request> GetRequest(int requestId);
        TaskLog GetRecentTaskCompleteEvent(string taskName, int requestId);
        TaskLog GetRecentTaskStartEvent(string taskName, int requestId);
        TimeSpan CalculateTotalTaskDuration(string taskName, int requestId);
        Response<WorkflowTask[]> GetActiveTasks(int requestId);
        Response<Request[]> GetActiveRequests();
        Response<Request[]> GetSuspendedRequests();
        Response<string> GetRequestTableColumnValue(string columnName, int requestId);
        Response<WorkflowTaskStatus> GetTaskStatus(string taskName, int requestId);
    }
}