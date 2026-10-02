namespace WebChat.Shared.Models.Friendships;

public class FriendshipToCreate
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
}