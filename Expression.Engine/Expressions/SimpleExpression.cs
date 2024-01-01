using Common.Infrastructure.Interfaces;

namespace ExpressionEngine
{
    public class SimpleExpression : IExpression
    {
        public SimpleExpression(string targetExpression) { TargetExpression = targetExpression; }
        public string TargetExpression { get; set; }
        public IResponse<string> ExpressionResult { get; set; }
        public IExpression Successor { get; set; }
        public IResponse<string> PredecessorResult { get; set; }
        public ICriteria ExpCriteria { get; set; }

        public IResponse<string> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
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
