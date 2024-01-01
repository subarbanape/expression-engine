using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public class RequestStatusTemplate : IRequestStatusTemplate
    {
        public string[] TaskNames { get; set; }
        public IExpression Expression { get; set; }
        public int PercentageComplete { get; set; }
    }
}
