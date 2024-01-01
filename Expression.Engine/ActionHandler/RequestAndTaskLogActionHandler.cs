using BusinessCase.Infrastructure.Interfaces;
using Common.Infrastructure.Data.Interfaces;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using ExpressionEngine;
using Microsoft.Extensions.Logging;
using System;

namespace BusinessCase.WorkflowEventsConsumer.ActionHandler
{
    public class RequestAndTaskLogActionHandler : IActionHandler
    {
        IExpressionInterpreter expressionInterpreter;

        public RequestAndTaskLogActionHandler(
            ILoggerFactory loggerFactory,
            IBusinessCaseDataManager businessCaseDataManager, 
            IConfigManager configManager)
        {
            var macroProcessor = new MacroProcessor(businessCaseDataManager, configManager, 
                                        loggerFactory.CreateLogger<MacroProcessor>());
            expressionInterpreter = new ExpressionInterpreter(macroProcessor);
        }

        public Response<T> Invoke<T>(IAction action)
        {
            var response = action.Expression.Evaluate(expressionInterpreter, action.Params);
            return (Response<T>)Convert.ChangeType(response, typeof(Response<T>));
        }
    }
}
