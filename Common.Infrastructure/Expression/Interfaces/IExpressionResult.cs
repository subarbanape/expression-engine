using Common.Infrastructure.Models;

namespace Common.Infrastructure.Interfaces
{
    public interface IExpressionResult<T>
    {
        public ExpressionResultType EvalResult { get; }
        public T Result { get; }
        public bool Success { get; }

        public string Message { get; }
    }

    public interface IExpressionResult : IExpressionResult<string>
    {
    }
}