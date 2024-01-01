namespace Common.Infrastructure.Enums
{
    public enum WorkflowTaskStatus
    {
        None = 0,
        New = 1,
        Assigned = 2,
        Complete = 3,
        Cancelled = 4,
        Overdue = 5,
        Reassigned = 6,
        Removed = 7
    }
}
