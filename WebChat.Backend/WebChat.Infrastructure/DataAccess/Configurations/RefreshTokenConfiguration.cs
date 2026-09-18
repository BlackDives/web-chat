using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("refresh_tokens");
        
        builder.Property(rt => rt.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(rt => rt.Token)
            .HasColumnName("token")
            .IsRequired();
        
        builder.Property(rt => rt.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(rt => rt.ExpiresOn)
            .HasColumnName("expires_on")
            .IsRequired();
    }
}