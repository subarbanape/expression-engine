using Common.Infrastructure.Interfaces;
using Common.Infrastructure.Models;

namespace ExpressionEngine
{
    public class Criteria : ICriteria
    {
        public Criteria() { }
        public Criteria(string c) => TargetCriteria = c;

        //[JsonProperty("_TargetCriteria")]
        public string TargetCriteria { get; set; }

        public IResponse<bool> Evaluate(IExpressionInterpreter expressionInterpreter, IActionParams actionParams)
        {
            if (TargetCriteria == "None") return Response<bool>.Success(true);
            return expressionInterpreter.Interpret(this, actionParams);
        }
    }
}
