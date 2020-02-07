using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using WebSocketServer.Connection.WebSocket;

namespace WebSocketServer.Connection
{
    public class SocketClient
    {
        public Socket Socket { get; private set; }
        public SocketListener Server { get; private set; }

        public SocketClient(Socket socket)
        {
            this.Socket = socket;
        }

        public SocketClient(Socket socket, SocketListener server)
        {
            this.Socket = socket;
            this.Server = server;
        }

        public void Send(string message) 
        {
            WebSocketHandler.CreateSendMessage(message, this);
        }
        public void Send(byte[] data)
        {
            var state = new SocketState(this.Socket, data);

            if (data.Length > SocketState.DEFAULT_BUFFER_SIZE)
                throw new Exception("TODO!!!");

            this.Socket.BeginSend(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), state);
        }

        private void SendCallback(IAsyncResult ar)
        {
            var state = (SocketState)ar.AsyncState;
            int bytesSent = state.Client.EndSend(ar);

            //System.Console.WriteLine("Data sent!");
        }

        public void Receive()
        {
            var state = new SocketState(this.Socket);

            this.Socket.BeginReceive(state.Buffer, 0, SocketState.DEFAULT_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
        }


        private void ReceiveCallback(IAsyncResult ar)
        {
            var state = (SocketState)ar.AsyncState;

            int bytesRead = state.Client.EndReceive(ar);
            if (bytesRead <= 0)
            {
                System.Console.WriteLine("Client disconnected!");
                return;
            }


            var received = Encoding.UTF8.GetString(state.Buffer, 0, bytesRead);
            if (Regex.IsMatch(received, "^GET", RegexOptions.IgnoreCase))
            {
                System.Console.WriteLine("> Handshake started!");
                WebSocketHandler.HandleHandshake(received, this);
            }
            else
            {
                if (WebSocketHandler.IsCancelFrame(state.Buffer, bytesRead))
                {
                    Console.WriteLine("Client disconneted!");
                    this.Server?.EmitDisconnected(this);
                    return;
                }

                received = WebSocketHandler.HandleMessage(state.Buffer);
                this.Server?.EmitMessageReceived(this, received);
                System.Console.WriteLine($"> Received data: {received}");
            }

            state.Client.BeginReceive(state.Buffer, 0, SocketState.DEFAULT_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            Array.Clear(state.Buffer, 0, state.Buffer.Length);
        }
    }
}