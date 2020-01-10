using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TwitchBot.Domain.Abstract;
using TwitchBot.Domain.Interfaces;
using TwitchBot.Services.Models;
using static TwitchBot.Services.Models.TwitchBadges;

namespace TwitchBot.Domain.Entities
{
    /// <summary>
    /// Bot commands.
    /// </summary>
    public class Command : AEntity, IEntity
    {
        [NotMapped]
        public const string IDENTIFIER = "!";
        [NotMapped]
        public const string ACTIVATE_IDENTIFIER = "++";
        [NotMapped]
        public const string DEACTIVATE_IDENTIFIER = "--";

        public string Name { get; set; }
        public string TypeCommand { get; set; }
        public string Action { get; set; }
        public bool PublicResponse { get; set; }
        public bool IsSpecialCommand { get; set; }
        public Guid CreatedById { get; set; }
        public LocalBadges Operators { get; set; }
        public virtual Channel CreatedBy { get; set; }
        public virtual IEnumerable<CommandChannel> CommandsInChannel { get; set; }
    }
}
