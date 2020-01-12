using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchBot.Application.Interfaces;
using TwitchBot.Application.MessageHandler;
using TwitchBot.Application.Services;
using TwitchBot.Data.Context;
using TwitchBot.Data.Repositories;
using TwitchBot.Domain.Repositories;
using TwitchBot.Services;
using TwitchBot.Services.Interfaces;

namespace TwitchChat.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddDbContext<TwitchBotContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("Local")));

                    services.AddSingleton<ICommandService, CommandService>();
                    services.AddSingleton<ITwitchService, TwitchService>();
                    services.AddSingleton<IMessageHandler, MessageHandler>();
                    services.AddSingleton<IRepositoryCommand, RepositoryCommand>();
                    services.AddHostedService<Worker>();
                });
        }
    }
}
