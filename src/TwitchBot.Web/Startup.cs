using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchBot.Application.Interfaces;
using TwitchBot.Application.Services;
using TwitchBot.Data.Context;
using TwitchBot.Data.Repositories;
using TwitchBot.Domain.Entities;
using TwitchBot.Domain.Repositories;
using TwitchBot.Services.TwitchService;
using TwitchBot.Web.Data;

namespace TwitchBot.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            // DbContext
            services.AddDbContext<TwitchBotContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Local")));

            // Repositories
            services.AddScoped(typeof(IRepositoryChannel), typeof(RepositoryChannel));
            services.AddScoped(typeof(IRepositoryCommand), typeof(RepositoryCommand));

            // Services
            services.AddScoped(typeof(IChannelService), typeof(ChannelService));
            services.AddScoped(typeof(ICommandService), typeof(CommandService));
            services.AddSingleton(typeof(IMessageHandler), typeof(MessageHandler));
            services.AddSingleton(typeof(ITwitchService), typeof(TwitchService));
        }
              
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
