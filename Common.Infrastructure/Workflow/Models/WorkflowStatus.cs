namespace Common.Infrastructure.Enums
{
    public enum WorkflowStatus
    {
        None = -1,
        Unknown = 0,
        Active = 1,
        Complete = 2,
        Cancelled = 3,
        Queued = 4,
        Suspended = 5,
    }
}
