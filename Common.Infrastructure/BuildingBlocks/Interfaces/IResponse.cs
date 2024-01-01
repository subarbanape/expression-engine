using Common.Infrastructure.Enums;
using System;

namespace Common.Infrastructure.Interfaces
{
    public interface IResponse<T>
    {
        public T Data { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public bool IsSuccess();
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}