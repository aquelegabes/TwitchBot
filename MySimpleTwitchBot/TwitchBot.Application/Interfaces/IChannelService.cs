using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Application.Interfaces
{
    /// <summary>
    /// Interface responsible for handling <see cref="Channel"/> related logic.
    /// </summary>
    public interface IChannelService : IBaseService<Channel>
    {
    }
}
