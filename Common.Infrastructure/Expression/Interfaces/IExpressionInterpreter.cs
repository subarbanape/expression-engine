namespace Common.Infrastructure.Interfaces
{
    public interface IExpressionInterpreter
    {
        IResponse<string> Interpret(IExpression expression, IActionParams actionParams);
        IResponse<bool> Interpret(ICriteria criteria, IActionParams actionParams);
    }
}