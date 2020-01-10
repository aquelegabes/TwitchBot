using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Domain.Abstract;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Entities
{
    public class TwitchBadge : AEntity, IEntity
    {
        public int Flag { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
