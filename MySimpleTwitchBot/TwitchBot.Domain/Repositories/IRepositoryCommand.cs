using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Domain.Repositories
{
    /// <summary>
    /// Interface to serve as a repository for <see cref="Command"/> database model.
    /// </summary>
    public interface IRepositoryCommand : IRepositoryBase<Command> 
    {
    }
}
