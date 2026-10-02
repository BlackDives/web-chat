namespace WebChat.Shared.Models.Spaces;

public class SpaceToAdd
{
    public Guid Creator {  get; set; }
    public string Name { get; set; }
    public string? ServerIcon { get; set; }
}