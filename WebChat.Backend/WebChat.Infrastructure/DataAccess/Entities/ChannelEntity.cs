using System.ComponentModel.DataAnnotations.Schema;
using WebChat.Shared.Enums;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("channels")]
public class ChannelEntity
{
    [Column("channel_id")]
    public Guid Id { get; set; }
    
    [Column("channel_space_id")]
    public Guid SpaceId { get; set; }
    public SpaceEntity SpaceEntity { get; set; }
    
    [Column("channel_name")]
    public string Name { get; set; }
    
    [Column("channel_type")]
    public ChannelTypeEnum Type { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}