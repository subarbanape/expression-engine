using Common.Infrastructure.Models;

namespace Common.Infrastructure.Interfaces
{
    public interface IAPI<T>
    {
        string Name { get; }
        IAPIParams APIParams { get; set; }
        APIResponse<T> APIResponse { get; }
    }

    public interface IAPI : IAPI<string>
    { }
}
