using System;
using WebSocketServer.Connection;

namespace WebSocketServer.Connection.Event
{
    public class SocketEventArgs : EventArgs
    {
        public SocketClient Socket { get; private set; }
        public SocketEventArgs(SocketClient socket)
        {
            this.Socket = socket;
        }
    }
}