using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchBot.Services.TwitchService
{
    public static class Extensions
    {
        public async static Task WriteLineAndFlushAsync(this StreamWriter sw, string message)
        {
            await sw.WriteLineAsync(message);
            await sw.FlushAsync();
        }
    }

    /// <summary>
    /// Class responsible for handling functionalities related to the twitch service.
    /// </summary>
    public class TwitchService : ITwitchService
    {
        /// <summary>
        /// Twitch service host.
        /// </summary>
        public const string TWITCH_HOST = "irc.twitch.tv";
        /// <summary>
        /// Twitch service port.
        /// </summary>
        public const int TWITCH_PORT = 6667;

        private string Username => Credentials.Username;
        private string Password => Credentials.Password;

        /// <summary>
        /// Collection to all threads connected to channels.
        /// </summary>
        public ICollection<ServiceThread> Threads { get; private set; }

        /// <summary>
        /// Constructor to initialize threads.
        /// </summary>
        public TwitchService()
        {
            Threads = new List<ServiceThread>();
        }

        /// <summary>
        /// Reads incoming messages from the chat.
        /// </summary>
        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        /// <summary>
        /// Handlers received messages. Must use  custom implementation.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Connect to twitch service with provided username and password.
        /// </summary>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        /// <returns>True whether could connect otherwise false.</returns>
        public async Task ConnectAsync(string channel)
        {
            try
            {
                ServiceThread st = new ServiceThread(TWITCH_HOST, TWITCH_PORT, channel);
                st.Thread = new Thread(async () =>
                {
                    while (true)
                    {
                        Thread.Sleep(100);

                        if (st.TCPClient.Available > 0 || st.Reader.Peek() >= 0)
                        {
                            var message = await st.Reader.ReadLineAsync();
                            OnMessageReceived(new MessageReceivedEventArgs(message, channel));
                        }
                    }
                });
                st.Start();

                // logs in
                await st.Writer.WriteLineAndFlushAsync(
                        "PASS " + Password + Environment.NewLine +
                        "NICK " + Username + Environment.NewLine +
                        "USER " + Username + " 8 * :" + Username
                    );

                // shows how many users are logged on chat, shows online mods
                await st.Writer.WriteLineAndFlushAsync(
                    "CAP REQ :twitch.tv/membership"
                );
                // join chatroom
                await st.Writer.WriteLineAndFlushAsync(
                    $"JOIN #{channel.ToLower()}"
                );

                Threads.Add(st);
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Reconnect to twitch service with provided username/password/channel.
        /// </summary>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        public async Task ReconnectAsync(string channel)
        {
            Disconnect(channel);
            await ConnectAsync(channel);
        }

        /// <summary>
        /// Disconnects bot from a twitch channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        public void Disconnect(string channel)
        {
            var st = Threads.First(t => t.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase));
            st.Abort();
            Threads.Remove(st);
        }

        /// <summary>
        /// Write a message to twitch chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public async Task SendMessageAsync(string message, string channel)
        {
            var serviceThread = Threads.First(st => st.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(message))
            {
                var msgFormat =
                    $":{Username.ToLower()}!{Username.ToLower()}@{Username.ToLower()}.tmi.twitch.tv PRIVMSG #{channel.ToLower()} :{message}";

                await serviceThread.Writer.WriteLineAndFlushAsync(msgFormat);
            }
        }
    }
}
