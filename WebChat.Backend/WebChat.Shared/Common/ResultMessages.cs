namespace WebChat.Shared.Common;

public record ResultMessage
{
    public string Message { get; }
    
    private ResultMessage(string message)
    {
        Message = message;
    }
    
    public static ResultMessage UserNotFound = new("User not found");
    
    public static implicit operator string(ResultMessage result) => result.Message;
}