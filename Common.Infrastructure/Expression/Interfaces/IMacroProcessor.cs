using Common.Infrastructure.Expression.Interfaces;

namespace Common.Infrastructure.Interfaces
{
    public interface IMacroProcessor
    {
        IResponse<string> Run(IMacro macro, IExpressionInterpreter expressionInterpreter, IActionParams actionParams);
    }
}
