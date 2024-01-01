using Common.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace ExpressionEngine
{
    public class MultiCriteria
    {
        public List<ICriteria> CriteriaList { get; set; }

        public ICriteriaResult ExpressionResult { get; private set; }

        public string TargetCriteria { get; }

        IExpressionInterpreter expressionInterpreter;

        public ICriteriaResult Evaluate()
        {
            ICriteriaResult result = null;
            ExpressionResult = result;
            return result;
        }
    }
}
