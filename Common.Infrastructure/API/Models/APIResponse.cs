namespace Common.Infrastructure.Models
{
    public class APIResponse<T> : Response<T>
    {
        public new static APIResponse<T> Success(T result)
        { return new APIResponse<T>() { Data = result, ResponseCode = Enums.ResponseCode.Success }; }
    }
}
