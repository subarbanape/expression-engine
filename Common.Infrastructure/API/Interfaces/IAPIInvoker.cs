using Common.Infrastructure.Models;

namespace Common.Infrastructure.Interfaces
{
    public interface IAPIInvoker
    {
        APIResponse<T> Invoke<T>(IAPI<T> apiName);
    }
}
