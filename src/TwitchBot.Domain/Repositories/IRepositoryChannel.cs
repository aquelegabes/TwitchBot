using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Entities;

namespace TwitchBot.Domain.Repositories
{
    /// <summary>
    /// Interface to serve as a repository for <see cref="Channel"/> database model.
    /// </summary>
    public interface IRepositoryChannel : IRepositoryBase<Channel> { }
}
