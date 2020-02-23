using SharpSocket.Connection;

namespace SharpSocket.Connection.Event
{
    public class SocketMessageEventArgs : SocketEventArgs
    {
        public string Message { get; private set; }
        public SocketMessageEventArgs(SocketClient socket, string message) : base(socket)
        {
            this.Message = message;
        }
    }
}