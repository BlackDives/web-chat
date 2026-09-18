using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess;

public class WebChatDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
{
    protected readonly IConfiguration _configuration;

    public WebChatDbContext(DbContextOptions<WebChatDbContext> options, IConfiguration configuration)
    : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDbConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoleMapping>().HasKey(urm => new { urm.UserId, urm.RoleId });
        modelBuilder.Entity<UserSpaceMemberEntity>().HasKey(usm => new { usm.UserId, usm.SpaceId });
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
    
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    public DbSet<SpaceEntity> Spaces { get; set; }
    public DbSet<ChannelEntity> Channels { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<FriendshipEntity> Friendships { get; set; }
    public DbSet<FriendshipEntity> Friends { get; set; }
    public DbSet<UserSpaceMemberEntity> UserSpaceMembers { get; set; }
}