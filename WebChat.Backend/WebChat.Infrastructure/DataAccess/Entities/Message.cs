using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("messages")]
public class Message
{
    [Column("message_id")]
    public Guid Id { get; set; }
    
    [Column("message_text")]
    public string MessageText { get; set; }
    
    [Column("channel_id")]
    public Guid ChannelId { get; set; }
    public ChannelEntity ChannelEntity { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}