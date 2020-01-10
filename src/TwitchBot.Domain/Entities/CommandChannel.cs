using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Abstract;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Entities
{
    public class CommandChannel : AEntity, IEntity
    {
        public Guid CommandId { get; set; }
        public Guid ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual Command Command { get; set; }
    }
}
