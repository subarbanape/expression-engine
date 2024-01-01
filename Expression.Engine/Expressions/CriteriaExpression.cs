using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;
using Newtonsoft.Json;

namespace ExpressionEngine
{
    public class CriteriaExpression : IExpression
    {
        public CriteriaExpression(string targetExpression, string criteria) { ExpCriteria = new Criteria(criteria); TargetExpression = targetExpression; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public ICriteria ExpCriteria { get; set; }
        public string TargetExpression { get; set; }
        public IResponse<string> ExpressionResult { get; set; }
        public IExpression Successor { get; set; }
        public IResponse<string> PredecessorResult { get; set; }

        public IResponse<string> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            var criteriaResult = ExpCriteria.Evaluate(expressionInterpreter, actionParams);
            if (!criteriaResult.IsSuccess() &&
                criteriaResult.ResponseCode != ResponseCode.EvaluatedFalse) return ((Response<bool>)criteriaResult).ConvertDataToString();

            ExpressionResult = criteriaResult.IsSuccess() ? expressionInterpreter.Interpret(this, actionParams) : ((Response<bool>)criteriaResult).ConvertDataToString();
            if (Successor != null)
            {
                Successor.PredecessorResult = ExpressionResult;
                return Successor.Evaluate(expressionInterpreter, actionParams);
            }
            return ExpressionResult;
        }
    }
}
