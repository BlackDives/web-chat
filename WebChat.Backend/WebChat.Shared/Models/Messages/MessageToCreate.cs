namespace WebChat.Shared.Models.Messages;

public class MessageToCreate
{
    public Guid SenderId { get; set; }
    public string TextContent { get; set; }
    public List<string?> Attachments { get; set; }
}