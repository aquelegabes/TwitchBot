using System;
using System.Threading.Tasks;
using TwitchBot.Services.Models;

namespace TwitchBot.Services.Interfaces
{
    /// <summary>
    /// Inteface responsible for handling <see cref="TwitchService"/> functionalities.
    /// </summary>
    public interface ITwitchService
    {
        /// <summary>
        /// Event that handles how messages are received.
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Checks if the bot is already connected to the channel.
        /// </summary>
        /// <param name="channel">Channel name</param>
        /// <returns>True whether could connect, otherwise false.</returns>
        bool IsConnectedToChannel(string channel);

        /// <summary>
        /// Connect to twitch service with provided username and password.
        /// </summary>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        /// <returns>True whether could connect, otherwise false.</returns>
        Task ConnectAsync(string channel);

        /// <summary>
        /// Reconnect to twitch service with provided username/password/channel.
        /// </summary>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        Task ReconnectAsync(string channel);

        /// <summary>
        /// Disconnects bot from a twitch channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        void Disconnect(string channel);

        /// <summary>
        /// Write a message to twitch chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <param name="channel">Channel to send message.</param>
        Task SendMessageAsync(string message, string channel = "", bool systemMessage = false);
    }
}
