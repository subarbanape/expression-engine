using Common.Infrastructure.Models;

namespace Common.Infrastructure.Interfaces
{
    public interface IActionHandler
    {
        Response<T> Invoke<T>(IAction action);
    }
}
