using System;
using System.Threading.Tasks;
using TwitchBot.Services.TwitchService;

namespace TwitchBot.ConsoleApp
{
    class Program
    {
        public static bool connected = false;
        static async Task Main(string[] args)
        {
            TwitchService service =
                new TwitchService();

            service.MessageReceived += Service_MessageReceived;

            await service.ConnectAsync(Credentials.Username, Credentials.Password, Credentials.Channel);

            Console.WriteLine("[*] Awaiting connection...");
            while (true)
            {
                if (connected)
                {
                    var message = Console.ReadLine();
                    await service.SendMessageAsync(message);
                }
            }
        }

        private static void Service_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.IsUserMessage)
                Console.WriteLine($"[*] {e.UsrMessage.User}: {e.UsrMessage.Message}");
            else
                Console.WriteLine($"[*] {e.ToString()}");

            if (e.FromSystemFormat.Contains("366", StringComparison.OrdinalIgnoreCase))
            {
                connected = true;
                Console.WriteLine("[*] Connected");
            }
        }
    }
}
