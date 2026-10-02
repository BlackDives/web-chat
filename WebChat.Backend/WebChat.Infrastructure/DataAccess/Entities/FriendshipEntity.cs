using System.ComponentModel.DataAnnotations.Schema;
using WebChat.Shared.Enums;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("friendship")]
public class FriendshipEntity
{   
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("sender_id")]
    public Guid SenderId { get; set; }
    public ApplicationUser Sender { get; set; }
    
    [Column("receiver_id")]
    public Guid ReceiverId { get; set; }
    public ApplicationUser Receiver { get; set; }
    
    [Column("status")]
    public FriendshipStatusEnum FriendshipStatus { get; set; }
    
    [Column("received_on")]
    public DateTimeOffset ReceivedOn { get; set; }
    
    [Column("friends_on")]
    public DateTimeOffset? FriendsOn { get; set; }
    
    [Column("declined_on")]
    public DateTimeOffset? DeclinedOn { get; set; }
    
    [Column("blocked_on")]
    public DateTimeOffset? BlockedOn { get; set; }
}