using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitchBot.Domain.Entities;
using TwitchBot.Services.Models;
using static TwitchBot.Services.Models.TwitchBadges;

namespace TwitchBot.Data.Configuration
{
    /// <summary>
    /// Class responsible for configurating <see cref="Command"/> entity model.
    /// </summary>
    public class CommandConfiguration : AEntityConfiguration<Command>
    {
        public override void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.Property(p => p.Action).IsRequired();
            builder.Property(p => p.TypeCommand).IsRequired();
            builder.Property(p => p.Name);
            builder.Property(p => p.PublicResponse).HasDefaultValue(false);
            builder.Property(p => p.IsSpecialCommand).HasDefaultValue(false);
            builder.Property(p => p.Operators).HasDefaultValue(Badges.General);

            builder.HasOne(p => p.CreatedBy)
                .WithMany(m => m.Commands)
                .IsRequired()
                .HasForeignKey(fk => fk.Id);

            base.Configure(builder);
        }
    }
}
