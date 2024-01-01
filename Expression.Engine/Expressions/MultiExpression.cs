using Common.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace ExpressionEngine
{
    public class MultiExpression
    {
        public List<IExpression> Expressions { get; set; }

        public IExpressionResult ExpressionResult { get; set; }

        public IExpression Successor { get; set; }

        public IExpression Predecessor { get; set; }

        public IExpression Expression { get; }

        public IExpressionResult Evaluate()
        {
            IExpressionResult result = null;
            ExpressionResult = result;
            return result;
        }
    }
}
