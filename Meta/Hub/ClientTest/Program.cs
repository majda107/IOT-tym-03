using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;


namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Parse("192.168.43.169"), 5050);
            server.Start();

            Console.WriteLine("Listening...");

            var socket = server.AcceptSocket();

            var buffer = new byte[1024];
            socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

            var message = System.Text.Encoding.UTF8.GetString(buffer);
            Console.WriteLine(message);

            Console.ReadKey();
        }
    }
}
