using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public class Credentials : ICredentials
    {
        public string Domain { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
