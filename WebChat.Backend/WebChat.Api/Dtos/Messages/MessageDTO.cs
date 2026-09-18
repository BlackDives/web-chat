namespace web_api.Dtos.Messages;

public class MessageDTO
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    
    public string SenderUsername { get; set; }
    public string ChannelId { get; set; }
    public string Text { get; set; }
    public DateTime? Created { get; set; }
}