namespace web_api.ServiceMessages.Server;

public class ServerServiceErrorMessages
{
    public string Value { get; private set; }

    private ServerServiceErrorMessages(string value)
    {
        Value = value;
    }
    
    public static ServerServiceErrorMessages UserNotFound => new ServerServiceErrorMessages("User not found");
    public static ServerServiceErrorMessages UserAlreadyServerMember => new ServerServiceErrorMessages("User already server member");
    public static ServerServiceErrorMessages UserNotServerMember => new ServerServiceErrorMessages("User is not a server member");
}