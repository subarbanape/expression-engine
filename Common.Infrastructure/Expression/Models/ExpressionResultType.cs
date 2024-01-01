namespace Common.Infrastructure.Models
{
    public enum ExpressionResultType
    {
        ExceptionThrown = -3,
        UnknownError = -2,
        None = -1,
        Failure = 0,
        Success = 1,
        EvaluatedTrue = 2,
        EvaluatedFalse = 3,
        CriteriaDidntMatch = 4,
    }
}