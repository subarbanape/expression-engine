using Common.Infrastructure.Enums;
using Common.Infrastructure.Interfaces;
using System;

namespace Common.Infrastructure.Models
{
    public class Response<T> : IResponse<T>
    {
        public Response() { ResponseCode = ResponseCode.Success; }
        public Response(ResponseCode responseCode, T data) { ResponseCode = responseCode; Data = data; }
        public Response(ResponseCode responseCode, Exception exception) { ResponseCode = responseCode; Exception = exception; }
        public Response(ResponseCode responseCode) { ResponseCode = responseCode; }

        public T Data { get; set; }

        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public bool IsSuccess()
        {
            return (ResponseCode == ResponseCode.Success ||
                      ResponseCode == ResponseCode.EvaluatedTrue);
        }
        public bool IsCriticalError()
        {
            return (ResponseCode == ResponseCode.ExceptionThrown ||
                      ResponseCode == ResponseCode.Failure ||
                      ResponseCode == ResponseCode.MacroInterpretError ||
                      ResponseCode == ResponseCode.UnknownError);
        }

        public static Response<T> Success(T result) { return new Response<T>() { Data = result, ResponseCode = ResponseCode.Success }; }

        public static Response<T> ExceptionThrown(Exception ex)
        {
            return new Response<T>() { ResponseCode = ResponseCode.ExceptionThrown, Exception = ex };
        }

        public static Response<T> Success() { return new Response<T>() { ResponseCode = ResponseCode.Success }; }
        public static Response<T> Failure(Exception ex) { return new Response<T>() { Exception = ex, ResponseCode = ResponseCode.Failure }; }
        public static Response<T> UnknownError(Exception ex) { return new Response<T>() { Exception = ex, ResponseCode = ResponseCode.UnknownError }; }
        public static Response<T> Failure() { return new Response<T>() { ResponseCode = ResponseCode.Failure }; }
        public static Response<T> UnknownError() { return new Response<T>() { ResponseCode = ResponseCode.UnknownError }; }

        public Response<T1> GetAsError<T1>()
        {
            return new Response<T1>() { 
                ResponseCode = this.ResponseCode,
                Exception = this.Exception,
                Message = this.Message
            };
        }

        public static Response<T> EvalToFalse(T result) { return new Response<T>() { ResponseCode = ResponseCode.EvaluatedFalse, Data = result }; }
        public static Response<T> EvalToTrue(T result) { return new Response<T>() { ResponseCode = ResponseCode.EvaluatedTrue, Data = result }; }
        public static Response<T> Set(ResponseCode responseCode) { return new Response<T>() { ResponseCode = responseCode }; }
        public static Response<T> Set(ResponseCode responseCode, T data) { return new Response<T>() { ResponseCode = responseCode, Data = data }; }
        public Response<string> ConvertDataToString()
        {
            return new Response<string>() { 
                Message = this.Message, 
                Exception = this.Exception, 
                ResponseCode = this.ResponseCode,
                Data = this.Data?.ToString(),
            };
        }
        public override string ToString()
        {
            return
                (string.IsNullOrEmpty(Message)? string.Empty : $"Message: {Message}\n") +
                (Exception == null ? string.Empty : $"Exception: {Exception.ToString()}\n") +
                ($"ResponseCode: {ResponseCode.ToString()}\n") +
                (Data == null ? string.Empty : $"Data: {TryGetDataAsString()}");
        }

        public string TryGetDataAsString()
        {
            if (Data == null) return string.Empty;

            if (Data.GetType() == typeof(string[]))
            {
                var dataAsStringArray = GetData<string[]>();
                return string.Join(",", dataAsStringArray);
            }
            else return Data.ToString();
        }

        public T1 GetData<T1>()
        {
            if (Data != null && Data.GetType() == typeof(T1)) return (T1)Convert.ChangeType(Data, typeof(T1));
            else return default(T1);
        }
    }
}
