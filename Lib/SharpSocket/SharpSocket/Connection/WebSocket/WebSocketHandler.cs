using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpSocket.Connection.WebSocket
{
    public static class WebSocketHandler
    {
        public static byte WS_FIN = 0x80;
        public static bool DEBUG = true;
        public static void HandleHandshake(string received, SocketClient socket)
        {
            if(DEBUG) 
                Console.WriteLine($"Handshake data: {received}");

            string swk = Regex.Match(received, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();

            if(DEBUG)
                Console.WriteLine($"Received client key: {swk}");

            string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(swka));
            string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

            byte[] response = Encoding.UTF8.GetBytes(
                "HTTP/1.1 101 Switching Protocols\r\n" +
                "Connection: Upgrade\r\n" +
                "Upgrade: websocket\r\n" +
                "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

            if(DEBUG)
                Console.WriteLine($"Sending handshake hash: {swkaSha1Base64}");

            socket.Send(response);
        }
        public static string HandleMessage(byte[] buffer, int read)
        {   
            bool mask = (buffer[1] & 0b10000000) != 0;
            int msglen = buffer[1] - 128;
            int offset = 2;
            
            if (msglen == 126)
            {   
                msglen = BitConverter.ToUInt16(new byte[] {buffer[3], buffer[2]}, 0);
                offset = 4;
            }
            
            if (mask)
            {   
                byte[] decoded = new byte[msglen];
                byte[] masks = new byte[4]
                    {buffer[offset], buffer[offset + 1], buffer[offset + 2], buffer[offset + 3]};
                offset += 4;

                if(offset + msglen > read) 
                {
                    return null;
                }
                
                for (int i = 0; i < msglen; ++i)
                    decoded[i] = (byte) (buffer[offset + i] ^ masks[i % 4]);
                
                return Encoding.UTF8.GetString(decoded);
            }
            
            //return String.Empty;
            return null;
        }

        public static bool IsCancelFrame(byte[] buffer, int bytesRead)
        {
            if(bytesRead > 8 || buffer.Length < 0) 
                return false;

            if((buffer[0] | WS_FIN) == 0x08)
                return true;

            // if(buffer[0] > 128 && buffer[0] <= 143  ) 
            //     return true;


            return false;
        }

        public static void CreateSendMessage(string data, SocketClient client)
        {
            var payload = Encoding.UTF8.GetBytes(data);
            using (var packet = new MemoryStream())
            {
                byte firstbyte = 0b0_0_0_0_0000;
                firstbyte |= 0b1_0_0_0_0000;

                firstbyte += (byte) 0x1;
                packet.WriteByte(firstbyte);

                byte secondbyte = 0b0_0000000; // mask | [SIZE | SIZE  | SIZE  | SIZE  | SIZE  | SIZE | SIZE]

                if (payload.LongLength <= 0b0_1111101) // 125
                {
                    secondbyte |= (byte) payload.Length;
                    packet.WriteByte(secondbyte);
                }
                else if (payload.LongLength <= UInt16.MaxValue) // If length takes 2 bytes
                {
                    secondbyte |= 0b0_1111110; // 126
                    packet.WriteByte(secondbyte);

                    var len = BitConverter.GetBytes(payload.LongLength);
                    Array.Reverse(len, 0, 2);
                    packet.Write(len, 0, 2);
                }
                else // if (payload.LongLength <= Int64.MaxValue) // If length takes 8 bytes
                {
                    secondbyte |= 0b0_1111111; // 127
                    packet.WriteByte(secondbyte);

                    var len = BitConverter.GetBytes(payload.LongLength);
                    Array.Reverse(len, 0, 8);
                    packet.Write(len, 0, 8);
                }

                packet.Write(payload, 0, payload.Length);
                client.Send(packet.ToArray());
            }
        }
    }
}