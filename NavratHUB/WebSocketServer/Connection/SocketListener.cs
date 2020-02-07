using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using WebSocketServer.Connection.WebSocket;
using WebSocketServer.Connection.Event;

namespace WebSocketServer.Connection
{
    public class SocketListener
    {
        public delegate void SocketEventHandler(object sender, SocketEventArgs args);
        public delegate void SocketMessageEventHandler(object sender, SocketMessageEventArgs args);


        public event SocketEventHandler ClientConnected;
        public event SocketEventHandler ClientDisconnected;
        public event SocketMessageEventHandler MessageReceived;


        public TcpListener Listener { get; private set; }
        public IList<SocketClient> Clients { get; private set; }
        public SocketListener(IPAddress address, int port)
        {
            this.Listener = new TcpListener(address, port);
            this.Clients = new List<SocketClient>();
        }

        public void Start()
        {
            this.Listener.Start();
            while (true)
            {
                var socket = new SocketClient(this.Listener.AcceptSocket(), this);
                socket.Receive();
                this.EmitConnected(socket);
                this.Clients.Add(socket);
            }
        }

        public void EmitConnected(SocketClient client)
        {
            this.ClientConnected?.Invoke(this, new SocketEventArgs(client));
        }

        public void EmitDisconnected(SocketClient client)
        {
            this.ClientDisconnected?.Invoke(this, new SocketEventArgs(client));
        }

        public void EmitMessageReceived(SocketClient client, string message)
        {
            this.MessageReceived?.Invoke(this, new SocketMessageEventArgs(client, message));
        }
    }
}