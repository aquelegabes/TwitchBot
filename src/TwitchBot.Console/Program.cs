using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TwitchBot.Application.Services;
using TwitchBot.Data.Context;
using TwitchBot.Data.Repositories;
using TwitchBot.Domain.Repositories;
using TwitchBot.Services;
using TwitchBot.Services.Models;

namespace TwitchBot.ConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider = ConfigureCollection();
            ConfigureProviders();
            ConfigureMessageHandlers();

            // await AddDefaultCommands();

            string channel = "luigivaraschin237";
            await twitchService.ConnectAsync(channel);
            
            Console.WriteLine("[*] Awaiting connection...");
            while (true)
            {
                if (connected)
                {
                    var message = Console.ReadLine();
                    await twitchService.SendMessageAsync(message, channel);
                }
            }
        }

        private static void Service_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.IsUserMessage)
                Console.WriteLine($"[*] {e.UsrMessage.User}: {e.UsrMessage.Message}");
            //else
                Console.WriteLine($"[*] {e.ToString()}");

            if (e.FromSystemFormat.Contains("366", StringComparison.OrdinalIgnoreCase))
            {
                connected = true;
                Console.WriteLine("[*] Connected");
            }
        }
    }
}
