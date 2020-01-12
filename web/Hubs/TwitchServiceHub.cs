using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBot.WebApp.Hubs
{
    public class TwitchServiceHub : Hub
    {
        public async Task ConnectToChannel(string channel, string validationToken)
        {
            await Clients.All.SendAsync("Connect", channel, validationToken);
        }

        public async Task KeepChannelConnected(string channel)
        {
            await Clients.All.SendAsync("KeepConnected", channel);
        }
    }
}
