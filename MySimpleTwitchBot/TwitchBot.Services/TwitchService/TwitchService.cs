using System;
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
        /// <summary>
        /// 
        /// </summary>
        public bool Connected { get { return tcpClient?.Connected ?? false; } }
        public Thread ServiceThread { get; private set; }

        private readonly TcpClient tcpClient;
        private readonly StreamReader reader;
        private readonly StreamWriter writer;
        internal string Username { get; private set; }
        internal string Channel { get; private set; }

        /// <summary>
        /// Public constructor that initializes a new TCP Client connecting to Twitch service.
        /// </summary>
        public TwitchService()
        {
            this.tcpClient = new TcpClient(TWITCH_HOST, TWITCH_PORT);
            this.reader = new StreamReader(tcpClient.GetStream());
            this.writer = new StreamWriter(tcpClient.GetStream())
            {
                AutoFlush = true
            };
        }

        /// <summary>
        /// Reads incoming messages from the chat.
        /// </summary>
        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        /// <summary>
        /// Handler that uses the received message. Must use  custom implementation.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Connect to twitch service with provided username and password.
        /// </summary>
        /// <param name="username">Bot username.</param>
        /// <param name="password">Bot provided password token.</param>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        /// <returns>True whether could connect otherwise false.</returns>
        public async Task ConnectAsync(string username, string password, string channel)
        {
            this.Username = username;
            this.Channel = channel;
            try
            {
                // thread that reads incoming messages
                ServiceThread = new Thread(async () =>
                {
                    if (!tcpClient.Connected)
                        await tcpClient.ConnectAsync(TWITCH_HOST, TWITCH_PORT);

                    while (true)
                    {
                        Thread.Sleep(100);

                        if (tcpClient.Available > 0 || reader.Peek() >= 0)
                        {
                            var message = await reader.ReadLineAsync();
                            OnMessageReceived(new MessageReceivedEventArgs(message, channel));
                        }
                    }
                });
                ServiceThread.Start();

                // logs in
                await writer.WriteLineAndFlushAsync(
                        "PASS " + password + Environment.NewLine +
                        "NICK " + username + Environment.NewLine +
                        "USER " + username + " 8 * :" + username
                    );

                // shows how many users are logged on chat, shows online mods
                await writer.WriteLineAndFlushAsync(
                    "CAP REQ :twitch.tv/membership"
                );
                // join chatroom
                await writer.WriteLineAndFlushAsync(
                    $"JOIN #{channel.ToLower()}"
                );
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Reconnect to twitch service with provided username/password/channel.
        /// </summary>
        /// <param name="username">Bot username.</param>
        /// <param name="password">Bot provided password token.</param>
        /// <param name="channel">Channel for the bot to write messages and/or moderate.</param>
        /// <remarks>Passwords meaning your provided token.</remarks>
        public async Task ReconnectAsync(string username, string password, string channel)
        {
            Disconnect();
            this.Username = username;
            this.Channel = channel;
            await ConnectAsync(username, password, channel);
        }

        /// <summary>
        /// Disconnect from twitch service.
        /// </summary>
        public void Disconnect()
        {
            ServiceThread.Abort();
            this.Username = string.Empty;
            this.Channel = string.Empty;
            tcpClient.Close();
        }

        /// <summary>
        /// Write a message to twitch chat.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public async Task SendMessageAsync(string message)
        {
            var msgFormat = 
                $":{Username.ToLower()}!{Username.ToLower()}@{Username.ToLower()}.tmi.twitch.tv PRIVMSG #{Channel.ToLower()} :{message}";

            await this.writer.WriteLineAndFlushAsync(msgFormat);
        }
    }
}
