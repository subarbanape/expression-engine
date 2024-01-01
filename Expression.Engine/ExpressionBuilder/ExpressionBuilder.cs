
using Common.Infrastructure.Interfaces;

namespace ExpressionEngine
{
    public static class ExpressionBuilder
    {
        public static IExpression CriteriaExpression(this IExpression exp, string expression, string criteria)
        {
            exp.Successor = new CriteriaExpression(expression, criteria);
            return exp.Successor;
        }

        public static IExpression AnyCondition(this IExpression exp)
        {
            exp.Successor = new AnyCondition();
            return exp.Successor;
        }

        public static IExpression AllCondition(this IExpression exp)
        {
            exp.Successor = new AllCondition();
            return exp.Successor;
        }

        public static IExpression ExpressionChain()
        {
            return new ExpressionChain();
        }
    }
}
