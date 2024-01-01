using Newtonsoft.Json;

namespace Common.Infrastructure.Interfaces
{
    public interface ICommand
    {
        public string Command { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public IAction[] Actions { get; set; }
    }
}
