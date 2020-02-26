using System;
using SharpSocket.Connection;
using SharpSocket;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace SocketServer
{
    public class Server
    {
        public HubConnection HubConnection { get; private set; }
        public SocketListener SocketServer { get; private set; }
        public Dictionary<string, SocketClient> Clients { get; private set; }

        public Server(IPAddress serverAddress, int serverPort, string hubUrl)
        {
            this.SocketServer = new SocketListener(serverAddress, serverPort);
            this.HubConnection = new HubConnectionBuilder().WithUrl(hubUrl).WithAutomaticReconnect().Build();

            this.Clients = new Dictionary<string, SocketClient>();

            this.Bind();
        }

        private void Bind()
        {
            this.SocketServer.ClientConnected += (o, e) => { Console.WriteLine("Socket client connected!"); };
            this.SocketServer.ClientDisconnected += (o, e) => { Console.WriteLine("Socket client disconnected!"); }; // MAY NOT WORK YET?

            this.SocketServer.MessageReceived += this.HandleSocketData;

            this.HubConnection.Closed += async (e) => { 
                Console.WriteLine("SignalR connection closed... reconnecting in 2 seconds");  
                await Task.Delay(2000);
                await this.HubConnection.StartAsync();
            };

            this.HubConnection.On("OpenGate", () => {
                foreach(var client in this.Clients)
                {
                    if(!client.Key.Contains("gate")) continue;

                    client.Value.Send("open");
                }
            });
        }

        public void Connect()
        {
            Console.WriteLine("Starting socket server...");
            new Thread(() => this.SocketServer.Start()).Start(); 
            Console.WriteLine("Socket server started...");


            Console.WriteLine("Connecting to SignalR hub...");
            this.HubConnection.StartAsync().Wait();
            Console.WriteLine("Connected to SignalR hub...");
        }

        private void HandleSocketData(object sender, SharpSocket.Connection.Event.SocketMessageEventArgs e)
        {
            var split = e.Message.Split(";");
            if(split.Length == 2)
                Task.Run(() => this.RespondAsync(split[0], split[1])); // RUN AND FORGET
                
            else if(split.Length == 1)
            {
                Console.WriteLine("Adding client to database");

                if(this.Clients.ContainsKey(e.Message))
                    this.Clients[e.Message] = e.Socket;
                else
                    this.Clients.Add(e.Message, e.Socket);
            }
            else
                Console.WriteLine($"Invalid data ~ {e.Message}");
        }

        public async Task RespondAsync(string sensor, string value)
        {
            if(this.HubConnection.State == HubConnectionState.Connected)
                await this.HubConnection.SendAsync("SendData", sensor, value);
            else
                Console.WriteLine("Can't send data to hub because the connection isn't active..."); // QUEUE IN FUTURE?
        }
    }
}