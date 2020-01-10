using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Data.Context;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="TwitchBadge"/>.
    /// </summary>
    public class RepositoryTwitchBadge : RepositoryBase<TwitchBadge>, IRepositoryTwitchBadge
    {
        /// <summary>
        /// Initiate a new instance of <see cref="RepositoryTwitchBadge"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="TwitchBotContext"/> context.</param> 
        public RepositoryTwitchBadge(TwitchBotContext context) : base (context) { }
    }
}
