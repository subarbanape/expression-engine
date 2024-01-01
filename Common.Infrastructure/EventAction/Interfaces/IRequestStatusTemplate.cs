using Newtonsoft.Json;

namespace Common.Infrastructure.Interfaces
{
    public interface IRequestStatusTemplate
    {
        string[] TaskNames { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        IExpression Expression { get; set; }
        int PercentageComplete { get; set; }
    }
}
