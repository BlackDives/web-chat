using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebChat.Infrastructure.DataAccess.Entities;

[Table("users")]
public class ApplicationUser : IdentityUser<Guid>
{
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("enable_notifications")]
    public bool EnableNotifications { get; set; }
    
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }
    
    public ICollection<Role> Role { get; set; }
}