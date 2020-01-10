using System;
using TwitchBot.Services.Models;

namespace TwitchBot.Services.Interfaces
{
    /// <summary>
    /// Interface for creating a message handler.
    /// Set all your handling to <see cref="Handler"/> on constructor, then injects on your application layer.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Message received handler.
        /// </summary>
        EventHandler<MessageReceivedEventArgs> Handler { get; set; }
    }
}
