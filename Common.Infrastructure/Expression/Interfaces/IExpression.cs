using Newtonsoft.Json;

namespace Common.Infrastructure.Interfaces
{
    public interface IExpression
    {
        IResponse<string> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams);
        IResponse<string> ExpressionResult { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        IExpression Successor { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        IResponse<string> PredecessorResult { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        string TargetExpression { get; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        ICriteria ExpCriteria { get; set; }
    }
}