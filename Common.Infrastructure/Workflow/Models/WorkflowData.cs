using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public class WorkflowData<T> : IWorkflowData<T>
    {
        public string WorkflowKey { get; set; }
        public T Data { get; set; }
        //public Dictionary<string, string> ProcessAttributes { get; set; }
        //public Dictionary<string, string> FormAttributes { get; set; }
        //public Dictionary<string,Attachment> Attachments { get; set; }
        //public Dictionary<string,string> SubFormData { get; set; }
    }
}
