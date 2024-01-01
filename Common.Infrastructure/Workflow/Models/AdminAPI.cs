using Common.Infrastructure.Interfaces;

namespace Common.Infrastructure.Models
{
    public abstract class AdminAPI<T> : IWorkflowAdminAPI<T>
    {
        public AdminAPI() { this.Name = "Admin"; }
        public AdminAPI(string Name) : this() { this.Name += Constants.Symbols.Forwardslash + Name; }
        public string Name { get; }

        public abstract IAPIParams APIParams { get; set; }

        public abstract APIResponse<T> APIResponse { get; }
    }
}
