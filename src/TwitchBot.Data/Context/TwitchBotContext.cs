using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TwitchBot.Data.Configuration;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Data.Context
{
    /// <summary>
    /// A context containing existing models.
    /// </summary>
    public class TwitchBotContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitchBotContext"/> class
        /// using the specified options. The <see cref="OnConfiguring(DbContextOptionsBuilder)"/>
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="options">Options for this context.</param>
        public TwitchBotContext(DbContextOptions options) : base(options) { }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Channel> Channels { get; set; }

        /// <summary>
        /// Configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </summary>
        /// <param name="optionsBuilder">
        /// A builder used to create or modify options for this context.
        /// Databases (and other extensions) typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Configure the model that was discovered by convention from the entity types.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        /// <remarks>If a model is explicitly set on the options for this context then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder
                // .HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new CommandConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
        }
    }
}
