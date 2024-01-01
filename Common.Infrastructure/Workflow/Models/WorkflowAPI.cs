using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public abstract class WorkflowAPI<T> : IWorkflowAPI<T>
    {
        public WorkflowAPI() { this.Name = "Workflow"; }
        public WorkflowAPI(string Name) : this() { this.Name += Constants.Symbols.Forwardslash + Name; }
        public string Name { get; }

        public abstract IAPIParams APIParams { get; set; }

        public abstract APIResponse<T> APIResponse { get; }
    }
}
