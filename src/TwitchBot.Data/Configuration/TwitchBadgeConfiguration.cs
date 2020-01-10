using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Data.Configuration
{
    /// <summary>
    /// Class responsible for configurating <see cref="TwitchBadge"/> entity model.
    /// </summary>
    public class TwitchBadgeConfiguration : AEntityConfiguration<TwitchBadge>
    {
        public override void Configure(EntityTypeBuilder<TwitchBadge> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.DisplayName).IsRequired();
            builder.Property(p => p.Flag).IsRequired();

            base.Configure(builder);
        }
    }
}
