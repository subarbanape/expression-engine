using Common.Infrastructure.Models;

namespace Common.Infrastructure.Interfaces
{
    public interface IWorkflowEvent : IEvent
    {
        public Workflow Workflow { get; }
        public WorkflowTask[] WorkflowTasks { get; }
    }
}
