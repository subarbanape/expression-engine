using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;

namespace ExpressionEngine
{
    public class AllCondition : IExpression
    {
        public IResponse<string> ExpressionResult { get; set; }
        public IExpression Successor { get; set; }
        public IResponse<string> PredecessorResult { get; set; }
        public ICriteria ExpCriteria { get; set; }
        public string TargetExpression { get; set; }
        public IResponse<string> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            ExpressionResult = new Response<string>(ResponseCode.EvaluatedTrue);
            if (Successor != null)
            {
                Successor.PredecessorResult = ExpressionResult;
                return Successor.Evaluate(expressionInterpreter, actionParams);
            }
            return ExpressionResult;
        }
    }
}