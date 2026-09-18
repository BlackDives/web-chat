using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChat.Shared.Enums;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("spaces")]
public class SpaceEntity
{
    [Column("id")]
    public Guid Id {get; set;}
    
    [Required]
    [Column("space_name")]
    public string Name {get; set;}
    
    [Column("space_owner_id")]
    public Guid SpaceOwnerId {get; set;}
    
    [Column("space_type")]
    public SpaceTypeEnum  SpaceType {get; set;}
    
    [Column("created_at")]
    public DateTimeOffset CreatedAt {get; set;}
    
    [Column("updated_at")]
    public DateTimeOffset UpdatedAt {get; set;}
    
    public ICollection<ChannelEntity> Channels {get; set;}
}