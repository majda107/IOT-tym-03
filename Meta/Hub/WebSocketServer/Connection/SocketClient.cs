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
        public bool HandShake {get; private set;}

        public SocketClient(Socket socket)
        {
            this.Socket = socket;
            this.HandShake = false;
        }

        public SocketClient(Socket socket, SocketListener server)
        {
            this.Socket = socket;
            this.Server = server;
            this.HandShake = false;
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

            state.Read += bytesRead;

            string received = null;
            Console.WriteLine($" ~ Receive callback! Bytes read: {bytesRead}, Available: {state.Client.Available}");

            // var received = Encoding.UTF8.GetString(state.Buffer, 0, bytesRead);
            if(this.HandShake)
            {
                if (WebSocketHandler.IsCancelFrame(state.Buffer, state.Read))
                {
                    Console.WriteLine("Client disconneted!");
                    this.Server?.EmitDisconnected(this);
                    return;
                }

                // received = WebSocketHandler.HandleMessage(state.Buffer, state.Read);

                // if(received != null)
                // {
                //     this.Server?.EmitMessageReceived(this, received);
                //     System.Console.WriteLine($"> Received data: {received}");

                //     Console.WriteLine("Clearing state...");
                //     state.Clear();
                //     bytesRead = 0;    
                // }

                var messages = WebSocketHandler.HandlePacket(state.Buffer, state.Read);
                foreach(var message in messages)
                {
                    this.Server?.EmitMessageReceived(this, message);
                    System.Console.WriteLine($"> Received data: {message}");
                }

                if(messages.Count > 0)
                {
                    Console.WriteLine("Clearing state...");
                    state.Clear();
                    bytesRead = 0;    
                }
            }
            else
            {
                received = Encoding.UTF8.GetString(state.Buffer, 0, state.Read);
                if (Regex.IsMatch(received, "^GET", RegexOptions.IgnoreCase) && received.Contains("Key"))
                {
                    System.Console.WriteLine("> Handshake started!");
                    WebSocketHandler.HandleHandshake(received, this);
                    this.HandShake = true;

                    Console.WriteLine("Clearing state...");
                    state.Clear();
                    bytesRead = 0;
            
                }
            }

            state.Client.BeginReceive(state.Buffer, bytesRead, SocketState.DEFAULT_BUFFER_SIZE - state.Read, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            // Array.Clear(state.Buffer, 0, state.Buffer.Length);
        }
    }
}