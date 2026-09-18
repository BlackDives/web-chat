using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasColumnName("normalized_name");
        
        builder.Ignore(r => r.ConcurrencyStamp);
    }
}