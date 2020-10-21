using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using ClassLibrary1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            // set server to TCP listener on local
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started!");


            while (true)
            {
                // set client = new tcp client. 
                var connection = server.AcceptTcpClient();

                Console.WriteLine("Accepted client!");

                var stream = connection.GetStream();
                var stream2 = connection.GetStream();
                byte[] data = new byte[connection.ReceiveBufferSize];
                var cnt = stream.Read(data);

                var msg = Encoding.UTF8.GetString(data, 0, cnt);
                Console.WriteLine($"Message from client:  {msg}");

                // Desiralize the request into r and use methods. 

                RequestContainer r = Util.FromJson<RequestContainer>(msg);
                //RequestContainer r = Util.FromJson<RequestContainer>(msg);
                Console.WriteLine("the method of msg is: \n" + r.Method);

                // RESPOND TYPES:
                Response perfect = new Response("1 OK", "Well done.");
                Response created = new Response("2 Created: ", " ");
                Response updated = new Response("3 Updated: ", " ");
                Response badRequest = new Response("4 missing method: ", " ");
                Response notFound = new Response("5 Not found: ", " ");
                Response error = new Response("6 error: ", " ");

                bool whatever = false;
                if (r.Method == null)
                {
                    var missedmethodJson = Util.ToJson(badRequest);
                    Console.WriteLine(missedmethodJson);
                    byte[] response = Encoding.UTF8.GetBytes(missedmethodJson.ToUpper());
                    stream.Write(response);
                    continue;
                }
                else if (r.Date == 0)
                {
                    Response missingDate = new Response("4 missing date", " ");
                    var missDate = Util.ToJson(missingDate);
                    byte[] missDate2 = Encoding.UTF8.GetBytes(missDate.ToUpper());
                    stream.Write(missDate2);
                    Console.WriteLine("we are here");
                    continue;
                }
                else
                {
                    var perfectJson = Util.ToJson(perfect);
                    Console.WriteLine(perfectJson);
                    byte[] response = Encoding.UTF8.GetBytes(perfectJson.ToUpper());
                    stream.Write(response);
                    //stream.Write(Encoding.UTF8.GetBytes(methodExist));
                    Console.WriteLine("method is: " + r.Method);
                }
            }
        }
    }
}