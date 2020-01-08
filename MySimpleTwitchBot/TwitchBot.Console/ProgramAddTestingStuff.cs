using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchBot.Domain.Entities;
using TwitchBot.Services.TwitchService;

namespace TwitchBot.ConsoleApp
{
    partial class Program
    {
        public static async Task AddDefaultCommands()
        {
            Channel chann;
            if (await channelService.Count() < 1)
            {
                chann = new Channel
                {
                    Name = Credentials.Username,
                    CreatedAt = DateTime.UtcNow
                };
                await channelService.Add(chann);
            }
            else
                chann = await channelService.GetById("a2be366e-ec10-4b5b-b443-2ec8b4b92cdc");

            if (await commandService.Count() < 4)
            {
                //var comm1 = new Command
                //{
                //    CreatedAt = DateTime.UtcNow,
                //    PublicResponse = true,
                //    TypeCommand = "++s",
                //    Action = "/subscribers",
                //    CreatedBy = chann,
                //    Name = "Subscribers mode on",
                //};

                //await commandService.Add(comm1);

                var comm2 = new Command
                {
                    CreatedAt = DateTime.UtcNow,
                    PublicResponse = true,
                    TypeCommand = "--s",
                    Action = "/subscribersoff",
                    CreatedBy = chann,
                    Name = "Subscribers mode off",
                };

                await commandService.Add(comm2);

                var comm3 = new Command
                {
                    CreatedAt = DateTime.UtcNow,
                    PublicResponse = true,
                    TypeCommand = "++e",
                    Action = "/emoteonly",
                    CreatedBy = chann,
                    Name = "Emote only",
                };

                await commandService.Add(comm3);

                var comm4 = new Command
                {
                    CreatedAt = DateTime.UtcNow,
                    PublicResponse = true,
                    TypeCommand = "--e",
                    Action = "/emoteonlyoff",
                    CreatedBy = chann,
                    Name = "Emote only off",

                };

                await commandService.Add(comm4);
            }
        }
    }
}
