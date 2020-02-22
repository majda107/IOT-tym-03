using System;
using System.Net.Sockets;
using WebSocketServer.Connection.WebSocket;

namespace WebSocketServer.Connection
{
    public class SocketState
    {
        public static int DEFAULT_BUFFER_SIZE = 512;
        public Socket Client { get; set; }
        public byte[] Buffer { get; set; }
        public int Read { get; set;}

        public SocketState(Socket client)
        {
            this.Client = client;
            this.Buffer = new byte[SocketState.DEFAULT_BUFFER_SIZE];
            this.Read = 0;
        }

        public SocketState(Socket client, byte[] buffer)
        {
            this.Client = client;
            this.Buffer = buffer;
        }
        public SocketState()
        {
            this.Client = null;
            this.Buffer = new byte[SocketState.DEFAULT_BUFFER_SIZE];
        }

        public void Clear()
        {
            this.Buffer = new byte[SocketState.DEFAULT_BUFFER_SIZE];
            this.Read = 0;
        }
    }
}