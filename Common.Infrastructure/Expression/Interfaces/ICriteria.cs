using Newtonsoft.Json;

namespace Common.Infrastructure.Interfaces
{
    public interface ICriteria
    {
        [JsonProperty("_TargetCriteria")]
        public string TargetCriteria { get; set; }
        IResponse<bool> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams);
    }
}