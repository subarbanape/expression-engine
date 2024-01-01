using Common.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System.Linq;

namespace Common.Infrastructure.Models
{
    public class GenericCommand : ICommand
    {
        public GenericCommand() { }
        public GenericCommand(GenericCommandAction[] actions, string command)
        {
            Actions = actions?.ToList().ConvertAll(action => (IAction)action).ToArray();
            Command = command;
        }

        public string Command { get; set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IAction[] Actions { get; set; }
    }

    public class CancelTasksIfAny : GenericCommand
    {

    }

    public class CompleteTasksIfAny : GenericCommand
    {

    }
}
