using Common.Helper;
using Common.Infrastructure.Data.Interfaces;
using Common.Infrastructure.Enums;
using Common.Infrastructure.Expression.Interfaces;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine
{
    public class MacroProcessor : IMacroProcessor
    {
        public IDataManager DataManager { get; set; }
        public IConfigManager ConfigManager { get; private set; }

        public MacroProcessor(IDataManager dataManager,
            IConfigManager configManager)
        {
            this.DataManager = dataManager;
            this.ConfigManager = configManager;
        }

        public IResponse<string> Run(IMacro macro, IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            try
            {
                switch (macro.Name)
                {
                    case "GetActiveTasksInCollection": return GetActiveTasksInCollection(macro.Params);
                    case "GetTaskStatus": return GetTaskStatus(macro.Params);
                    case "CalculateTaskDuration": return CalculateTaskDuration(macro.Params);
                    case "CalculateTasksDuration": return CalculateTasksDuration(macro.Params);
                    case "CalculateRequestDuration": return CalculateRequestDuration(macro.Params);
                    case "GetRecentRequestEvent": return GetRecentRequestEvent(macro.Params);
                    case "CalculateTimeElapsedBetweenTasks": return CalculateTimeElapsedBetweenTasks(macro.Params);
                    case "GetRequestTableColumnValue": return GetRequestTableColumnValue(macro.Params);
                    case "CalculateRequestPercentageComplete": return CalculateRequestPercentageComplete(macro.Params);
                    case "CalculateRequestApprovalStatus": return CalculateRequestApprovalStatus(macro.Params, expressionInterpreter, actionParams);
                    default: return new Response<string>() { ResponseCode = ResponseCode.MacroNotFound };
                }
            }
            catch (Exception ex)
            {
                return Response<string>.Failure(ex);
            }
        }

        private IResponse<string> GetActiveTasksInCollection(Dictionary<string, object> paramsList)
        {
            try
            {
                var response = DataManager.GetActiveTasks(SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
                var activeTasks = response.Data;

                if (response.ResponseCode == ResponseCode.TaskNotFound) return Response<string>.Set(ResponseCode.TaskNotFound);
                if (!response.IsSuccess() || response.IsCriticalError()) return response.ConvertDataToString();
                if (activeTasks == null || activeTasks.Length == 0) return Response<string>.Set(ResponseCode.TaskNotFound);

                var activeTasksArray = activeTasks.ToList().Select(item => item.TaskDisplayName).Distinct().ToArray();
                string activeTasksCommaSeparated = string.Join(", ", activeTasksArray).Trim().Trim(',');
                return Response<string>.Success(activeTasksCommaSeparated);
            }
            catch (Exception ex)
            {
                return Response<string>.Failure(ex);
            }
        }

        Response<Tuple<string, string>> CalculateRequestApprovalStatusAndPercentage(int requestId, 
            IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            var requestResponse = DataManager.GetRequest(requestId);
            if (!requestResponse.IsSuccess() || requestResponse.IsCriticalError())
            {
                return requestResponse.GetAsError<Tuple<string,string>>();
            }

            var request = requestResponse.GetData<Request>();

            if (request.Status == WorkflowStatus.Complete) return Response<Tuple<string, string>>.Success(new Tuple<string, string>("Process Complete", "100"));
            if (request.Status == WorkflowStatus.Cancelled) return Response<Tuple<string, string>>.Success(new Tuple<string, string>("Process Cancelled", "100"));

            var response = DataManager.GetActiveTasks(requestId);
            if (response.IsCriticalError())
            {
                return response.GetAsError<Tuple<string, string>>();
            }

            if (!response.IsSuccess() && response.ResponseCode == ResponseCode.TaskNotFound)
            {
                return response.GetAsError<Tuple<string, string>>();
            }

            if (!response.IsSuccess())
            {
                return response.GetAsError<Tuple<string, string>>();
            }

            var activeTasks = response.Data;
            if (activeTasks == null || activeTasks.Length == 0)
                return Response<Tuple<string, string>>.Set(ResponseCode.TaskNotFound);

            var distinctActiveTasks = activeTasks.ToList().Select(item => item.TaskDisplayName).Distinct().ToArray();

            if (distinctActiveTasks == null || distinctActiveTasks.Length == 0) return Response<Tuple<string, string>>.Set(ResponseCode.TaskNotFound);

            var requestStatusTemplates = ConfigManager.GetRequestStatusTemplate(distinctActiveTasks);

            if (requestStatusTemplates == null || requestStatusTemplates.Count() == 0)
            {
                return Response<Tuple<string, string>>.Set(ResponseCode.TaskNotFound);
            }

            // calculate request approval status
            string expressionResult = string.Empty;
            if (expressionInterpreter != null && actionParams != null)
            {
                requestStatusTemplates.ToList().ForEach(item =>
                {
                    var response = item.Expression.Evaluate(expressionInterpreter, actionParams);
                    expressionResult += (response.IsSuccess() ? ", " + response.Data : string.Empty);
                });
                expressionResult = expressionResult.Trim().Trim(',').Trim();
            }

            // calcualte request percentage complete
            int percentageComplete = 10;
            requestStatusTemplates.ToList().ForEach(item =>
            {
                if (percentageComplete < item.PercentageComplete)
                {
                    percentageComplete = item.PercentageComplete;
                }
            });

            return Response<Tuple<string, string>>.Success(new Tuple<string,string>(expressionResult, percentageComplete.ToString()));
        }

        private IResponse<string> CalculateRequestApprovalStatus(Dictionary<string, object> paramsList, IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            try
            {
                var requestId = SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString());
                var responseRequestApprovalStatusAndPercentage = CalculateRequestApprovalStatusAndPercentage(requestId, expressionInterpreter, actionParams);
                if (responseRequestApprovalStatusAndPercentage.IsCriticalError() || !responseRequestApprovalStatusAndPercentage.IsSuccess())
                    return responseRequestApprovalStatusAndPercentage.ConvertDataToString();
                var requestApprovalStatus = responseRequestApprovalStatusAndPercentage.Data?.Item1;
                return Response<string>.Success(requestApprovalStatus);
            }
            catch (Exception ex)
            {
                return Response<string>.Failure(ex);
            }
        }

        private IResponse<string> CalculateRequestPercentageComplete(Dictionary<string, object> paramsList)
        {
            try
            {
                var requestId = SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString());
                var responseRequestApprovalStatusAndPercentage = CalculateRequestApprovalStatusAndPercentage(requestId, null, null);
                if (responseRequestApprovalStatusAndPercentage.IsCriticalError() || !responseRequestApprovalStatusAndPercentage.IsSuccess()) 
                    return responseRequestApprovalStatusAndPercentage.ConvertDataToString();
                var percentageComplete = responseRequestApprovalStatusAndPercentage.Data?.Item2;
                return Response<string>.Success(percentageComplete);
            }
            catch (Exception ex)
            {
                return Response<string>.Failure(ex);
            }
        }

        private IResponse<string> GetRequestTableColumnValue(Dictionary<string, object> paramsList)
        {
            var response = DataManager.GetRequestTableColumnValue(paramsList["Column"].ToString(), SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            return response;
        }

        private IResponse<string> CalculateTimeElapsedBetweenTasks(Dictionary<string, object> paramsList)
        {
            var startTask = DataManager.GetRecentTaskStartEvent(paramsList["StartTask"].ToString(), SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            var endTask = DataManager.GetRecentTaskStartEvent(paramsList["EndTask"].ToString(), SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            if (endTask == null || startTask == null) return new Response<string>(ResponseCode.TaskNotFound);
            return Response<string>.Success(DateTimeHelper.GetTimeSpan_PrettyDate(startTask.EventDate, endTask.EventDate));
        }

        private IResponse<string> GetRecentRequestEvent(Dictionary<string, object> paramsList)
        {
            var requestLog = DataManager.GetRecentRequestLog(paramsList["RequestId"].ToString());
            if (requestLog == null) return Response<string>.Failure();
            return Response<string>.Success(requestLog.Event);
        }

        private IResponse<string> CalculateRequestDuration(Dictionary<string, object> paramsList)
        {
            var response = DataManager.GetRequest(SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            if (!response.IsSuccess()) return response.ConvertDataToString();
            var request = response.Data;
            if (request == null) return Response<string>.Failure();
            return Response<string>.Success(DateTimeHelper.GetTimeSpan_PrettyDate(request.RequestedDate, DateTime.Now));
        }

        private IResponse<string> CalculateTasksDuration(Dictionary<string, object> paramsList)
        {
            var taskNames = (string[])paramsList["Tasks"];
            var timeSpan = new TimeSpan();
            var requestId = SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString());
            Array.ForEach(taskNames, item => { timeSpan += DataManager.CalculateTotalTaskDuration(item, requestId); });
            return Response<string>.Success(DateTimeHelper.GetPrettyDate(timeSpan));
        }

        private IResponse<string> CalculateTaskDuration(Dictionary<string, object> paramsList)
        {
            var taskDuration = DataManager.CalculateTotalTaskDuration(paramsList["TaskName"].ToString(), SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            return Response<string>.Success(DateTimeHelper.GetPrettyDate(taskDuration));
        }

        private IResponse<string> GetTaskStatus(Dictionary<string, object> paramsList)
        {
            var response = DataManager.GetTaskStatus(paramsList["TaskName"].ToString(), SimpleDataConversionHelper.ToInt(paramsList["RequestId"].ToString()));
            if (!response.IsSuccess()) return response.ConvertDataToString();
            return Response<string>.Success(response.Data.ToString());
        }
    }
}
