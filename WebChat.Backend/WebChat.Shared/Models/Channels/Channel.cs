namespace WebChat.Shared.Models.Channels;

public class Channel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SpaceId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}