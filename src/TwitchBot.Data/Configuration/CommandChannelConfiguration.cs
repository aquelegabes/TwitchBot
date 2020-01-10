using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Data.Configuration
{
    public class CommandChannelConfiguration : AEntityConfiguration<CommandChannel>
    {
        public override void Configure(EntityTypeBuilder<CommandChannel> builder)
        {
            base.Configure(builder);
            builder.HasKey(t => new { t.ChannelId, t.CommandId });

            builder.HasOne(c => c.Command)
                .WithMany(m => m.CommandsInChannel)
                .HasForeignKey(fk => fk.CommandId);

            builder.HasOne(c => c.Channel)
                .WithMany(m => m.CommandsInChannel)
                .HasForeignKey(fk => fk.ChannelId);
        }
    }
}
