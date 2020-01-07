using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchBot.Services.TwitchService
{
    public interface IMessageHandler
    {
        /// <summary>
        /// Handles server ping request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void PongMessageReceivedHandler(object sender, MessageReceivedEventArgs message);
    }
}
