using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Data.Configuration
{
    /// <summary>
    /// Class responsible for configurating <see cref="Channel"/> entity model.
    /// </summary>
    public class ChannelConfiguration : AEntityConfiguration<Channel>
    {
        public override void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired();
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.HasMany(m => m.CreatedCommands)
                .WithOne(o => o.CreatedBy)
                .HasForeignKey(fk => fk.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
