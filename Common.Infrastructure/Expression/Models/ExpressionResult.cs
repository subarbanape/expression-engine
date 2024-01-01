using Common.Infrastructure.Interfaces;
using System;

namespace Common.Infrastructure.Models
{
    public class ExpressionResult<T> : IExpressionResult<T>
    {
        public ExpressionResult() { }
        public ExpressionResult(ExpressionResultType expressionResultType, T result)
        {
            this.EvalResult = expressionResultType; Result = result;
            SetSuccess();
        }
        public ExpressionResult(ExpressionResultType expressionResultType)
        {
            this.EvalResult = expressionResultType;
            SetSuccess();
        }

        public ExpressionResult(ExpressionResultType expressionResultType, Exception ex)
        {
            this.EvalResult = expressionResultType;
            SetSuccess();
            Message = ex.Message.ToString();
        }

        public void SetSuccess()
        {
            Success = (EvalResult == ExpressionResultType.EvaluatedTrue || EvalResult == ExpressionResultType.Success);
        }

        public T Result { get; }

        public ExpressionResultType EvalResult { get; }

        public bool Success { get; private set; }

        public string Message { get; }

    }

    public class ExpressionResult : ExpressionResult<string>
    {
        public ExpressionResult(ExpressionResultType expressionResultType) : base(expressionResultType) { }

    }
}
