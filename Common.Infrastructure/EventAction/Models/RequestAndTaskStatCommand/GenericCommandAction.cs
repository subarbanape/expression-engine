using Common.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Common.Infrastructure.Models
{
    public class GenericCommandAction : IAction
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IExpression Expression { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IActionParams Params { get; set; }
        public Response<string> Response { get; set; }
        public string Name { get; set; }
    }
}
