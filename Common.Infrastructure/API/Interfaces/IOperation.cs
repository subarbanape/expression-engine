namespace Common.Infrastructure.Interfaces
{
    public interface IOperable<T>
    {
        IResponse<T> Do(IServerInfo serverInfo, ICredentials credentials);
    }
}