using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchBot.Application.Interfaces;
using TwitchBot.Application.Services;
using TwitchBot.Data.Context;
using TwitchBot.Data.Repositories;
using TwitchBot.Domain.Repositories;
using TwitchBot.Services.TwitchService;

namespace TwitchBot.ConsoleApp
{
    partial class Program
    {
        public static string LOCAL_CONN = "Server=127.0.0.1;Port=5432;Database=twitchbot;User Id=postgres;Password=root;";
        public static bool connected = false;
        private static ITwitchService twitchService;

        private static IChannelService channelService;
        private static ICommandService commandService;

        private static IMessageHandler messageHandler;
        private static ServiceProvider ServiceProvider { get; set; }

        public static ServiceProvider ConfigureCollection()
        {
            var collection = new ServiceCollection();

            collection.AddDbContext<TwitchBotContext>(options =>
                options.UseNpgsql(LOCAL_CONN));

            collection.AddScoped<IRepositoryChannel, RepositoryChannel>();
            collection.AddScoped<IRepositoryCommand, RepositoryCommand>();

            collection.AddScoped<IChannelService, ChannelService>();
            collection.AddScoped<ICommandService, CommandService>();

            collection.AddScoped<ITwitchService, TwitchService>();
            collection.AddScoped<IMessageHandler, MessageHandler>();

            collection.BuildServiceProvider();
            return collection.BuildServiceProvider();
        }

        public static void ConfigureMessageHandlers()
        {
            messageHandler = new MessageHandler(twitchService);
            twitchService.MessageReceived += messageHandler.AtBotMessageReceivedHandler;
            twitchService.MessageReceived += messageHandler.PongMessageReceivedHandler;
            twitchService.MessageReceived += Service_MessageReceived;
        }

        public static void ConfigureProviders()
        {
            twitchService = ServiceProvider.GetService<ITwitchService>();
            commandService = ServiceProvider.GetService<ICommandService>();
            channelService = ServiceProvider.GetService<IChannelService>();
        }
    }
}
