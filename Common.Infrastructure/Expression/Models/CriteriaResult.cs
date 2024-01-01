using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public class CriteriaResult : ICriteriaResult
    {
        public CriteriaResult(bool result) { EvalResult = result; }
        public bool EvalResult { get; set; }
    }
}
