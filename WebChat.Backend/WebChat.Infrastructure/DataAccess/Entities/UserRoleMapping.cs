using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("user_role_mapping")]
public class UserRoleMapping
{
    [Column("role_id")]
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
}