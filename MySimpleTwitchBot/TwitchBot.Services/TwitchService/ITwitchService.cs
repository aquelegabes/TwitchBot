using System;
using System.Threading.Tasks;

namespace TwitchBot.Services.TwitchService
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
        /// Connect to twitch service with provided username and password.
        /// </summary>
        /// <param name="username">Bot username.</param>
        /// <param name="password">Bot provided password token.</param>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        /// <returns>True whether could connect otherwise false.</returns>
        Task ConnectAsync(string username, string password, string channel);

        /// <summary>
        /// Reconnect to twitch service with provided username/password/channel.
        /// </summary>
        /// <param name="username">Bot username.</param>
        /// <param name="password">Bot provided password token.</param>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        Task ReconnectAsync(string username, string password, string channel);

        /// <summary>
        /// Disconnect from twitch service.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Write a message to twitch chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        Task SendMessageAsync(string message);
    }
}
