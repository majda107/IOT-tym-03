using System;
using SharpSocket.Connection;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // var server = new Server(IPAddress.Parse("192.168.137.118"), 5050, "http://127.0.0.1:5000/stationhub");
            var server = new Server(IPAddress.Parse("192.168.137.118"), 5050, "http://192.168.137.118:5000/stationhub");
            server.Connect();

            Console.Write("Server running... type anything to send as test data, [exit] to stop the server...: ");
            var line = Console.ReadLine();
            while(line.ToLower() != "exit")
            {
                Task.Run(() => server.RespondAsync("temperature", line));
                Console.WriteLine("...");
                line = Console.ReadLine();
            }
        }
    }
}
