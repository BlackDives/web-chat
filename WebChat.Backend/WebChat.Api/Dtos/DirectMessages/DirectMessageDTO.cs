namespace web_api.Dtos.DirectMessages;

public class DirectMessageDTO
{
    public string MessageId { get; set; }
    public string DirectMessageChannelId { get; set; }
    public string SenderId  { get; set; }
    public string SenderUsername { get; set; }
    public string MessageContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}