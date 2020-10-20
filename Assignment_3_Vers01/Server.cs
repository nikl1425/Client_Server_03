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
                byte[] data = new byte[connection.ReceiveBufferSize];
                var cnt = stream.Read(data);

                var msg = Encoding.UTF8.GetString(data, 0, cnt);
                Console.WriteLine($"Message from client:  {msg}");

                data = Encoding.UTF8.GetBytes(msg.ToUpper());

                stream.Write(data);

                //var path = @"C:\Users\45535\Desktop\RAWDATA\C#\Projects\Assignment_3_Vers01\Assignment_3_Vers01\JsonServerTest.json";

                // Write input from client to Json response file.
                //File.WriteAllText(path, msg);

                // Desiralize the request into r and use methods. 

                RequestContainer r = JsonConvert.DeserializeObject<RequestContainer>(msg);
                //RequestContainer r = Util.FromJson<RequestContainer>(msg);
                Console.WriteLine("the method of msg is: \n" + r.Method);

                // RESPOND TYPES:
                Response missingMethod = new Response(1, "Define method in json");


                // test and send select response
                if (r.Method != null)
                {
                    var MissedmethodJson = missingMethod.ToJson();
                    var missMethod = Encoding.UTF8.GetBytes(MissedmethodJson);
                    Console.WriteLine(MissedmethodJson);
                    byte[] random = Encoding.UTF8.GetBytes(MissedmethodJson);
                    stream.Write(Encoding.UTF8.GetBytes(MissedmethodJson.ToUpper()));
                    //stream.Write(Encoding.UTF8.GetBytes(methodExist));
                    Console.WriteLine("method is: " + r.Method);
                    
                }
                else
                {
                    Console.WriteLine("Method missing in request");
                }
            }
        }
    }
}