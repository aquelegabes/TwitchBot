using TwitchBot.Data.Context;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="Command"/>.
    /// </summary>
    public class RepositoryChannel : RepositoryBase<Channel>, IRepositoryChannel
    {
        /// <summary>
        /// Initiate a new instance of <see cref="RepositoryCommand"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="TwitchBotContext"/> context.</param>    
        public RepositoryChannel(TwitchBotContext context) : base(context) { }
    }
}

