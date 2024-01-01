using Common.Infrastructure.Models;
using System;

namespace Common.Infrastructure.Interfaces
{
    public interface IWorkflowStatEvent : IEvent
    {
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public RequestAndTaskLogAction[] Actions { get; set; }
        public ICommand[] PostExecuteCommands { get; set; }
        public string WorkflowId { get; set; }
    }
}