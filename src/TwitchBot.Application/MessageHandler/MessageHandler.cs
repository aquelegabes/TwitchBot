using System;
using TwitchBot.Services;
using TwitchBot.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using TwitchBot.Domain.Entities;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Models;
using Microsoft.Extensions.Configuration;

namespace TwitchBot.Application.MessageHandler
{
    /// <summary>
    /// Class responsible for handling messages.
    /// </summary>
    public class MessageHandler : IMessageHandler
    {
        /// <summary>
        /// Database access to commands.
        /// </summary>
        private readonly ICommandService commandService;
        private readonly TimeSpan RespondToAtSpan = new TimeSpan(0, 0, 15);
        private readonly List<CommandsUsed> LastCommands = new List<CommandsUsed>();
        private readonly ITwitchService TwitchService;
        private readonly string Username;

        public EventHandler<MessageReceivedEventArgs> Handler { get; set; }

        /// <summary>
        /// Commands used.
        /// </summary>
        private struct CommandsUsed
        {
            public string Command { get; set; }
            public string Channel { get; set; }
            public DateTime LastUsed { get; set; }
        }

        /// <summary>
        /// Last time someone @me.
        /// </summary>
        private DateTime LastAtMe { get; set; } = DateTime.UtcNow;

        public MessageHandler(
            ICommandService commandService,
            ITwitchService service,
            IConfiguration configuration)
        {
            this.commandService = commandService;
            this.TwitchService = service;

            Username = configuration.GetSection("Credentials").GetSection("Username").Value;

            Handler += PongMessageReceivedHandler;
            Handler += AtBotMessageReceivedHandler;
            Handler += CommandMessageReceivedHandler;
        }

        /// <summary>
        /// Handles server ping request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void PongMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            if (e.FromSystemFormat.Contains("ping", StringComparison.OrdinalIgnoreCase))
            {
                Task.Run(async () =>
                {
                    await this.TwitchService
                        .SendMessageAsync("PONG :tmi.twitch.tv", e.Channel, systemMessage: true);
                });
            }
        }

        /// <summary>
        /// Handles that when people @me I respond.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AtBotMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            if (DateTime.Now.ToUniversalTime() > LastAtMe.Add(RespondToAtSpan))
            {
                if (e.IsUserMessage
                    && e?.UsrMessage.Message.Contains(Username, StringComparison.OrdinalIgnoreCase) == true)
                {
                    Task.Run(async () =>
                    {
                        await this.TwitchService
                            .SendMessageAsync("Don't @me bro! I'm trying to working here. DansGame", e.Channel);
                    }).Wait();
                    LastAtMe = DateTime.Now.ToUniversalTime();
                }
            }
        }

        /// <summary>
        /// Handles bot commands.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            if (e.IsUserMessage)
            {
                var msg = e.UsrMessage.Message;
                // special commands
                if (msg.Contains(Command.ACTIVATE_IDENTIFIER) || msg.Contains(Command.DEACTIVATE_IDENTIFIER))
                {
                    IEnumerable<Command> command = new List<Command>();
                    Task.Run(async () => {
                        command = await this.commandService.WhereAsync(comm =>
                               comm.IsSpecialCommand
                            && comm.TypeCommand == msg);
                    }).Wait();

                    if (command.Any())
                    {
                        var msgToSend = command.First().Action;
                        var comm = command.First();
                        // contains any of the flags
                        if ((e.UsrMessage.User.Badges & comm.Operators) != 0)
                        {
                            Task.Run(async () =>
                            {
                                await this.TwitchService
                                    .SendMessageAsync(msgToSend, e.Channel);
                            }).Wait();
                        }
                    }
                }
                // regular commands
                else if (msg.Contains(Command.IDENTIFIER))
                {
                    var commands = new List<string>();
                    msg.Split(' ')
                        .Select(comm =>
                        {
                            if (comm.Length > 1 && comm['0'] == '!')
                                commands.Add(comm);
                            return comm;
                        });
                }
            }
        }
    }
}
