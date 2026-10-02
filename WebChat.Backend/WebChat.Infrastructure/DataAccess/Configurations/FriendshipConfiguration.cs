using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<FriendshipEntity>
{
    public void Configure(EntityTypeBuilder<FriendshipEntity> builder)
    {
        builder.ToTable("friendship");
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Receiver)
            .WithMany()
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
    }
}