using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchBot.Services.TwitchService
{
    /// <summary>
    /// Class responsible for handling messages.
    /// </summary>
    public class MessageHandler : IMessageHandler
    {
        /// <summary>
        /// Twitch service.
        /// </summary>
        private readonly ITwitchService service;

        public MessageHandler(ITwitchService service)
        {
            this.service = service;
            service.MessageReceived += PongMessageReceivedHandler;
        }

        /// <summary>
        /// Handles server ping request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void PongMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            if (e.FromSystemFormat.Contains("ping", StringComparison.OrdinalIgnoreCase))
                this.service.SendMessageAsync("PONG :tmi.twitch.tv");
        }
    }
}
