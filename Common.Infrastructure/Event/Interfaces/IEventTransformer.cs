namespace Common.Infrastructure.Interfaces
{
    public interface IEventTransformer
    {
        IEvent Transform<T>(T eventData);
    }
}