using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WebChat.Shared.Enums;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("user_role")]
public class Role : IdentityRole<Guid>
{
    [Key]
    [Column("role_id")]
    public Guid RoleId { get; set; }
    
    [Column("role_name")]
    public UserRoleEnum RoleName { get; set; }
    
    public ICollection<ApplicationUser> Users { get; set; }
}