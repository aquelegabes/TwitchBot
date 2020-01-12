using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Models;

namespace TwitchChat.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITwitchService _twitchService;
        private readonly IMessageHandler _msgHandler;

        public Worker(
            ILogger<Worker> logger,
            ITwitchService twitchService,
            IMessageHandler msgHandler)
        {
            _logger = logger;
            _twitchService = twitchService;
            _msgHandler = msgHandler;
        }

        private void IncomingMessages(object sender, MessageReceivedEventArgs e)
        {
            _logger.LogDebug(e.FromSystemFormat);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker started at: {DateTime.UtcNow} UTC.");
            _twitchService.MessageReceived += _msgHandler.Handler;
            _twitchService.MessageReceived += IncomingMessages;
            await _twitchService.ConnectAsync("itshafu");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping worker");
            _twitchService.Disconnect("itshafu");
            await Task.Delay(1 * 1000);
            _logger.LogInformation($"Worker stopped at: {DateTime.UtcNow} UTC.");
        }
    }
}
