using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Infrastructure.Enums
{
    public enum ResponseCode
    {
        UnknownError = -2,
        None = -1,
        Failure = 0,
        Success = 1,
        MacroNotFound = 2,
        RecordNotFound = 3,
        ExceptionThrown = 4,
        TaskNotFound = 5,
        EvaluatedTrue = 6,
        EvaluatedFalse = 7,
        WorkflowNotFound = 8,
        ProcessComplete = 9,
        MacroInterpretError = 10,
        InputNullOrEmptyOrInvalid = 11,
        ActionNotFound = 12,
        ExpressionError = 13,
        UniqueKeyViolation = 14,
    }
}
