using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Abstract;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Entities
{
    /// <summary>
    /// Authorized channels.
    /// </summary>
    public class Channel : AEntity, IEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<CommandChannel> CommandsInChannel { get; set; }
        public virtual IEnumerable<Command> CreatedCommands { get; set; }
    }
}
