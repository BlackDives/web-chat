namespace web_api.ServiceMessages.Channel;

public class ChannelServiceErrorMessage
{
    public string Value { get; private set; }

    private ChannelServiceErrorMessage(string value)
    {
        Value = value;
    }
    
    public static ChannelServiceErrorMessage ChannelNotFound => new ChannelServiceErrorMessage("ChannelEntity not found.");
}