using Common.Infrastructure.Interfaces;

namespace ExpressionEngine
{
    public class BooleanExpression : IExpression
    {
        public string TargetExpressionOnTrue { get; set; }
        public string TargetExpressionOnFalse { get; set; }
        public ICriteria ExpCriteria { get; set; }
        public IResponse<string> ExpressionResult { get; set; }

        public IExpression Successor { get; set; }
        public IResponse<string> PredecessorResult { get; set; }

        public string TargetExpression { get; set; }

        public IResponse<string> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            var criteriaResult = ExpCriteria.Evaluate(expressionInterpreter, actionParams);

            TargetExpression = (criteriaResult.ResponseCode == Common.Infrastructure.Enums.ResponseCode.EvaluatedTrue ?
                TargetExpressionOnTrue : TargetExpressionOnFalse);

            ExpressionResult = expressionInterpreter.Interpret(this, actionParams);

            if (Successor != null)
            {
                Successor.PredecessorResult = ExpressionResult;
                return Successor.Evaluate(expressionInterpreter, actionParams);
            }

            return ExpressionResult;
        }
    }
}
