using Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.BotUserId);

            builder.HasIndex(u => u.BotUserId).IsUnique();

            builder.HasIndex(u => u.TelegramUserId).IsUnique();

            builder.HasOne(bu => bu.UserInfo)
                .WithOne(ui => ui.BotUsers)
                .HasForeignKey<UserInfo>(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
