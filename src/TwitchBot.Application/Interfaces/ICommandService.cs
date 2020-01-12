using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Application.Interfaces
{
    /// <summary>
    /// Interface responsible for handling <see cref="Command"/> related logic.
    /// </summary>
    public interface ICommandService : IBaseService<Command>
    {
    }
}
