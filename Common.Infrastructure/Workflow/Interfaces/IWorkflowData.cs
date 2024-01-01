namespace Common.Infrastructure.Interfaces
{
    public interface IWorkflowData<T>
    {
        string WorkflowKey { get; set; }
        T Data { get; set; }
        //public Dictionary<string, string> ProcessAttributes { get; set; }
        //public Dictionary<string, string> FormAttributes { get; set; }
        //public Dictionary<string, Attachment> Attachments { get; set; }
        //public Dictionary<string,string> SubFormData { get; set; }
    }

}
