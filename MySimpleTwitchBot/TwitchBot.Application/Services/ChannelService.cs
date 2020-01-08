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
    public sealed class ChannelService : BaseService<Channel>, IChannelService
    {
        /// <summary>
        /// Getting repository through constructor and DI.
        /// </summary>
        /// <param name="repository"></param>
        public ChannelService(IRepositoryChannel repository) : base (repository) { }
    }
}
