using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("user_space_mapping")]
public class UserSpaceMemberEntity
{
    [Column("user_id")]
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    [Column("space_id")]
    public Guid SpaceId { get; set; }
    public SpaceEntity Space { get; set; }
    
    [Column("member_since")]
    public DateTimeOffset MemberSince { get; set; }
}