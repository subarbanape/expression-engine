namespace Common.Infrastructure.Interfaces
{
    public interface ICredentials
    {
        string Domain { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }
}