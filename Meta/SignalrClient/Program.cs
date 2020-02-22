using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalrClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, ssl) => true;

            Console.WriteLine("Connecting to hub...");
            var connection = new HubConnectionBuilder().WithUrl("http://127.0.0.1:5000/stationhub").Build();

            connection.Closed += async (error) => {
                Console.WriteLine($"Connection closed due to the: {error.ToString()}... retrying in 2 seconds");
                await Task.Delay(2000);
                await connection.StartAsync();
            };

            await connection.StartAsync();

            Console.WriteLine("Conneceted!");

            var line = Console.ReadLine();
            while(line.ToLower() != "exit")
            {
                await connection.SendAsync("SendData", "test", line);
                Console.WriteLine($"~ {line} ... sent ... [exit] for exit");

                line = Console.ReadLine();
            }

            Console.WriteLine("Ending app...");
        }
    }
}
