using System;

namespace TwitchBot.Domain.Interfaces
{
    /// <summary>
    /// Interface responsible for handling database entities.
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
