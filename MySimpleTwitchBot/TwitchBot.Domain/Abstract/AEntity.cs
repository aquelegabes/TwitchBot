using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Abstract
{
    /// <summary>
    /// Abstract class that implements <see cref="IEntity"/> and serves as base class for all database entities.
    /// </summary>
    public abstract class AEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
