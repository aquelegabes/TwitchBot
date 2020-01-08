using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBot.Data.Context;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="Command"/>.
    /// </summary>
    public class RepositoryCommand : RepositoryBase<Command>, IRepositoryCommand
    {
        /// <summary>
        /// Initiate a new instance of <see cref="RepositoryCommand"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="TwitchBotContext"/> context.</param>    
        public RepositoryCommand(TwitchBotContext context) : base (context) { }
    }
}
