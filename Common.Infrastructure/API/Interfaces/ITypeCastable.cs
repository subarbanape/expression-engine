namespace Common.Infrastructure.Interfaces
{
    public interface ITypeCastable
    {
        T Cast<T>(object obj);
    }
}
