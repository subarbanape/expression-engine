using System;

namespace Common.Infrastructure.Models
{
    public partial class TaskStat
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string StatName { get; set; }
        public string TaskName { get; set; }
        public string Stat { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, RequestId: {RequestId}, StatName: {StatName}, TaskName: {TaskName}, Stat: {Stat}, " +
                $"CreatedBy: {CreatedBy}, CreatedDate: {CreatedDate}, ModifiedBy: {ModifiedBy}, ModifiedDate: {ModifiedDate}";
        }
    }
}
