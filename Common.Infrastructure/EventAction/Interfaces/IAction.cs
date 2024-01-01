using Common.Infrastructure.Models;
using Newtonsoft.Json;

namespace Common.Infrastructure.Interfaces
{
    public interface IAction
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        IActionParams Params { get; set; }
        Response<string> Response { get; set; }
        string Name { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        IExpression Expression { get; set; }
    }
}
