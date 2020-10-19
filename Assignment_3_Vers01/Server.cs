using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using ClassLibrary1;
using Newtonsoft.Json;

namespace Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started!");

            while (true)
            {
                /*var client = server.AcceptTcpClient();
                Console.WriteLine("Accepted client!");

                var stream = client.GetStream();
                var msg = Read(client, stream);

                Console.WriteLine($"Message from Server-Client {msg}");

                var data = Encoding.UTF8.GetBytes(msg.ToUpper());
                stream.Write(data);
                
                /*private static string Read(TcpClient client, NetworkStream stream)
                {
                    byte[] data = new byte[client.ReceiveBufferSize];
                    var cnt = stream.Read(data);
                    var msg = Encoding.UTF8.GetString(data, 0, cnt);
                    return msg;
                }*/
                
                ////////jacob/////////
                var client = server.AcceptTcpClient();
                Console.WriteLine("Accepted client!");

                var stream = client.GetStream();
                byte[] data = new byte[client.ReceiveBufferSize];
                var cnt = stream.Read(data);
                var msg = Encoding.UTF8.GetString(data, 0, cnt);
                
                Console.WriteLine($"Message from client:  {msg}");

                data = Encoding.UTF8.GetBytes(msg.ToUpper());
                
                stream.Write(data);
                //////jacob////////
                
                var path  = @"C:\Users\45535\Desktop\RAWDATA\C#\Projects\Assignment_3_Vers01\ClassLibrary1\JsonFiles\JsonServerTest.json";
                
                // Write input from client to Json response file.
                File.WriteAllText(path, msg);
                
            }
        }
    }
}