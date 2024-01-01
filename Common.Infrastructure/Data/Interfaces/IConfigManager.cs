using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Data.Interfaces
{
    public interface IConfigManager
    {
        IRequestStatusTemplate[] GetRequestStatusTemplate(string[] activeTasks);
    }
}
