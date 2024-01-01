using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public class RequestAndTaskLogAction : IAction
    {
        RequestAndTaskLogActionParams @params;

        public IExpression Expression { get; set; }
        public IActionParams Params { get => @params; set => @params = (RequestAndTaskLogActionParams)value; }
        public Response<string> Response { get; set; }

        public string Name { get; set; }
    }
}
