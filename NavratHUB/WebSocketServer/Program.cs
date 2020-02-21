using System;
using System.Net;
using WebSocketServer.Connection;
using WebSocketServer.Connection.WebSocket;

namespace WebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // foreach(var adresses in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            // {
            //     System.Console.WriteLine(adresses);
            // }

            var listener = new SocketListener(IPAddress.Parse("192.168.43.169"), 5050);
            // var listener = new SocketListener(IPAddress.Parse("192.168.137.118"), 5050);
            listener.Start();

            listener.ClientConnected += (o, e) => 
            {
                Console.WriteLine("Client connected!");
            };

            listener.ClientDisconnected += (o, e) => 
            {
                Console.WriteLine("Client disconnected!");
            };

            listener.MessageReceived += (o, e) => 
            {
                Console.WriteLine("Received: ", e.Message);
            };

            while(Console.ReadLine().ToLower() != "exit") Console.WriteLine("Type 'exit' to exit...");
        }
    }
}
