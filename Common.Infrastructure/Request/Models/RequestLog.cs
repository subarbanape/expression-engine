using System;

namespace Common.Infrastructure.Models
{
    public class RequestLog
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string Event { get; set; }
        public DateTime? EventDate { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
