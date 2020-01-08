using System.ComponentModel.DataAnnotations.Schema;
using TwitchBot.Domain.Abstract;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Entities
{
    /// <summary>
    /// Bot commands.
    /// </summary>
    public class Command : AEntity, IEntity
    {
        [NotMapped]
        public const string IDENTIFIER = "!";
        public string Name { get; set; }
        public string TypeCommand { get; set; }
        public string Action { get; set; }
        public bool PublicResponse { get; set; }
        public virtual Channel CreatedBy { get; set; }
    }
}
