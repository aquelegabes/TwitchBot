using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TwitchBot.Services.Models;
using TwitchBot.Services.Interfaces;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace TwitchBot.Services
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

        /// <summary>
        /// Collection to all threads connected to channels.
        /// </summary>
        public ICollection<ServiceThread> Threads { get; private set; }

        internal TcpClient TCPClient { get; private set; }
        internal StreamReader Reader { get; private set; }
        internal StreamWriter Writer { get; private set; }
        private string Username { get; }

        /// <summary>
        /// Constructor to initialize threads.
        /// </summary>
        public TwitchService(IConfiguration configuration)
        {
            Threads = new List<ServiceThread>();
            
            Username = configuration.GetSection("Credentials:Username").Value;
            string Password = configuration.GetSection("Credentials:Password").Value;
            
            // initializing client
            this.TCPClient = new TcpClient(TWITCH_HOST, TWITCH_PORT);
            this.Reader = new StreamReader(TCPClient.GetStream());
            this.Writer = new StreamWriter(TCPClient.GetStream());

            // logs in
            Task.Run(async () => await Writer.WriteLineAndFlushAsync(
                    "PASS " + Password + Environment.NewLine +
                    "NICK " + Username + Environment.NewLine +
                    "USER " + Username + " 8 * :" + Username
            )).Wait();
            // Adds membership state event (NAMES, JOIN, PART, or MODE) functionality.
            //By default we do not send this data to clients without this capability.
            //await st.Writer.WriteLineAndFlushAsync(
            //     "CAP REQ :twitch.tv/membership"
            // );

            // Enables USERSTATE, GLOBALUSERSTATE, ROOMSTATE, HOSTTARGET, NOTICE and CLEARCHAT raw commands.
            Task.Run(async () => await Writer.WriteLineAndFlushAsync(
                "CAP REQ :twitch.tv/commands"
            )).Wait();

            // Adds IRC v3 message tags to PRIVMSG, USERSTATE, NOTICE and GLOBALUSERSTATE (if enabled with commands CAP)
            Task.Run(async () => await Writer.WriteLineAndFlushAsync(
                "CAP REQ :twitch.tv/tags"
            )).Wait();
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
                ServiceThread st = new ServiceThread(channel);
                st.Thread = new Thread(async () =>
                {
                    try
                    {
                        while (!st.CancellationToken.IsCancellationRequested)
                        {
                            if (TCPClient.Available > 0 || Reader.Peek() >= 0)
                            {
                                var message = await Reader.ReadLineAsync();
                                OnMessageReceived(new MessageReceivedEventArgs(message, channel));
                            }

                            st.CancellationToken.Token.ThrowIfCancellationRequested();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.WriteLine($"[*] Thread ({st.Channel}) has stopped working, through a cancellation request.");
                    }
                });

                st.Start();

                // join chatroom
                await SendMessageAsync(
                    message: $"JOIN #{channel.ToLower()}", 
                    systemMessage: true
                );

                Threads.Add(st);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Reconnect to twitch service with provided username/password/channel.
        /// </summary>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        public async Task ReconnectAsync(string channel)
        {
            try
            {
                Disconnect(channel);
                await ConnectAsync(channel);
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        /// <summary>
        /// Disconnects bot from a twitch channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        public void Disconnect(string channel)
        {
            try
            {
                var st = Threads.First(t => t.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase));
                st.Abort();
                Threads.Remove(st);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Write a message to twitch chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public async Task SendMessageAsync(string message, string channel = "", bool systemMessage = false)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    if (systemMessage == false)
                    {
                        var msgFormat =
                            $":{Username.ToLower()}!{Username.ToLower()}@{Username.ToLower()}.tmi.twitch.tv PRIVMSG #{channel.ToLower()} :{message}";

                        await Writer.WriteLineAndFlushAsync(msgFormat);
                        Debug.WriteLine(msgFormat);
                    }
                    else
                    {
                        await Writer.WriteLineAndFlushAsync(message);
                        Debug.WriteLine(message);
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        /// <summary>
        /// Checks if the bot is already connected to the channel.
        /// </summary>
        /// <param name="channel">Channel name</param>
        /// <returns>True whether could connect, otherwise false.</returns>
        public bool IsConnectedToChannel(string channel)
        {
            return Threads?.Any(t => t.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public void Dispose()
        {
            TCPClient.Close();
            TCPClient.Dispose();
            Writer.Close();
            Writer.Dispose();
            Reader.Close();
            Reader.Dispose();
        }
    }
}
