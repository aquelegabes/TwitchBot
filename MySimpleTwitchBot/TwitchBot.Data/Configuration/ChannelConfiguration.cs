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
            base.Configure(builder);
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.Commands)
                .WithOne(o => o.CreatedBy)
                .HasForeignKey(fk => fk.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(key => key.Id);
        }
    }
}
