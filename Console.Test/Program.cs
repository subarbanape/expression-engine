// See https://aka.ms/new-console-template for more information
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using Console.Test;
using ExpressionEngine;

IMacroProcessor macroProcessor = new MacroProcessor(new DemoDataManager(), new DemoConfigManager());
IExpressionInterpreter expressionInterpreter = new ExpressionInterpreter(macroProcessor);

IAction action = new RequestAndTaskStatCommandAction();
action.Name = "OneLineSummary";

var expression = ExpressionBuilder.ExpressionChain();
var root = expression;

var criteriaExpression1 = new CriteriaExpression("Task {TaskName} is assigned to {User} on {TaskAssignedDate} and cancelled on {TaskCancelledDate}. Overall time taken: MACRO(CalculateTaskDuration, {TaskName}, {RequestId})",
            "MACRO(GetTaskStatus, {TaskName}, {RequestId}) is Cancelled or Removed");

var anyCondition1 = criteriaExpression1.AnyCondition();

var criteriaExpression2 = anyCondition1.CriteriaExpression("Task {TaskName} is assigned to {User} on {TaskAssignedDate}. Time elapsed: MACRO(CalculateTaskDuration, {TaskName}, {RequestId})",
            "MACRO(GetTaskStatus, {TaskName}, {RequestId}) is Assigned or New or Overdue");

var anyCondition2 = criteriaExpression2.AnyCondition();

var criteriaExpression3 = anyCondition2.CriteriaExpression("Task {TaskName} completed on {TaskCompletedDate} by {User}. Overall time taken: MACRO(CalculateTaskDuration, {TaskName}, {RequestId})",
            "MACRO(GetTaskStatus, {TaskName}, {RequestId}) is Complete");

expression.Successor = criteriaExpression1;

action.Expression = expression;
action.Params = new RequestAndTaskStatCommandActionParams { 
    Date = DateTime.Today,
    FromUser = "John Doe",
    Initiator = "Lily Allen",
    OriginalUser = "John Doe",
    RequestId = 3991,
    TaskAssignedDate = DateTime.Today.AddDays(-2),
    TaskCompletedDate = DateTime.Today,
    TaskName = "Deliver Pizza",
    User = "John Doe",
    WorkflowId = "Test Workflow Id",
};

var response = action.Expression.Evaluate(expressionInterpreter, action.Params);
System.Console.WriteLine(response.ToString());
System.Console.ReadLine();


