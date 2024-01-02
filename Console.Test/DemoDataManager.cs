using Common.Helper;
using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Test
{
    public class DemoDataManager : IDataManager
    {
        TimeSpan IDataManager.CalculateTotalTaskDuration(string taskName, int requestId)
        {
            return DateTime.Today.AddDays(2) - DateTime.Today;
        }

        Response<Request[]> IDataManager.GetActiveRequests()
        {
            throw new NotImplementedException();
        }

        Response<WorkflowTask[]> IDataManager.GetActiveTasks(int requestId)
        {
            throw new NotImplementedException();
        }

        RequestLog IDataManager.GetRecentRequestLog(string requestId)
        {
            throw new NotImplementedException();
        }

        TaskLog IDataManager.GetRecentTaskCompleteEvent(string taskName, int requestId)
        {
            throw new NotImplementedException();
        }

        TaskLog IDataManager.GetRecentTaskStartEvent(string taskName, int requestId)
        {
            throw new NotImplementedException();
        }

        Response<Request> IDataManager.GetRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        Response<string> IDataManager.GetRequestTableColumnValue(string columnName, int requestId)
        {
            throw new NotImplementedException();
        }

        Response<Request[]> IDataManager.GetSuspendedRequests()
        {
            throw new NotImplementedException();
        }

        Response<WorkflowTaskStatus> IDataManager.GetTaskStatus(string taskName, int requestId)
        {
            return Response<WorkflowTaskStatus>.Success(WorkflowTaskStatus.Complete);
            //return Response<WorkflowTaskStatus>.Success((WorkflowTaskStatus)
            //    new Random().Next((int)WorkflowTaskStatus.None, (int)WorkflowTaskStatus.Removed));
        }
}
}
