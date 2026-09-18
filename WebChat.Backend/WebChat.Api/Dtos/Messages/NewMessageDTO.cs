namespace web_api.Dtos.Messages;

public class NewMessageDTO
{
    public string SenderId { get; set; }
    public string ChannelId { get; set; }
    public string Text { get; set; }
}