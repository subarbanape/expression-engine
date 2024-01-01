using Common.Helper;
using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEngine
{
    public class ExpressionInterpreter : IExpressionInterpreter
    {
        public IMacroProcessor MacroProcessor { get; }

        public ExpressionInterpreter(IMacroProcessor macroProcessor)
        {
            MacroProcessor = macroProcessor;
        }

        public IResponse<string> Interpret(IExpression expression, IActionParams actionParams)
        {
            var targetExpression = expression.TargetExpression;
            var macroExp = new MacroExpressionParser();
            var macroExpResult = macroExp.Interpret(targetExpression);
            if (!macroExpResult.IsSuccess() && macroExpResult.ResponseCode != ResponseCode.MacroNotFound) return Response<string>.Failure();

            if (macroExpResult.ResponseCode != ResponseCode.MacroNotFound)
            {
                var macroParams = macroExpResult.Data.Params;
                CollectionHelper.TryAdd(macroParams, "WorkflowId", actionParams.WorkflowId);
                CollectionHelper.TryAdd(macroParams, "Date", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.Date));
                CollectionHelper.TryAdd(macroParams, "TaskAssignedDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskAssignedDate));
                CollectionHelper.TryAdd(macroParams, "TaskCancelledDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCancelledDate));
                CollectionHelper.TryAdd(macroParams, "TaskCompletedDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCompletedDate));
                CollectionHelper.TryAdd(macroParams, "FromUser", actionParams.FromUser);
                CollectionHelper.TryAdd(macroParams, "RequestId", actionParams.RequestId.ToString());
                CollectionHelper.TryAdd(macroParams, "TaskName", actionParams.TaskName);
                CollectionHelper.TryAdd(macroParams, "OriginalUser", actionParams.OriginalUser);
                CollectionHelper.TryAdd(macroParams, "User", actionParams.User);
                CollectionHelper.TryAdd(macroParams, "Initiator", actionParams.Initiator);

                // Macroprocessor has some expressions too
                var macroRunResult = MacroProcessor.Run(macroExpResult.Data, this, actionParams);
                if (!macroRunResult.IsSuccess()) return macroRunResult;

                targetExpression = targetExpression.Replace(macroExpResult.Data.MacroExpression, macroRunResult.Data);
            }

            targetExpression = targetExpression
               .Replace("{WorkflowId}", actionParams.WorkflowId)
               .Replace("{Date}", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.Date))
               .Replace("{TaskAssignedDate}", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskAssignedDate))
               .Replace("{TaskCancelledDate}", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCancelledDate))
               .Replace("{TaskCompletedDate}", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCompletedDate))
               .Replace("{FromUser}", actionParams.FromUser)
               .Replace("{RequestId}", actionParams.RequestId.ToString())
               .Replace("{TaskName}", actionParams.TaskName)
               .Replace("{OriginalUser}", actionParams.OriginalUser)
               .Replace("{User}", actionParams.User)
               .Replace("{Initiator}", actionParams.Initiator);

            return Response<string>.EvalToTrue(targetExpression);
        }


        // Macro format: <macro> <auxiliary-verb> <value1> or <value2> or ...<value-n>
        public IResponse<bool> Interpret(ICriteria criteria, IActionParams actionParams)
        {
            var macroExp = new MacroExpressionParser();
            var macroExpResut = macroExp.Interpret(criteria.TargetCriteria);
            if (!macroExpResut.IsSuccess()) return Response<bool>.Failure();

            var macroParams = macroExpResut.Data.Params;
            CollectionHelper.TryAdd(macroParams, "WorkflowId", actionParams.WorkflowId);
            CollectionHelper.TryAdd(macroParams, "Date", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.Date));
            CollectionHelper.TryAdd(macroParams, "TaskAssignedDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskAssignedDate));
            CollectionHelper.TryAdd(macroParams, "TaskCancelledDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCancelledDate));
            CollectionHelper.TryAdd(macroParams, "TaskCompletedDate", DateTimeHelper.ToFormatMMDDYYYYhhmmtt(actionParams.TaskCompletedDate));
            CollectionHelper.TryAdd(macroParams, "FromUser", actionParams.FromUser);
            CollectionHelper.TryAdd(macroParams, "RequestId", actionParams.RequestId.ToString());
            CollectionHelper.TryAdd(macroParams, "TaskName", actionParams.TaskName);
            CollectionHelper.TryAdd(macroParams, "OriginalUser", actionParams.OriginalUser);
            CollectionHelper.TryAdd(macroParams, "User", actionParams.User);
            CollectionHelper.TryAdd(macroParams, "Initiator", actionParams.Initiator);

            var macroRunResult = MacroProcessor.Run(macroExpResut.Data, this, actionParams);
            if (!macroRunResult.IsSuccess()) return Response<bool>.Failure();

            var auxiliaryVerbExp = new AuxiliaryVerbParser();
            var auxiliaryVerbExpResut = auxiliaryVerbExp.Interpret(criteria.TargetCriteria);
            if (!auxiliaryVerbExpResut.IsSuccess()) return Response<bool>.Failure();

            var valuesToCompareExp = new ValuesToCompareExpressionParser();
            var valuesToCompareExpResult = valuesToCompareExp.Interpret(criteria.TargetCriteria);
            if (!valuesToCompareExpResult.IsSuccess()) return Response<bool>.Failure();

            // now check the criteria
            if (auxiliaryVerbExpResut.Data == "is")
            {
                bool criteriaMatch = false;
                foreach (var item in valuesToCompareExpResult.Data)
                {
                    if (macroRunResult.Data?.ToLower() == item.ToLower()) { return Response<bool>.EvalToTrue(true); }
                }
                return Response<bool>.EvalToFalse(criteriaMatch);
            }

            return Response<bool>.Failure();
        }
    }

    public interface IExpressionParser<T>
    {
        public IResponse<T> Interpret(string expression);
    }

    internal class AuxiliaryVerbParser : IExpressionParser<string>
    {
        const string pattern = @"( is | is not )";

        public AuxiliaryVerbParser()
        {
        }

        public IResponse<string> Interpret(string expression)
        {
            var resultMacro = RegexHelper.Match(pattern, expression);
            if (resultMacro.Success)
            {
                return new Response<string>(ResponseCode.Success, resultMacro.Value.Trim());
            }
            return new Response<string>(ResponseCode.UnknownError);
        }
    }
    class TasksMacroParamsExpressionParser : IExpressionParser<string[]>
    {
        public IResponse<string[]> Interpret(string expression)
        {
            if(string.IsNullOrEmpty(expression)) return Response<string[]>.Set(ResponseCode.InputNullOrEmptyOrInvalid);

            var commaSeparatedTaskNamesUnParsed = expression.Split(',');
            List<string> taskNameList = new List<string>();
            Array.ForEach(commaSeparatedTaskNamesUnParsed, item => {
                var taskNameVal = item.Split("Task:");
                if (taskNameVal != null && taskNameVal.Length == 2)
                {
                    taskNameList.Add(taskNameVal[1].Trim(new char[] { '}', ']' }));
                }
            });
            
            return new Response<string[]>(ResponseCode.Success, taskNameList.ToArray());
        }
    }
    class MacroParamsExpressionParser : IExpressionParser<Dictionary<string, string>>
    {
        public IResponse<Dictionary<string, string>> Interpret(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return Response<Dictionary<string, string>>.Set(ResponseCode.InputNullOrEmptyOrInvalid);

            var macroParams = new Dictionary<string, string>();
            for (int index = 0; index < expression.Length;)
            {
                var character = expression[index];
                if (StringHelper.IsCurlyBrace(character))
                {
                    // get token param
                    var macroTokenParam = StringHelper.GetValueBetweenBrackets(expression, index);
                    if (string.IsNullOrEmpty(macroTokenParam)) { index++; continue; }

                    index += macroTokenParam.Length + 2;
                    var nameValuePair = StringHelper.SplitOnFirstOccurence(macroTokenParam, ':');
                    macroParams.Add(nameValuePair.Name, nameValuePair.Value);
                }
                else if (StringHelper.IsComma(character))
                {
                    int increment = 0;
                    var macroParam = StringHelper.GetValueBetweenCommas(expression, index);
                    if (!string.IsNullOrEmpty(macroParam)) increment = 2;

                    if (string.IsNullOrEmpty(macroParam))
                    {
                        increment = 1;
                        macroParam = expression.Substring(index + 1, expression.Length - index - 1);
                    }

                    if (string.IsNullOrEmpty(macroParam)) { index++; continue; }

                    // recursive call to get the macro param
                    var response = Interpret(macroParam);
                    if (response.IsSuccess())
                    {

                        index += macroParam.Length + increment;
                        macroParams = CollectionHelper.Copy(macroParams, response.Data);
                    }
                }
                else if (StringHelper.IsSpace(character)) { index++; }
                else
                {
                    var macroNameParam = StringHelper.GetValueBetweenCommas(',' + expression, index);
                    if (string.IsNullOrEmpty(macroNameParam)) { index++; continue; }

                    index += macroNameParam.Length + 1;
                    macroParams.Add("MacroName", macroNameParam);
                }
            }

            return Response<Dictionary<string, string>>.Success(macroParams);
        }
    }
    class MacroExpressionParser : IExpressionParser<Macro>
    {
        //const string patternMacro = @"MACRO\(([^\)]*)\)";
        //const string patternMacroInputs = @"(?<=\().+?(?=\))";
        public IResponse<Macro> Interpret(string expression)
        {
            var resultMacro = StringHelper.GetValueBetweenBrackets(expression, expression.IndexOf("MACRO") + "MACRO".Length, '(', ')');
            if (string.IsNullOrEmpty(resultMacro)) return Response<Macro>.Set(ResponseCode.MacroNotFound);
            
            var resultMacroParams = new MacroParamsExpressionParser().Interpret(resultMacro);
            if (!resultMacroParams.IsSuccess()) return Response<Macro>.Set(ResponseCode.MacroInterpretError);

            var macroParams = resultMacroParams.Data;

            if (macroParams == null || macroParams.Count == 0)
                return new Response<Macro>(ResponseCode.MacroInterpretError);

            var macro = new Macro(macroParams["MacroName"], $"MACRO({resultMacro})");
            macroParams.ToList().ForEach(item =>
            {
                macro.AddParam(item.Key, item.Value);
            });

            var tasksString = CollectionHelper.TryGetValue(macroParams, "Tasks");
            var taskNamesResponse = new TasksMacroParamsExpressionParser().Interpret(tasksString);
            // Tasks param may not always be present
            if( !taskNamesResponse.IsSuccess() && taskNamesResponse.ResponseCode != ResponseCode.InputNullOrEmptyOrInvalid) 
                return Response<Macro>.Set(ResponseCode.MacroInterpretError);
            var taskNames = taskNamesResponse.Data;

            macro.AddParam("StartTask", CollectionHelper.TryGetValue(macroParams, "StartTask"));
            macro.AddParam("EndTask", CollectionHelper.TryGetValue(macroParams, "EndTask"));
            macro.AddParam("Column", CollectionHelper.TryGetValue(macroParams, "Column"));
            macro.AddParam("Key", CollectionHelper.TryGetValue(macroParams, "Key"));
            macro.AddParam("Task", CollectionHelper.TryGetValue(macroParams, "Task"));
            macro.AddParam("Tasks", taskNames);
            return new Response<Macro>(ResponseCode.Success, macro);
        }
    }

    // only supports 'or' in the criteria
    public class ValuesToCompareExpressionParser : IExpressionParser<string[]>
    {
        public IResponse<string[]> Interpret(string expression)
        {
            try
            {
                // tbd - use regex to match
                string[] splitValues = expression.Split(" or ");
                if (splitValues == null || splitValues.Length == 0)
                    return new Response<string[]>(ResponseCode.Failure);

                // trim the prefix of the first value. It will come with the entire expression
                var firstValue = splitValues[0];
                firstValue = firstValue.Trim();
                int lastIndexOfSpace = firstValue.LastIndexOf(" ");
                firstValue = firstValue.Substring(lastIndexOfSpace, firstValue.Length - lastIndexOfSpace);
                splitValues[0] = firstValue.Trim();
                return new Response<string[]>(ResponseCode.Success, splitValues);
            }
            catch (Exception ex)
            {
                return new Response<string[]>(ResponseCode.ExceptionThrown, ex);
            }
        }
    }
}
