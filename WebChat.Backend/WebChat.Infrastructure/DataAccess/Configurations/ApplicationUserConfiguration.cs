using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");
       
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(u => u.UserName)
            .HasColumnName("username")
            .IsRequired();
        
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number");
        
        
        builder.Ignore(u => u.PasswordHash);
        builder.Ignore(u => u.PhoneNumberConfirmed);
        builder.Ignore(u => u.EmailConfirmed);
    }
}