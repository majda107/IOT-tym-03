using System;
using SharpSocket.Connection;

namespace SharpSocket.Connection.Event
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