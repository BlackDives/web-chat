namespace WebChat.Shared.Models.Spaces;

public class Space
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ServerIcon { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}