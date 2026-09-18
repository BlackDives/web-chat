using WebChat.Shared.Enums;

namespace WebChat.Shared.Common;

public class Result<T>
{
    public bool Success { get; set; }
    public T Value { get; set; }
    public ResultMessage? ErrorMessage { get; set; }
    public ServiceErrorEnum ErrorType { get; set; }

    private Result(bool success, T value, ResultMessage errorMessage, ServiceErrorEnum errorType)
    {
        Success = success;
        Value = value;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static Result<T> Ok(T value) => new(true, value, null, ServiceErrorEnum.None);
    public static Result<T> Fail(ResultMessage errorMessage, ServiceErrorEnum errorType) => new(false, default(T), errorMessage, errorType);
}