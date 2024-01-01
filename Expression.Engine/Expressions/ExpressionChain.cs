using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;

namespace ExpressionEngine
{
    public class ExpressionChain : IExpression
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
                Successor.PredecessorResult = null;
                Successor.Evaluate(expressionInterpreter, actionParams);
            }

            // Walk through nested expressions and get the right results.
            var result = ExpressionResult;
            var successor = Successor;
            while (successor != null && successor.ExpressionResult != null)
            {
                if (successor.GetType() != typeof(AllCondition)
                    && successor.GetType() != typeof(AnyCondition)
                    && successor.GetType() != typeof(ExpressionChain))
                {
                    result = successor.ExpressionResult;
                    if (result.ResponseCode == ResponseCode.EvaluatedTrue &&
                       (successor.Successor != null && successor.Successor.GetType() == typeof(AnyCondition)))
                        break;
                }
                successor = successor.Successor;
            }

            return result;
        }
    }
}
