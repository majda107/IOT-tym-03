using System;
using SharpSocket.Connection;
using System.Net;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 5050;

            var listener = new SocketListener(address, port);
            listener.Start();
            Console.WriteLine("Hello World!");
        }
    }
}
