using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBot.Application.Services;
using Microsoft.AspNetCore.SignalR;
using TwitchBot.Services.Models;
using TwitchBot.Services.Interfaces;
using TwitchBot.Application.Interfaces;
using TwitchBot.Domain.Entities;
using TwitchBot.Services;
using System.Diagnostics;

namespace TwitchBot.WebApp.Pages
{
    public class TwitchServiceBase : ComponentBase
    {
        [Parameter]
        public string Channel { get; set; }
        public bool IsConnected { get; set; } = false;

        [Inject]
        protected ITwitchService Service { get; set; }
        [Inject]
        protected IMessageHandler MessageHandler { get; set; }
        [Inject]
        protected IChannelService ChannelService { get; set; }
        [Inject]
        protected ICommandService CommandService { get; set; }
        protected IList<IDictionary<string, string>> Messages { get; set; } = new List<IDictionary<string, string>>();
        protected int MaxMessages { get; set; }
        protected ICollection<int> MaxChatMessages = new int[]
        {
            10, 50, 100
        };

        protected void AddMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            Debug.WriteLine(e.FromSystemFormat);

            if (e.IsUserMessage)
            {
                //Debug.WriteLine($"{e.UsrMessage.User}: {e.UsrMessage.Message}");
                if (Channel == e.Channel)
                {
                    Messages.Add(new Dictionary<string, string>
                    {
                        { "user", e.UsrMessage.User },
                        { "message", e.UsrMessage.Message }
                    });
                }
                InvokeAsync(this.StateHasChanged);
                // The current thread is not associated with the Dispatcher. 
                // Use InvokeAsync() to switch execution to the Dispatcher when 
                // triggering rendering or component state.
            }

            if (Messages.Count > MaxMessages)
            {
                Messages.Clear();
                GC.SuppressFinalize(Messages);
                Messages = new List<IDictionary<string, string>>();
            }
        }

        protected override void OnInitialized()
        {
            IsConnected = Service.IsConnectedToChannel(Channel);
            Service.MessageReceived += MessageHandler.Handler;
            Service.MessageReceived += AddMessageReceivedHandler;
        }

        protected async Task AddChannel(bool doit = false)
        {
            if (doit)
            {
                Channel chann;
                chann = new Channel
                {
                    Name = Credentials.Username,
                    CreatedAt = DateTime.UtcNow
                };
                await ChannelService.Add(chann);
            }
        }

        protected async Task AddTestCommands(bool doit = false)
        {
            if (doit)
            {
                Channel chann;
                chann = await ChannelService.GetFirst(c => c.Name == Credentials.Username);

                if (await CommandService.Count() < 4)
                {
                    var comm1 = new Command
                    {
                        CreatedAt = DateTime.UtcNow,
                        PublicResponse = true,
                        TypeCommand = "++s",
                        Action = "/subscribers",
                        CreatedBy = chann,
                        Name = "Subscribers mode on",
                        IsSpecialCommand = true,
                        Operators = TwitchBadges.LocalBadges.Moderator | TwitchBadges.LocalBadges.Broadcaster,
                    };

                    await CommandService.Add(comm1);

                    var comm2 = new Command
                    {
                        CreatedAt = DateTime.UtcNow,
                        PublicResponse = true,
                        TypeCommand = "--s",
                        Action = "/subscribersoff",
                        CreatedBy = chann,
                        Name = "Subscribers mode off",
                        IsSpecialCommand = true,
                        Operators = TwitchBadges.LocalBadges.Moderator | TwitchBadges.LocalBadges.Broadcaster,
                    };

                    await CommandService.Add(comm2);

                    var comm3 = new Command
                    {
                        CreatedAt = DateTime.UtcNow,
                        PublicResponse = true,
                        TypeCommand = "++e",
                        Action = "/emoteonly",
                        CreatedBy = chann,
                        Name = "Emote only",
                        IsSpecialCommand = true,
                        Operators = TwitchBadges.LocalBadges.Moderator | TwitchBadges.LocalBadges.Broadcaster,
                    };

                    await CommandService.Add(comm3);

                    var comm4 = new Command
                    {
                        CreatedAt = DateTime.UtcNow,
                        PublicResponse = true,
                        TypeCommand = "--e",
                        Action = "/emoteonlyoff",
                        CreatedBy = chann,
                        Name = "Emote only off",
                        IsSpecialCommand = true,
                        Operators = TwitchBadges.LocalBadges.Moderator | TwitchBadges.LocalBadges.Broadcaster,
                    };

                    await CommandService.Add(comm4);
                }
            }
        }

        protected void Disconnect()
        {
            if (Service.IsConnectedToChannel(Channel))
            {
                Service.Disconnect(Channel);
                Messages.Clear();
                StateHasChanged();
                IsConnected = false;
            }
        }

        protected async Task Connect()
        {
            if (!IsConnected && !string.IsNullOrWhiteSpace(Channel))
            {
                await Service.ConnectAsync(Channel);
                IsConnected = true;
            }
        }
    }
}
