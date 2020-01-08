using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Data.Configuration
{
    /// <summary>
    /// Class responsible for configurating <see cref="Command"/> entity model.
    /// </summary>
    public class CommandConfiguration : AEntityConfiguration<Command>
    {
        public override void Configure(EntityTypeBuilder<Command> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Action).IsRequired();
            builder.Property(p => p.TypeCommand).IsRequired();
            builder.Property(p => p.Name);
            builder.Property(p => p.PublicResponse).HasDefaultValue(false);

            builder.HasOne(p => p.CreatedBy)
                .WithMany(m => m.Commands)
                .IsRequired()
                .HasForeignKey(fk => fk.Id);

            builder.HasKey(key => key.Id);
        }
    }
}
