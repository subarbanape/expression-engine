using Common.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System.Linq;

namespace Common.Infrastructure.Models
{
    public class RequestAndTaskStatCommand : ICommand
    {
        public RequestAndTaskStatCommand() { }
        public RequestAndTaskStatCommand(RequestAndTaskStatCommandAction[] actions, string command)
        {
            Actions = actions?.ToList().ConvertAll(action => (IAction)action).ToArray();
            Command = command;
        }

        public string Command { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IAction[] Actions { get; set; }
    }
    public class CalculateRequestOneLineSummary : RequestAndTaskStatCommand
    {

    }
    public class CalculateRequestApprovalStatus : RequestAndTaskStatCommand
    {
    }
    public class CalculateRequestPercentageComplete : RequestAndTaskStatCommand
    {
    }
    public class CalculateTimeElapseForCollectATCAmount : RequestAndTaskStatCommand
    {
    }
    public class CalculateTimeElapseForCollectFinanceData : RequestAndTaskStatCommand
    {
    }
    public class CalculateTaskOneLineSummaryForAllTasks : RequestAndTaskStatCommand
    {
    }
    public class CalculateTimeElapseForExecutivesAndCFOReview : RequestAndTaskStatCommand
    {
    }
}
