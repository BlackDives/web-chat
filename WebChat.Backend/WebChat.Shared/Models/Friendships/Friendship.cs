using WebChat.Shared.Enums;
using WebChat.Shared.Models.Users;

namespace WebChat.Shared.Models.Friendships;

public class Friendship
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; }
    public FriendshipStatusEnum FriendshipStatus { get; set; }
    public DateTimeOffset ReceivedOn { get; set; }
}