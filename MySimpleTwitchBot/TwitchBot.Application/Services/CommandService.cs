using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Application.Interfaces;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Application.Services
{
    /// <summary>
    /// Service responsible for handling <see cref="Command"/> related logic.
    /// </summary>
    public sealed class CommandService : BaseService<Command>, ICommandService
    {
        /// <summary>
        /// Getting repository through constructor and DI.
        /// </summary>
        /// <param name="repository"></param>
        public CommandService(IRepositoryCommand repository) : base (repository) { }
    }
}
