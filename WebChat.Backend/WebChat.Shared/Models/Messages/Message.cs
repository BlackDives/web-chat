namespace WebChat.Shared.Models.Messages;

public class Message
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string TextContent { get; set; }
    public List<string?>  Attachments { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset EditedAt { get; set; }
}